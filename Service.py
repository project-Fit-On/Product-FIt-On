import os
import json
import shutil
import uvicorn
import bpy
import cv2
from fastapi import FastAPI, File, UploadFile, HTTPException, Depends
from pydantic import BaseModel
from typing import Optional
import secrets
from pathlib import Path

from starlette.responses import FileResponse

from face_analysis import estimate_distance_from_eyes
from pose_analysis import measure_front_view, measure_side_view

# Create FastAPI instance
app = FastAPI()

# Store API keys (In production, store securely in a database or environment variable)
API_KEYS = {"user1": secrets.token_hex(16)}

# Directory paths
BASE_DIR = Path(__file__).resolve().parent  # Get the current script directory
UPLOAD_FOLDER = BASE_DIR / "uploads"
EXPORT_FOLDER = BASE_DIR / "exports"

# Ensure directories exist
UPLOAD_FOLDER.mkdir(parents=True, exist_ok=True)
EXPORT_FOLDER.mkdir(parents=True, exist_ok=True)


# API Key Dependency
def verify_api_key(api_key: str):
    if api_key not in API_KEYS.values():
        raise HTTPException(status_code=403, detail="Invalid API Key")
    return api_key


# Upload images endpoint
@app.post("/upload")
def upload_images(
        front_view: UploadFile = File(...),
        side_view: UploadFile = File(...),
        api_key: str = Depends(verify_api_key)
):
    # Save images in the `uploads/` directory
    front_path = UPLOAD_FOLDER / front_view.filename
    side_path = UPLOAD_FOLDER / side_view.filename

    with open(front_path, "wb") as buffer:
        shutil.copyfileobj(front_view.file, buffer)
    with open(side_path, "wb") as buffer:
        shutil.copyfileobj(side_view.file, buffer)

    return {"message": "Images uploaded successfully", "front_image": str(front_path), "side_image": str(side_path)}


# Process images and generate 3D model
@app.post("/process")
def process_images(
        front_view: UploadFile = File(...),
        side_view: UploadFile = File(...),
        gender: str = "male",
        api_key: str = Depends(verify_api_key)
):
    front_path = Path(UPLOAD_FOLDER) / front_view.filename
    side_path = Path(UPLOAD_FOLDER) / side_view.filename

    with open(front_path, "wb") as buffer:
        shutil.copyfileobj(front_view.file, buffer)
    with open(side_path, "wb") as buffer:
        shutil.copyfileobj(side_view.file, buffer)

    # Load images and estimate depth
    front_image = cv2.imread(str(front_path))
    side_image = cv2.imread(str(side_path))
    depth_est = estimate_distance_from_eyes(front_image) or 2.0

    # Perform measurements
    shoulder_m, waist_m = measure_front_view(front_image, depth_est)
    height_m, stomach_back_m = measure_side_view(side_image, depth_est)

    # Save measurements to JSON
    measurement_data = {
        "gender": gender,
        "depth_estimated": depth_est,
        "shoulder_width_m": shoulder_m if shoulder_m else None,
        "waist_width_m": waist_m if waist_m else None,
        "height_m": height_m if height_m else None,
        "stomach_to_back_m": stomach_back_m if stomach_back_m else None
    }
    json_filename = Path(EXPORT_FOLDER) / "measurements.json"
    with open(json_filename, "w") as json_file:
        json.dump(measurement_data, json_file, indent=4)

    # Blender Processing
    if "Cube" in bpy.data.objects:
        cube = bpy.data.objects["Cube"]
        bpy.data.objects.remove(cube, do_unlink=True)


    import_path = (Path("C:/Users/senir/Desktop/Blender/Male.fbx") if gender == "male"
                   else Path("C:/Users/senir/Desktop/Blender/Female.fbx"))
    bpy.ops.import_scene.fbx(filepath=str(import_path))
    obj = bpy.data.objects.get("Human")

    if obj:
        bpy.context.view_layer.objects.active = obj
        obj.select_set(True)

        height_factor = measurement_data["height_m"] / 1.75
        obj.scale.x *= height_factor
        obj.scale.y *= height_factor
        obj.scale.z *= height_factor

        bpy.ops.object.mode_set(mode='OBJECT')
        export_path = Path(EXPORT_FOLDER) / "optimized_model.obj"
        bpy.ops.wm.obj_export(filepath=str(export_path))
        return {"message": "Processing complete", "model_path": str(export_path)}

    raise HTTPException(status_code=500, detail="Blender processing failed")

# Get exported 3D model
@app.get("/download")
def download_model(api_key: str = Depends(verify_api_key)):
    export_path = EXPORT_FOLDER / "optimized_model.obj"
    bpy.ops.wm.obj_export(filepath=str(export_path))
    if not export_path.exists():
        raise HTTPException(status_code=404, detail="3D model not found")

    return FileResponse(path=export_path, filename="optimized_model.fbx", media_type="application/octet-stream")



# Run the server
if __name__ == "__main__":
    uvicorn.run(app, host="127.0.0.1", port=8000)
