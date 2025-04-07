import os
import shutil
import uvicorn
from fastapi import FastAPI, File, UploadFile, HTTPException, Form, Body
from pathlib import Path
from starlette.responses import FileResponse
from ModelCreation import modelCreation
from ModelCreation import setGender
from mangum import Mangum
from pydantic import BaseModel

# Create FastAPI instance
app = FastAPI()
handler = Mangum(app)

# Directory paths
BASE_DIR = Path(__file__).resolve().parent  # Get the current script directory
UPLOAD_FOLDER = BASE_DIR / "uploads"
EXPORT_FOLDER = BASE_DIR / "exports"


# Ensure directories exist
UPLOAD_FOLDER.mkdir(parents=True, exist_ok=True)
EXPORT_FOLDER.mkdir(parents=True, exist_ok=True)

# Upload images endpoint
@app.post("/upload/frontView")
async def upload_front_images(
        front_view: UploadFile = File(...),
):
    print("POST request received on /upload")
    allowed_extensions = {".jpg", ".jpeg", ".png"}
    if not (front_view.filename.endswith(tuple(allowed_extensions))):
        return {"error": "Only JPG and PNG files are allowed"}
    front_path = UPLOAD_FOLDER / "front_view.jpg"

    with open(front_path, "wb") as buffer:
        shutil.copyfileobj(front_view.file, buffer)
    return {"message": "Images uploaded successfully", "front_image": str(front_path)}

@app.post("/upload/sideView")
async def upload_side_images(
        side_view: UploadFile = File(...)):
    print("POST request received on /upload")
    allowed_extensions = {".jpg", ".jpeg", ".png"}
    if not ( side_view.filename.endswith(tuple(allowed_extensions))):
        return {"error": "Only JPG and PNG files are allowed"}
    side_path = UPLOAD_FOLDER / "side_view.jpg"
    with open(side_path, "wb") as buffer:
        shutil.copyfileobj(side_view.file, buffer)

    return {"message": "Images uploaded successfully","side_image": str(side_path)}


class GenderPayload(BaseModel):
    gender: str  # Lowercase to match JSON key

@app.post("/upload/gender")
async def upload_gender(payload: GenderPayload):
    print("POST request received on /upload")
    print(f'Gender : {payload.gender}')
    setGender(payload.gender)
    return {"message": "Gender uploaded successfully", "Gender": payload.gender}



@app.get("/process")
def process_images():
     modelCreation()
     return {"message": "Model Created Succesfully"}

# Get exported 3D model
@app.get("/download")
def download_model():
    export_path = EXPORT_FOLDER / "optimized_model.obj"
    if not export_path.exists():
        raise HTTPException(status_code=404, detail="3D model not found")

    return FileResponse(path=export_path, filename="optimized_model.obj", media_type="application/octet-stream")

if __name__ == "__main__":
    port = int(os.getenv("PORT", 8080))  # Cloud Run provides PORT automatically
    uvicorn.run(app, host="0.0.0.0", port=port)


