import json

import bpy
import cv2
from face_analysis import estimate_distance_from_eyes
from pose_analysis import measure_front_view, measure_side_view


# Load images
gender = "male"
front_image = cv2.imread("front_view.jpg")
side_image = cv2.imread("side_view.jpg")

# Estimate depth from eyes
depth_est = estimate_distance_from_eyes(front_image) or 2.0
print(f"Estimated depth: {depth_est:.2f} m")

# Perform measurements
shoulder_m, waist_m = measure_front_view(front_image, depth_est)
height_m, stomach_back_m = measure_side_view(side_image, depth_est)

# Print measurement results
print(f"📏 Shoulder Width: {shoulder_m:.2f} m" if shoulder_m else "⚠️ Shoulder width not detected.")
print(f"📏 Waist Width: {waist_m:.2f} m" if waist_m else "⚠️ Waist width not detected.")
print(f"📏 Height: {height_m:.2f} m" if height_m else "⚠️ Height not detected.")
print(f"📏 Stomach-to-Back: {stomach_back_m:.2f} m" if stomach_back_m else "⚠️ Stomach-to-back not detected.")

# Save measurements to JSON
measurement_data = {
    "gender": gender,
    "depth_estimated": depth_est,
    "shoulder_width_m": shoulder_m if shoulder_m else None,
    "waist_width_m": waist_m if waist_m else None,
    "height_m": height_m if height_m else None,
    "stomach_to_back_m": stomach_back_m if stomach_back_m else None
}

json_filename = "measurements.json"
with open(json_filename, "w") as json_file:
    json.dump(measurement_data, json_file, indent=4)

print(f"📂 Measurements saved to {json_filename}")

