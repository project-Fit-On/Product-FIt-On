using UnityEngine;
using UnityEngine.UI;

public class CameraPreview : MonoBehaviour
{
    public RawImage rawImage;       // Assign in Inspector
    private WebCamTexture webCamTex;

    void Start()
    {
        // Make sure we have at least one camera
        if (WebCamTexture.devices.Length > 0)
        {
            // Optionally, pick back camera vs front camera
            // e.g. var deviceName = WebCamTexture.devices[0].name;
            var deviceName = WebCamTexture.devices[0].name;

            // Create WebCamTexture
            webCamTex = new WebCamTexture(deviceName, 1920, 1080, 30);

            // Assign to RawImage
            rawImage.texture = webCamTex;

            // Start the camera
            webCamTex.Play();
        }
        else
        {
            Debug.LogError("No camera device found!");
        }
    }

    // You could stop or pause camera feed with
    // webCamTex.Stop() or webCamTex.Pause()
}
