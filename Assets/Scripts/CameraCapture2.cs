using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;

public class CameraCapture2 : MonoBehaviour
{
    public RawImage cameraPreview;
    public GameObject previewPanel;
    public RawImage capturedPhotoImage;

    private WebCamTexture webCamTexture;

    void Start()
    {
        // Hide the preview panel initially
        if (previewPanel != null)
            previewPanel.SetActive(false);

        StartCoroutine(InitializeCamera());
    }

    IEnumerator InitializeCamera()
    {
        if (WebCamTexture.devices.Length == 0)
        {
            Debug.LogWarning("No camera detected!");
            yield break;
        }

        // For iPhone back camera, you might pick something like:
        // var device = WebCamTexture.devices.FirstOrDefault(d => !d.isFrontFacing);
        // Or use devices[0] if that's actually your back camera
        string cameraName = WebCamTexture.devices[0].name;
        webCamTexture = new WebCamTexture(cameraName);

        cameraPreview.texture = webCamTexture;
        webCamTexture.Play();

        // Wait until the camera is actually producing frames
        while (webCamTexture.width <= 16)
        {
            // This yields 1 frame each time, preventing a tight loop
            yield return null;
        }

        // Now the camera feed is actually live and valid

        // Fix rotation (optional but recommended)
        AdjustPreviewTransform();
    }

    private void AdjustPreviewTransform()
    {
        if (webCamTexture == null) return;

        int rotation = webCamTexture.videoRotationAngle;
        bool mirrored = webCamTexture.videoVerticallyMirrored;

        // Rotate the RawImage
        cameraPreview.rectTransform.localEulerAngles = new Vector3(0, 0, -rotation);

        // Flip if needed
        Vector3 scale = Vector3.one;
        if (mirrored) scale.y = -1;
        cameraPreview.rectTransform.localScale = scale;
    }

    public void OnCapturePhotoButtonClick()
    {
        Texture2D captured = CapturePhoto();
        if (captured != null)
        {
            Debug.Log("Photo captured successfully!");
            SavePhoto(captured);

            if (capturedPhotoImage != null)
                capturedPhotoImage.texture = captured;

            if (previewPanel != null)
                previewPanel.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Failed to capture photo!");
        }
    }

    private Texture2D CapturePhoto()
    {
        if (webCamTexture == null || !webCamTexture.isPlaying)
        {
            Debug.LogWarning("Camera is not active!");
            return null;
        }

        // Make sure there's at least some resolution
        if (webCamTexture.width <= 16)
        {
            Debug.LogWarning("Camera hasn't fully started yet!");
            return null;
        }

        Texture2D capturedTexture = new Texture2D(
            webCamTexture.width,
            webCamTexture.height,
            TextureFormat.RGB24,
            false
        );
        capturedTexture.SetPixels(webCamTexture.GetPixels());
        capturedTexture.Apply();

        return capturedTexture;
    }

    private void SavePhoto(Texture2D photo)
    {
        string fileName = "Photo2.png";
        byte[] imageBytes = photo.EncodeToPNG();
        string path = Path.Combine(Application.persistentDataPath, fileName);

        File.WriteAllBytes(path, imageBytes);
        Debug.Log("Saved photo to: " + path);
    }

    public void OnClosePreview()
    {
        if (previewPanel != null)
            previewPanel.SetActive(false);
    }
    public void StopCamera()
    {
        if (webCamTexture != null && webCamTexture.isPlaying)
        {
            webCamTexture.Stop();
            webCamTexture = null;
            cameraPreview.texture = null;  // Clear the RawImage
            Debug.Log("Camera stopped.");
        }
    }
}
