using UnityEngine;
using ZXing; // from the ZXing library
using ZXing.Common;

public class QRCodeScanner : MonoBehaviour
{
    private WebCamTexture webCamTexture;
    private IBarcodeReader barcodeReader;
    public UnityEngine.UI.RawImage rawImage;

    void Start()
    {
        // 1. Initialize the barcode reader
        rawImage.texture = webCamTexture;

        barcodeReader = new BarcodeReader
        {
            // You can configure options if needed
            AutoRotate = true,
            Options = new DecodingOptions
            {
                TryHarder = true
            }
        };

        // 2. Find a suitable camera device
        WebCamDevice[] devices = WebCamTexture.devices;
        if (devices != null && devices.Length > 0)
        {
            // pick the first camera
            string cameraName = devices[0].name;

            // 3. Start the camera
            webCamTexture = new WebCamTexture(cameraName, Screen.width, Screen.height);
            webCamTexture.Play();
        }
    }

    void Update()
    {
        if (webCamTexture != null && webCamTexture.isPlaying)
        {
            // Try decoding the current frame
            try
            {
                // 4. Create a Color32 array from the WebCamTexture
                Color32[] cameraFrame = webCamTexture.GetPixels32();

                // 5. Decode
                var result = barcodeReader.Decode(cameraFrame, webCamTexture.width, webCamTexture.height);
                if (result != null)
                {
                    // We have a QR code!
                    Debug.Log("QR Code Detected: " + result.Text);
                    // For performance, you might stop scanning after detection or handle once per second, etc.

                    // OPTIONAL: If you only want to detect once, you can do:
                    // webCamTexture.Stop();
                }
            }
            catch (System.Exception ex)
            {
                Debug.LogWarning("QR decode error: " + ex.Message);
            }
        }
    }
}
