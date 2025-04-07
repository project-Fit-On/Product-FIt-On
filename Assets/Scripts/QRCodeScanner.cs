using UnityEngine;
using UnityEngine.UI;
using ZXing;
using ZXing.Common;
using System.Collections.Generic;

public class QRCodeScanner : MonoBehaviour
{
    private WebCamTexture webCamTexture;
    private IBarcodeReader barcodeReader;

    [Header("UI References")]
    public RawImage rawImage;
    public RawImage rawImageParent;   // the camera feed
    public GameObject backgroundPanel; // panel with the background image

    [Header("Cloth Spawn")]
    public Transform clothSpawnPoint; // where you want the cloth to appear

    [Header("Cloth Management")]
    public Transform clothParent; // Assign this to an empty GameObject in the scene

    public float clothScaleFactor = 2f; // how big you want it

    // Dictionary: QR code text -> path in Resources
    private Dictionary<string, string> clothMap = new Dictionary<string, string>
    {
        { "8822443311", "ClothModels/8822443311/8822443311" },
        {"886754321","ClothModels/886754321/886754321"}
    };

    void Start()
    {
        // Initialize barcode reader
        barcodeReader = new BarcodeReader
        {
            AutoRotate = true,
            Options = new DecodingOptions { TryHarder = true }
        };

        // Start the camera feed
        WebCamDevice[] devices = WebCamTexture.devices;
        if (devices.Length > 0)
        {
            webCamTexture = new WebCamTexture(devices[0].name, Screen.width, Screen.height);
            webCamTexture.Play();
            rawImage.texture = webCamTexture;
        }

        // Ensure background panel is hidden initially
        if (backgroundPanel) backgroundPanel.SetActive(false);
    }

    void Update()
    {
        if (webCamTexture != null && rawImage != null)
        {
            // 1) Read the rotation angle from the camera
            int rotation = webCamTexture.videoRotationAngle;

            // 2) Apply a rotation to the RawImage rect transform
            rawImage.rectTransform.localEulerAngles = new Vector3(0, 0, -rotation);

            // 3) Check if the image is vertically flipped
            if (webCamTexture.videoVerticallyMirrored)
            {
                // Flip the UV rect on the Y-axis
                rawImage.uvRect = new Rect(0, 1, 1, -1);
            }
            else
            {
                // Reset to normal
                rawImage.uvRect = new Rect(0, 0, 1, 1);
            }
        }
        if (webCamTexture != null && webCamTexture.isPlaying)
        {
            try
            {
                Color32[] cameraFrame = webCamTexture.GetPixels32();
                var result = barcodeReader.Decode(cameraFrame, webCamTexture.width, webCamTexture.height);
                if (result != null)
                {
                    Debug.Log("QR Code Detected: " + result.Text);

                    // Stop scanning + show cloth
                    webCamTexture.Stop();
                    rawImage.gameObject.SetActive(false);
                    rawImageParent.gameObject.SetActive(false);

                    // Show background panel
                    if (backgroundPanel) backgroundPanel.SetActive(true);

                    // Now spawn the cloth
                    SpawnCloth(result.Text);
                }
            }
            catch (System.Exception ex)
            {
                Debug.LogWarning("QR decode error: " + ex.Message);
            }
        }
    }

    public void StartCamera()
    {
        if (webCamTexture == null)
        {
            WebCamDevice[] devices = WebCamTexture.devices;
            if (devices.Length > 0)
            {
                webCamTexture = new WebCamTexture(devices[0].name, Screen.width, Screen.height);
            }
        }

        if (webCamTexture != null && !webCamTexture.isPlaying)
        {
            webCamTexture.Play();
            rawImage.texture = webCamTexture;

            rawImage.gameObject.SetActive(true);
            rawImageParent.gameObject.SetActive(true);
        }
    }

    public void StopCamera()
    {

        if (webCamTexture != null)
        {
            if (webCamTexture.isPlaying)
            {
                webCamTexture.Stop();
                Debug.Log("Camera stopped.");
            }

            // Clear the RawImage to release any frame buffer
            if (rawImage != null)
            {
                rawImage.texture = null;
            }

            // Nullify and destroy the WebCamTexture completely (important on macOS)
            webCamTexture = null;
            Resources.UnloadUnusedAssets(); // Optional: prompt Unity to GC unused stuff
        }

        if (rawImage.gameObject.activeSelf)
            rawImage.gameObject.SetActive(false);

        if (rawImageParent.gameObject.activeSelf)
            rawImageParent.gameObject.SetActive(false);

    }

    private void SpawnCloth(string serial)
    {
        if (!clothMap.ContainsKey(serial))
        {
            Debug.LogWarning("No cloth mapping for serial: " + serial);
            return;
        }

        // Load the prefab from Resources
        string path = clothMap[serial];
        GameObject clothPrefab = Resources.Load<GameObject>(path);
        if (clothPrefab == null)
        {
            Debug.LogWarning("Could not load prefab from path: " + path);
            return;
        }

        // Spawn the cloth at the spawn point, parented under clothParent
        GameObject spawned = Instantiate(clothPrefab, clothSpawnPoint.position, clothSpawnPoint.rotation);

        // Parent it under clothParent if it's set
        if (clothParent != null)
        {
            spawned.transform.SetParent(clothParent);
        }

        // Scale it up
        spawned.transform.localScale *= clothScaleFactor;

        Debug.Log("Spawned cloth: " + serial);
    }
}
