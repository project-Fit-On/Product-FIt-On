import json

import cv2
import bpy
import mediapipe as mp
import numpy as np
import piexif
from mediapipe.python.solutions import face_mesh

from main import shoulder_m

# -----------------------------------------------------------------------------
# 1) MediaPipe Pose + FaceMesh Setup
# -----------------------------------------------------------------------------
mp_pose = mp.solutions.pose
pose = mp_pose.Pose()

face_mesh_detector = face_mesh.FaceMesh(
    static_image_mode=True,
    max_num_faces=1,
    refine_landmarks=True,
    min_detection_confidence=0.5
)

# -----------------------------------------------------------------------------
# 2) Read Focal Length from EXIF
# -----------------------------------------------------------------------------
def get_focal_length(image_path):
    """
    Reads EXIF FocalLength (tag 0x920A) from a JPEG using piexif.
    Returns the focal length in mm (float), or None if missing.
    """
    exif_data = piexif.load(image_path)
    exif_ifd = exif_data.get("Exif", {})
    focal_length_tag = 0x920A

    if focal_length_tag in exif_ifd:
        # Typically stored as a Rational, e.g. (57, 10) => 5.7 mm
        focal_rational = exif_ifd[focal_length_tag]
        numerator, denominator = focal_rational
        return numerator / denominator
    else:
        return None

# Attempt to read focal lengths from both images
focal_front_mm = get_focal_length("front_view.jpg")
focal_side_mm  = get_focal_length("side_view.jpg")

# Decide on final focal length
if focal_front_mm is None and focal_side_mm is None:
    print("⚠️ No EXIF focal length found in either image. Falling back to 5.7 mm.")
    FOCAL_LENGTH_MM = 5.7
elif focal_front_mm is None:
    print(f"⚠️ No EXIF focal length in front_view, using side_view only ({focal_side_mm:.2f} mm)")
    FOCAL_LENGTH_MM = focal_side_mm
elif focal_side_mm is None:
    print(f"⚠️ No EXIF focal length in side_view, using front_view only ({focal_front_mm:.2f} mm)")
    FOCAL_LENGTH_MM = focal_front_mm
else:
    # Both found
    FOCAL_LENGTH_MM = (focal_front_mm + focal_side_mm) / 2
    print(f"Focal lengths found: front={focal_front_mm:.2f} mm, side={focal_side_mm:.2f} mm.")
    print(f"Using average FOCAL_LENGTH_MM = {FOCAL_LENGTH_MM:.2f} mm")

# -----------------------------------------------------------------------------
# 3) Camera/Sensor Specs
# -----------------------------------------------------------------------------
SENSOR_WIDTH_MM  = 9.0
SENSOR_HEIGHT_MM = 7.0
IMAGE_WIDTH_PIXELS  = 6048
IMAGE_HEIGHT_PIXELS = 4524

# Convert mm -> px
FOCAL_LENGTH_X_PIXELS = (FOCAL_LENGTH_MM / SENSOR_WIDTH_MM) * IMAGE_WIDTH_PIXELS
FOCAL_LENGTH_Y_PIXELS = (FOCAL_LENGTH_MM / SENSOR_HEIGHT_MM) * IMAGE_HEIGHT_PIXELS

# -----------------------------------------------------------------------------
# 4) Face Landmark Functions
# -----------------------------------------------------------------------------
def detect_face_landmarks(image):
    """
    Uses MediaPipe FaceMesh to find left_eye (33) and right_eye (263).
    Returns {'left_eye': (x,y), 'right_eye': (x,y)} or None if no face found.
    """
    image_rgb = cv2.cvtColor(image, cv2.COLOR_BGR2RGB)
    results = face_mesh_detector.process(image_rgb)

    if results.multi_face_landmarks:
        face_landmarks = results.multi_face_landmarks[0]
        left_eye  = face_landmarks.landmark[33]
        right_eye = face_landmarks.landmark[263]

        h, w, _ = image.shape
        left_eye_coords  = (int(left_eye.x * w), int(left_eye.y * h))
        right_eye_coords = (int(right_eye.x * w), int(right_eye.y * h))

        return {'left_eye': left_eye_coords, 'right_eye': right_eye_coords}
    return None

def estimate_distance_from_eyes(image):
    """
    Estimate camera-to-face distance based on average IPD (63 mm).
    distance_m ~ (IPD_m * focal_length_in_px) / (eye_distance_pixels)
    """
    landmarks = detect_face_landmarks(image)
    if landmarks is None:
        return None

    left_eye_px  = landmarks['left_eye']
    right_eye_px = landmarks['right_eye']
    eye_distance_pixels = np.linalg.norm(np.array(left_eye_px) - np.array(right_eye_px))

    IPD_METERS = 0.063
    distance_m = (IPD_METERS * FOCAL_LENGTH_X_PIXELS) / eye_distance_pixels
    return distance_m

# -----------------------------------------------------------------------------
# 5) Pose Functions
# -----------------------------------------------------------------------------
def detect_keypoints(image):
    img_rgb = cv2.cvtColor(image, cv2.COLOR_BGR2RGB)
    results = pose.process(img_rgb)
    return results

def get_landmark_coords(image, landmarks, idx):
    h, w, _ = image.shape
    landmark = landmarks.landmark[idx]
    if landmark.visibility > 0.3:
        x = int(landmark.x * w)
        y = int(landmark.y * h)
        return (x, y)
    return None

def pixel_to_meters(pixel_distance, focal_length_pixels, depth_meters):
    if pixel_distance is None:
        return None
    return (pixel_distance * depth_meters) / focal_length_pixels

def measure_front_view(image, depth):
    """
    Returns (shoulder_width_m, waist_width_m).
    """
    results = detect_keypoints(image)
    if not results.pose_landmarks:
        return None, None

    landmarks = results.pose_landmarks

    # Shoulder
    left_shoulder = get_landmark_coords(image, landmarks, mp_pose.PoseLandmark.LEFT_SHOULDER)
    right_shoulder = get_landmark_coords(image, landmarks, mp_pose.PoseLandmark.RIGHT_SHOULDER)
    if left_shoulder and right_shoulder:
        shoulder_px = np.linalg.norm(np.array(left_shoulder) - np.array(right_shoulder))
        shoulder_m = pixel_to_meters(shoulder_px, FOCAL_LENGTH_X_PIXELS, depth)
    else:
        shoulder_m = None

    # Waist
    left_hip = get_landmark_coords(image, landmarks, mp_pose.PoseLandmark.LEFT_HIP)
    right_hip = get_landmark_coords(image, landmarks, mp_pose.PoseLandmark.RIGHT_HIP)
    if left_hip and right_hip:
        waist_px = np.linalg.norm(np.array(left_hip) - np.array(right_hip))
        waist_m = pixel_to_meters(waist_px, FOCAL_LENGTH_X_PIXELS, depth)
    else:
        waist_m = None

    return shoulder_m, waist_m

def measure_side_view(image, depth):
    """
    Returns (height_m, stomach_back_m).
    """
    results = detect_keypoints(image)
    if not results.pose_landmarks:
        return None, None

    landmarks = results.pose_landmarks

    # Top of head: midpoint of ears or fallback to nose
    left_ear  = get_landmark_coords(image, landmarks, mp_pose.PoseLandmark.LEFT_EAR)
    right_ear = get_landmark_coords(image, landmarks, mp_pose.PoseLandmark.RIGHT_EAR)
    if left_ear and right_ear:
        head_top = ((left_ear[0] + right_ear[0]) // 2,
                    (left_ear[1] + right_ear[1]) // 2)
    else:
        head_top = get_landmark_coords(image, landmarks, mp_pose.PoseLandmark.NOSE)

    # Foot or ankle
    foot_idx = get_landmark_coords(image, landmarks, mp_pose.PoseLandmark.RIGHT_FOOT_INDEX)
    if not foot_idx:
        foot_idx = get_landmark_coords(image, landmarks, mp_pose.PoseLandmark.RIGHT_ANKLE)

    if head_top and foot_idx:
        height_px = np.linalg.norm(np.array(head_top) - np.array(foot_idx))
        height_m = pixel_to_meters(height_px, FOCAL_LENGTH_Y_PIXELS, depth)
    else:
        height_m = None

    # Shoulders -> hips
    left_shoulder = get_landmark_coords(image, landmarks, mp_pose.PoseLandmark.LEFT_SHOULDER)
    right_shoulder = get_landmark_coords(image, landmarks, mp_pose.PoseLandmark.RIGHT_SHOULDER)
    left_hip = get_landmark_coords(image, landmarks, mp_pose.PoseLandmark.LEFT_HIP)
    right_hip = get_landmark_coords(image, landmarks, mp_pose.PoseLandmark.RIGHT_HIP)

    if left_shoulder and right_shoulder and left_hip and right_hip:
        shoulder_mid = (
            (left_shoulder[0] + right_shoulder[0]) // 2,
            (left_shoulder[1] + right_shoulder[1]) // 2
        )
        hip_mid = (
            (left_hip[0] + right_hip[0]) // 2,
            (left_hip[1] + right_hip[1]) // 2
        )
        thickness_px = np.linalg.norm(np.array(hip_mid) - np.array(shoulder_mid))
        stomach_back_m = pixel_to_meters(thickness_px, FOCAL_LENGTH_X_PIXELS, depth)
    else:
        stomach_back_m = None

    return height_m, stomach_back_m

# -----------------------------------------------------------------------------
# 6) Main
# -----------------------------------------------------------------------------
if __name__ == "__main__":
    front_image = cv2.imread("front_view.jpg")
    side_image  = cv2.imread("side_view.jpg")

    if front_image is None or side_image is None:
        print("⚠️ Error: Could not load front_view.jpg or side_view.jpg")
        exit()

    # Estimate distance from eyes in front image
    depth_est = estimate_distance_from_eyes(front_image)
    if depth_est is None:
        depth_est = 2.0  # fallback if face detection fails
        print("⚠️ Could not detect face in front image. Falling back to depth=2.0 m")
    else:
        print(f"Estimated depth from eyes: {depth_est:.2f} m")

    # Measure from front + side
    shoulder_width_m, waist_width_m = measure_front_view(front_image, depth_est)
    height_m, stomach_back_m = measure_side_view(side_image, depth_est)

    if shoulder_width_m:
        print(f"📏 Shoulder Width: {shoulder_width_m:.2f} m")
    else:
        print("⚠️ Shoulder width not detected.")

    if waist_width_m:
        print(f"📏 Waist Width: {waist_width_m:.2f} m")
    else:
        print("⚠️ Waist width not detected.")

    if height_m:
        print(f"📏 Height: {height_m:.2f} m")
    else:
        print("⚠️ Height not detected.")

    if stomach_back_m:
        print(f"📏 Stomach-to-Back: {stomach_back_m:.2f} m")
    else:
        print("⚠️ Stomach-to-back distance not detected.")

# Get original dimensions
height, width = front_image.shape[:2]

# Resize to a quarter of original dimensions
new_size = (width // 2, height // 2)

# Resize images
front_image_resized = cv2.resize(front_image, new_size)
side_image_resized = cv2.resize(side_image, new_size)

# # Display resized images
# cv2.imshow("Resized Front View", front_image_resized)
# cv2.imshow("Resized Side View", side_image_resized)
# cv2.waitKey(0)
# cv2.destroyAllWindows()

# Save measurements to JSON
measurement_data = {
    "gender": "male",
    "depth_estimated": depth_est,
    "shoulder_width_m": shoulder_width_m if shoulder_width_m else None,
    "waist_width_m": waist_width_m if waist_width_m else None,
    "height_m": height_m if height_m else None,
    "stomach_to_back_m": stomach_back_m if stomach_back_m else None
}

json_filename = "measurements.json"
with open(json_filename, "w") as json_file:
    json.dump(measurement_data, json_file, indent=4)

print(f"📂 Measurements saved to {json_filename}")

# Install and enable MPFB2 add-on
addon_path = "C:/Users/senir/Downloads/mpfb2-master.zip"
bpy.ops.preferences.addon_install(filepath=addon_path)
bpy.ops.preferences.addon_enable(module="bl_ext.user_default.mpfb")
bpy.ops.wm.save_userpref()
# import_path = r"C:\Users\senir\Desktop\Blender\Male.obj"
# bpy.ops.wm.obj_import(filepath=import_path)

# Load JSON data
json_path = r"/src/measurements.json"
with open(json_path, "r") as file:
    data = json.load(file)

print("📥 Loaded measurement data:", data)

bpy.ops.preferences.addon_enable(module="bl_ext.user_default.mpfb")
# Load JSON data
with open(r"/src/measurements.json", "r") as file:
    data = json.load(file)

print(data)

# Create a human in MakeHuman Blender Plugin
bpy.context.scene.MPFB_NH_phenotype_gender = data["gender"]
bpy.ops.mpfb.create_human()

if data["gender"] == "female":
    bpy.context.scene.mpfb_macropanel_cupsize = 0.76

# Define normalization values
max_height = 2.5
max_shoulder_width = 0.75
max_waist = 0.6

# Ensure data keys exist
if "height_m" in data and "waist_width_m" in data and "shoulder_width_m" in data:
    bpy.context.scene.mpfb_macropanel_height = data["height_m"] / max_height
    bpy.context.scene.hip_hip_scale_horiz_decr_incr = data["waist_width_m"] / max_waist
    bpy.context.scene.torso_measure_shoulder_dist_decr_incr = data["shoulder_width_m"] / max_shoulder_width
else:
    print("Error: Missing required keys in JSON file")

export_path = r"C:\Users\senir\Desktop\optimized_model.obj"
bpy.ops.wm.obj_export(filepath=export_path)