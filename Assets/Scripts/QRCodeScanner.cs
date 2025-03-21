using UnityEngine;
using ZXing; // from the ZXing library
using ZXing.Common;
using UnityEngine.UI;
using System.Collections.Generic;

public class QRCodeScanner : MonoBehaviour
{
    private WebCamTexture webCamTexture;
    private IBarcodeReader barcodeReader;
    public RawImage rawImage;

    // 1) Dictionary: QR code -> resource path
    //    (Here, you assume you have prefab/.obj in "Assets/Resources/ClothModels")
    private Dictionary<string, string> clothMap = new Dictionary<string, string>
    {
        { "8822443311", "ClothModels/8822443311/8822443311" },
        {"886754321","ClothModels/886754321/886754321"}
        // etc...
    };

    void Start()
    {
        // 2) Initialize the barcode reader
        barcodeReader = new BarcodeReader
        {
            AutoRotate = true,
            Options = new DecodingOptions
            {
                TryHarder = true
            }
        };

        // 3) Find a suitable camera device
        WebCamDevice[] devices = WebCamTexture.devices;
        if (devices != null && devices.Length > 0)
        {
            // pick the first camera
            string cameraName = devices[0].name;

            // 4) Start the camera
            webCamTexture = new WebCamTexture(cameraName, Screen.width, Screen.height);
            webCamTexture.Play();

            // Show the camera feed on the RawImage
            rawImage.texture = webCamTexture;
        }
    }

    void Update()
    {
        if (webCamTexture != null && webCamTexture.isPlaying)
        {
            // Try decoding the current frame
            try
            {
                Color32[] cameraFrame = webCamTexture.GetPixels32();
                var result = barcodeReader.Decode(cameraFrame, webCamTexture.width, webCamTexture.height);

                if (result != null)
                {
                    Debug.Log("QR Code Detected: " + result.Text);

                    // 5) Use the decoded text to look up and spawn the cloth
                    OnQRCodeDetected(result.Text);

                    // If you only want to scan once, stop the camera
                    webCamTexture.Stop();
                }
            }
            catch (System.Exception ex)
            {
                Debug.LogWarning("QR decode error: " + ex.Message);
            }
        }
    }

    private void OnQRCodeDetected(string qrSerial)
    {
        // Look up if there's a matching cloth resource path
        if (clothMap.ContainsKey(qrSerial))
        {
            string resourcePath = clothMap[qrSerial];

            // Load the model or prefab from Resources
            // (Ensure you have something like "Assets/Resources/ClothModels/884231234567.prefab")
            GameObject clothPrefab = Resources.Load<GameObject>(resourcePath);

            if (clothPrefab != null)
            {
                // Spawn at Vector3.zero or in front of camera
                Camera mainCam = Camera.main;
                if (mainCam != null)
                {
                    // Example: 2 units in front of camera
                    Vector3 positionInFront = mainCam.transform.position + mainCam.transform.forward * 2f;
                    GameObject spawnedCloth = Instantiate(clothPrefab, positionInFront, Quaternion.identity);

                    // Optionally rotate it to face the camera
                    spawnedCloth.transform.LookAt(mainCam.transform);

                    Debug.Log("Spawned cloth for serial: " + qrSerial);
                }
                else
                {
                    // Fallback if there's no main camera
                    Instantiate(clothPrefab, Vector3.zero, Quaternion.identity);
                }
            }
            else
            {
                Debug.LogWarning("Could not load prefab from path: " + resourcePath);
            }
        }
        else
        {
            Debug.Log("No matching cloth found for serial: " + qrSerial);
        }
    }
}
