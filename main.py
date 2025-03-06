import json
import bpy
import cv2
from face_analysis import estimate_distance_from_eyes
from pose_analysis import measure_front_view, measure_side_view

Gender = 'male'
def setGender(gender):
    global Gender
    Gender = gender

def modelCreation():
    # Load images

    front_image = cv2.imread("front_view.jpg")
    side_image = cv2.imread("side_view.jpg")

    # Estimate depth from eyes
    depth_est = estimate_distance_from_eyes(front_image) or 2.0
    print(f"Estimated depth: {depth_est:.2f} m")

    # Perform measurements
    shoulder_m, waist_m = measure_front_view(front_image, depth_est)
    height_m, stomach_back_m = measure_side_view(side_image, depth_est)

    # Print measurement results
    print(f"📏 Shoulder Width: {shoulder_m:.2f} m" if shoulder_m else " Shoulder width not detected.")
    print(f"📏 Waist Width: {waist_m:.2f} m" if waist_m else " Waist width not detected.")
    print(f"📏 Height: {height_m:.2f} m" if height_m else "️ Height not detected.")
    print(f"📏 Stomach-to-Back: {stomach_back_m:.2f} m" if stomach_back_m else " Stomach-to-back not detected.")

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
    json_path = r"C:\Users\senir\Desktop\Product-FIt-On-imageProcessing\measurements.json"
    with open(json_path, 'r') as file:
        body_data = json.load(file)

    # Get the human model
    # Check if "Cube" exists in the scene
    if "Cube" in bpy.data.objects:
        cube = bpy.data.objects["Cube"]
        bpy.data.objects.remove(cube, do_unlink=True)
        print("Deleted the Cube!")
    else:
        print("Cube not found!")

    for obj in bpy.data.objects:
        if obj.type in {'CAMERA', 'LIGHT'}:
            bpy.data.objects.remove(obj, do_unlink=True)


    if body_data["gender"] == "male":
        import_path = r"C:\Users\senir\Desktop\Product-FIt-On-imageProcessing\DefaultModel\Male.fbx"
        bpy.ops.import_scene.fbx(filepath=import_path)
    elif body_data["gender"] == "female":
        import_path = r"C:\Users\senir\Desktop\Product-FIt-On-imageProcessing\DefaultModel\Female.fbx"
        bpy.ops.import_scene.fbx(filepath=import_path)

    obj = bpy.data.objects.get("Human")  # Ensure the model name matches the one in Blender
    if obj:
        bpy.context.view_layer.objects.active = obj
        obj.select_set(True)

        # Access the vertex groups
        # vgroups = obj.vertex_groups

        # Height Adjustment (Scaling Along Z-axis)
        height_factor = body_data["height_m"] / 1.75  # Assuming default model height is ~1.7m
        obj.scale.x *= height_factor   # Scale width-wise
        obj.scale.y *= height_factor   # Scale depth-wise
        obj.scale.z *= height_factor

        # # Shoulder Width Adjustment (Scaling Along X-axis)
        # shoulder_factor = body_data["shoulder_width_m"] / 0.35  # Assuming default shoulder width ~0.35m
        # if "shoulder01.L" in vgroups and "shoulder01.R" in vgroups:
        #     bpy.ops.transform.resize(value=(shoulder_factor, 1, 1))
        #
        # # Waist Width Adjustment (Scaling Along X-axis)
        # waist_factor = body_data["waist_width_m"] / 0.2  # Assuming default waist width ~0.2m
        # if "spine01" in vgroups:
        #     bpy.ops.transform.resize(value=(waist_factor, 1, 1))
        #
        # # Stomach Depth Adjustment (Scaling Alosng Y-axis)
        # depth_factor = body_data["stomach_to_back_m"] / 0.5  # Assuming default depth ~0.5m
        # if "spine01" in vgroups:
        #     bpy.ops.transform.resize(value=(1, depth_factor, 1))

        # Export the adjusted model
        export_path = r"C:\Users\senir\Desktop\Product-FIt-On-imageProcessing\exports\optimized_model.fbx"
        bpy.ops.export_scene.fbx(filepath=export_path)
        print(f"Model exported to {export_path}")

        print("Model updated successfully based on JSON values.")
    else:
        print("Error: Human model not found in Blender.")

