import cv2
import mediapipe as mp
import numpy as np
from camera_specs import FOCAL_LENGTH_X_PIXELS

mp_face_mesh = mp.solutions.face_mesh
face_mesh = mp_face_mesh.FaceMesh(
    static_image_mode=True, max_num_faces=1, refine_landmarks=True, min_detection_confidence=0.5
)

def detect_face_landmarks(image):
    """Detects eyes and returns their coordinates."""
    image_rgb = cv2.cvtColor(image, cv2.COLOR_BGR2RGB)
    results = face_mesh.process(image_rgb)

    if results.multi_face_landmarks:
        face_landmarks = results.multi_face_landmarks[0]
        left_eye = face_landmarks.landmark[33]
        right_eye = face_landmarks.landmark[263]

        h, w, _ = image.shape
        return {
            'left_eye': (int(left_eye.x * w), int(left_eye.y * h)),
            'right_eye': (int(right_eye.x * w), int(right_eye.y * h))
        }
    return None

def estimate_distance_from_eyes(image):
    """Estimates face-to-camera distance."""
    landmarks = detect_face_landmarks(image)
    if landmarks is None:
        return None

    left_eye_px, right_eye_px = landmarks['left_eye'], landmarks['right_eye']
    eye_distance_px = np.linalg.norm(np.array(left_eye_px) - np.array(right_eye_px))

    IPD_METERS = 0.063  # Average Interpupillary Distance
    return (IPD_METERS * FOCAL_LENGTH_X_PIXELS) / eye_distance_px
