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

        // Instantiate bigger and place it at the clothSpawnPoint
        GameObject spawned = Instantiate(clothPrefab);

        // If you have a dedicated transform to position it:
        if (clothSpawnPoint)
        {
            spawned.transform.position = clothSpawnPoint.position;
            spawned.transform.rotation = clothSpawnPoint.rotation;
        }
        else
        {
            // fallback if no clothSpawnPoint is set
            spawned.transform.position = new Vector3(0f, 0f, 1f);
        }

        // Make it bigger
        spawned.transform.localScale = spawned.transform.localScale * clothScaleFactor;

        Debug.Log("Spawned cloth: " + serial);
    }
}
