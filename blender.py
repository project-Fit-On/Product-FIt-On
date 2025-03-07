# import bpy
# import json
# import pyautogui
#
# add_on_path = r"C:\Users\senir\Downloads\mpfb2-master.zip"
#
# with open(r"measurements.json", "r") as file:
#     data = json.load(file)
#
# print(data)
#
# # Create a human in MakeHuman Blender Plugin
# bpy.context.scene.MPFB_NH_phenotype_gender = data["gender"]
# bpy.ops.mpfb.create_human()
#
# if data["gender"] == "female":
#     bpy.context.scene.mpfb_macropanel_cupsize = 0.76
#
# # Define normalization values
# max_height = 2.5
# max_shoulder_width = 0.75
# max_waist = 0.6
#
# # Ensure data keys exist
# if "height_m" in data and "waist_width_m" in data and "shoulder_width_m" in data:
#     bpy.context.scene.mpfb_macropanel_height = data["height_m"] / max_height
#     bpy.context.scene.hip_hip_scale_horiz_decr_incr = data["waist_width_m"] / max_waist
#     bpy.context.scene.torso_measure_shoulder_dist_decr_incr = data["shoulder_width_m"] / max_shoulder_width
# else:
#     print("Error: Missing required keys in JSON file")
#
# export_path = r"C:\Users\senir\Desktop\optimized_model.obj"
# bpy.ops.wm.obj_export(filepath=export_path)

