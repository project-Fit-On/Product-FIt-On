import cv2
import mediapipe as mp
import numpy as np
from camera_specs import FOCAL_LENGTH_X_PIXELS, FOCAL_LENGTH_Y_PIXELS

mp_pose = mp.solutions.pose
pose = mp_pose.Pose()

def detect_keypoints(image):
    img_rgb = cv2.cvtColor(image, cv2.COLOR_BGR2RGB)
    return pose.process(img_rgb)

def get_landmark_coords(image, landmarks, idx):
    """Extracts pixel coordinates of a landmark."""
    h, w, _ = image.shape
    landmark = landmarks.landmark[idx]
    if landmark.visibility > 0.3:
        return int(landmark.x * w), int(landmark.y * h)
    return None

def pixel_to_meters(pixel_distance, focal_length_pixels, depth_meters):
    """Converts pixel distance to meters."""
    return (pixel_distance * depth_meters) / focal_length_pixels if pixel_distance else None

def measure_front_view(image, depth):
    """Measures shoulder and waist width."""
    results = detect_keypoints(image)
    if not results.pose_landmarks:
        return None, None

    landmarks = results.pose_landmarks
    left_shoulder = get_landmark_coords(image, landmarks, mp_pose.PoseLandmark.LEFT_SHOULDER)
    right_shoulder = get_landmark_coords(image, landmarks, mp_pose.PoseLandmark.RIGHT_SHOULDER)

    shoulder_m = None
    if left_shoulder and right_shoulder:
        shoulder_px = np.linalg.norm(np.array(left_shoulder) - np.array(right_shoulder))
        shoulder_m = pixel_to_meters(shoulder_px, FOCAL_LENGTH_X_PIXELS, depth)

    left_hip = get_landmark_coords(image, landmarks, mp_pose.PoseLandmark.LEFT_HIP)
    right_hip = get_landmark_coords(image, landmarks, mp_pose.PoseLandmark.RIGHT_HIP)

    waist_m = None
    if left_hip and right_hip:
        waist_px = np.linalg.norm(np.array(left_hip) - np.array(right_hip))
        waist_m = pixel_to_meters(waist_px, FOCAL_LENGTH_X_PIXELS, depth)

    return shoulder_m, waist_m

def measure_side_view(image, depth):
    """Measures height and stomach-to-back width."""
    results = detect_keypoints(image)
    if not results.pose_landmarks:
        return None, None

    landmarks = results.pose_landmarks
    left_ear = get_landmark_coords(image, landmarks, mp_pose.PoseLandmark.LEFT_EAR)
    right_ear = get_landmark_coords(image, landmarks, mp_pose.PoseLandmark.RIGHT_EAR)

    head_top = ((left_ear[0] + right_ear[0]) // 2, (left_ear[1] + right_ear[1]) // 2) if left_ear and right_ear else None
    foot_idx = get_landmark_coords(image, landmarks, mp_pose.PoseLandmark.RIGHT_FOOT_INDEX)

    height_m = None
    if head_top and foot_idx:
        height_px = np.linalg.norm(np.array(head_top) - np.array(foot_idx))
        height_m = pixel_to_meters(height_px, FOCAL_LENGTH_Y_PIXELS, depth)

    left_shoulder = get_landmark_coords(image, landmarks, mp_pose.PoseLandmark.LEFT_SHOULDER)
    right_shoulder = get_landmark_coords(image, landmarks, mp_pose.PoseLandmark.RIGHT_SHOULDER)
    left_hip = get_landmark_coords(image, landmarks, mp_pose.PoseLandmark.LEFT_HIP)
    right_hip = get_landmark_coords(image, landmarks, mp_pose.PoseLandmark.RIGHT_HIP)

    stomach_back_m = None
    if left_shoulder and right_shoulder and left_hip and right_hip:
        shoulder_mid = ((left_shoulder[0] + right_shoulder[0]) // 2, (left_shoulder[1] + right_shoulder[1]) // 2)
        hip_mid = ((left_hip[0] + right_hip[0]) // 2, (left_hip[1] + right_hip[1]) // 2)
        thickness_px = np.linalg.norm(np.array(hip_mid) - np.array(shoulder_mid))
        stomach_back_m = pixel_to_meters(thickness_px, FOCAL_LENGTH_X_PIXELS, depth)

    return height_m, stomach_back_m
