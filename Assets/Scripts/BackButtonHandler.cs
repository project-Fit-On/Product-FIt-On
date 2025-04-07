using UnityEngine;

public class BackButtonHandler : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject scannerPanel;   // The active QR scanner UI
    public GameObject homePanel;      // The UI to go back to

    [Header("Managers & References")]
    public Transform clothParent;     // The parent holding all the cloths
    public QRCodeScanner qrScanner;   // Reference to the QR scanner (to stop camera)

    public void OnBackButtonClicked()
    {
        // 1. Stop camera feed
        if (qrScanner != null)
        {
            qrScanner.StopCamera();
        }

        // 2. Switch UI
        scannerPanel.SetActive(false);
        homePanel.SetActive(true);

        // 3. Deactivate all spawned cloths under the parent container
        foreach (Transform child in clothParent)
        {
            child.gameObject.SetActive(false);
        }

        Debug.Log("Returned to home. Deactivated all spawned cloths and stopped camera.");
    }
}
