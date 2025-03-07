import os
import shutil
import uvicorn
from fastapi import FastAPI, File, UploadFile, HTTPException, Form
import secrets
from pathlib import Path
from starlette.responses import FileResponse
from main import modelCreation
from main import setGender
from mangum import Mangum

# Create FastAPI instance
app = FastAPI()
handler = Mangum(app)

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
@app.post("/upload/frontView")
async def upload_front_images(
        front_view: UploadFile = File(...),
):
    print("POST request received on /upload")
    allowed_extensions = {".jpg", ".jpeg", ".png"}
    if not (front_view.filename.endswith(tuple(allowed_extensions))):
        return {"error": "Only JPG and PNG files are allowed"}
    front_path = UPLOAD_FOLDER / front_view.filename

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
    side_path = UPLOAD_FOLDER / side_view.filename
    with open(side_path, "wb") as buffer:
        shutil.copyfileobj(side_view.file, buffer)

    return {"message": "Images uploaded successfully","side_image": str(side_path)}

@app.post("/upload/gender")
async def upload_side_images(
        Gender: str = Form(...)):
    print("POST request received on /upload")
    setGender(Gender)
    return {"message": "Gender uploaded successfully","Gender": str(Gender)}



@app.get("/process")
def process_images():
     modelCreation()
     return {"message": "Model Created Succesfully"}

# Get exported 3D model
@app.get("/download")
def download_model():
    export_path = EXPORT_FOLDER / "optimized_model.fbx"
    if not export_path.exists():
        raise HTTPException(status_code=404, detail="3D model not found")

    return FileResponse(path=export_path, filename="optimized_model.fbx", media_type="application/octet-stream")



# Run the server
if __name__ == "__main__":
    port = int(os.environ.get("PORT", 8000))  # Railway assigns a dynamic port
    uvicorn.run(app, host="0.0.0.0", port=port)
