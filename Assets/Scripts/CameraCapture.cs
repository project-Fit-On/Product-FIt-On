using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class CameraCapture : MonoBehaviour
{
    [Header("UI")]
    public RawImage cameraPreview;  // Assign this in the Inspector (your RawImage)

    private WebCamTexture webCamTexture;

    void Start()
    {
        // Make sure the user’s device has a camera
        if (WebCamTexture.devices.Length > 0)
        {
            // Pick the first available camera (on phone you might pick devices[0] for the back camera, etc.)
            string cameraName = WebCamTexture.devices[0].name;
            webCamTexture = new WebCamTexture(cameraName);

            // Assign the camera texture to the RawImage
            cameraPreview.texture = webCamTexture;

            // Start camera feed
            webCamTexture.Play();
        }
        else
        {
            Debug.LogWarning("No camera detected on this device!");
        }

    }

    public void OnCapturePhotoButtonClick()
    {
        // 1. Actually capture the photo
        Texture2D captured = CapturePhoto();

        // 2. If successful, let’s do something with it—like saving it
        if (captured != null)
        {
            // Here is the point where you KNOW a photo is taken,
            // because CapturePhoto() has returned a valid texture.

            Debug.Log("Photo captured successfully!");

            // Optionally, do a save
            SavePhoto(captured);
        }
        else
        {
            Debug.LogWarning("Failed to capture photo!");
        }
    }

    // Example function to be called by your “Capture” button
    public Texture2D CapturePhoto()
    {
        // If camera not running, just exit
        if (webCamTexture == null || !webCamTexture.isPlaying)
            return null;

        // Create a new texture the same size as the WebCamTexture
        Texture2D capturedTexture = new Texture2D(webCamTexture.width, webCamTexture.height, TextureFormat.RGB24, false);

        // Copy the pixels from WebCamTexture into the new Texture2D
        capturedTexture.SetPixels(webCamTexture.GetPixels());
        capturedTexture.Apply();

        // For the moment (Phase 1), we just return this captured Texture2D.
        // We'll save it in Phase 2.
        return capturedTexture;
    }

    public void SavePhoto(Texture2D photo)
    {
        // Convert to PNG (or use EncodeToJPG if you prefer)
        byte[] imageBytes = photo.EncodeToPNG();

        // Generate a unique filename (e.g. Photo1.png / Photo2.png)
        // or you can use a timestamp if you prefer
        string fileName = "Photo1.png";

        // Build the full path
        string fullPath = Path.Combine(Application.persistentDataPath, fileName);

        // Write the bytes to the file
        File.WriteAllBytes(fullPath, imageBytes);

        Debug.Log("Saved photo to: " + fullPath);
    }
}
