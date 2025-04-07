import json
import trimesh
import cv2


from face_analysis import estimate_distance_from_eyes
from pose_analysis import measure_front_view, measure_side_view

Gender = 'Male'
def setGender(gender):
    global Gender
    Gender = gender

def modelCreation():
    # Load images
    front_image = cv2.imread(r"uploads/front_view.jpg")
    side_image = cv2.imread(r"uploads/side_view.jpg")

    # Estimate depth from eyes
    depth_est = estimate_distance_from_eyes(front_image) or 2.0
    print(f"Estimated depth: {depth_est:.2f} m")

    # Perform measurements
    shoulder_m, waist_m = measure_front_view(front_image, depth_est)
    height_m, stomach_back_m = measure_side_view(side_image, depth_est)

    # Print measurement results
    print(f" Shoulder Width: {shoulder_m:.2f} m" if shoulder_m else " Shoulder width not detected.")
    print(f" Waist Width: {waist_m:.2f} m" if waist_m else " Waist width not detected.")
    print(f" Height: {height_m:.2f} m" if height_m else "️ Height not detected.")
    print(f" Stomach-to-Back: {stomach_back_m:.2f} m" if stomach_back_m else " Stomach-to-back not detected.")

    # Save measurements to JSON
    measurement_data = {
        "gender": Gender,
        "depth_estimated": depth_est,
        "shoulder_width_m": shoulder_m if shoulder_m else None,
        "waist_width_m": waist_m if waist_m else None,
        "height_m": height_m if height_m else None,
        "stomach_to_back_m": stomach_back_m if stomach_back_m else None
    }

    json_filename = "measurements.json"
    with open(json_filename, "w") as json_file:
        json.dump(measurement_data, json_file, indent=4)

    print(f" Measurements saved to {json_filename}")

    # Load JSON data
    json_path = "measurements.json"
    with open(json_path, 'r') as file:
        body_data = json.load(file)

        # Load the human model using Trimesh
        if body_data["gender"] == "Male":
            model_path = "DefaultModel/Male.obj"  # Use OBJ format
        else:
            model_path = "DefaultModel/Female.obj"

        try:
            mesh = trimesh.load_mesh(model_path)
            print(f" Successfully loaded {Gender}model: {model_path}")
        except Exception as e:
            print(f" Error loading model: {e}")
            return

        # Apply transformations based on measurements
        default_height = 1.75
        if body_data["height_m"]:
            if body_data["height_m"]>1:
                height_factor = body_data["height_m"] / default_height
            else:
                height_factor = 1.75
        else:
            height_factor = 1.75

        mesh.apply_scale(height_factor)

        # Export the adjusted model
        export_path = "exports/optimized_model.obj"
        mesh.export(export_path)
        print(f" {Gender}Model exported to {export_path}")

    print(" Model updated successfully based on JSON values.")
