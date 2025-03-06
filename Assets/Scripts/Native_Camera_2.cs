using UnityEngine;
using UnityEngine.UI;       // for RawImage, if you want to preview
using System.Collections;   // for IEnumerator
using System.IO;

// Import the plugin’s namespace
using NativeCameraNamespace;

public class Native_Camera2 : MonoBehaviour
{
    public RawImage photoPreviewUI; // optional, if you want to preview the photo in a UI
    public int maxPhotoSize = 2048; // maximum dimension for the loaded Texture2D

    // We'll track how many photos have been taken so we can name them image1, image2, etc.
    private int photoCount = 1;

    public void OnTakePhotoButtonClicked()
    {
        // Ask for permission & capture photo
        TakePhoto();
    }

    private void TakePhoto()
    {
        // Check permission first (optional)
        NativeCamera.Permission permission = NativeCamera.CheckPermission(true);
        if (permission != NativeCamera.Permission.Granted)
        {
            // Request permission
            permission = NativeCamera.RequestPermission(true);
            // If user doesn't grant permission, you won't proceed
            if (permission != NativeCamera.Permission.Granted)
            {
                Debug.LogWarning("User did not grant camera permission!");
                return;
            }
        }

        // Actually capture the photo
        NativeCamera.TakePicture((path) =>
        {
            // This callback will be executed after the user takes a photo or cancels
            Debug.Log("Photo path: " + path);
            if (path == null)
            {
                Debug.LogWarning("User cancelled taking a photo.");
                return;
            }

            // Optionally, load the photo into a Texture2D
            Texture2D texture = NativeCamera.LoadImageAtPath(path, maxPhotoSize);
            if (texture == null)
            {
                Debug.LogWarning("Failed to load the captured image!");
                return;
            }

            // Do something with the texture: (e.g. display it in a RawImage)
            if (photoPreviewUI != null)
            {
                photoPreviewUI.texture = texture;
                photoPreviewUI.gameObject.SetActive(true);
            }

            // Increase the photo count so we know whether it's image1 or image2
            photoCount++;

            // Decide on a filename (image1.png for the first capture, image2.png for the second, etc.)
            // If you only ever take 2 photos total, you can just do:
            string newFileName = "image" + photoCount + ".png";

            // Build the destination path inside the app's persistent folder
            string destinationPath = Path.Combine(Application.persistentDataPath, newFileName);

            // Copy from the plugin's temp path to our permanent path, overwriting if needed
            File.Copy(path, destinationPath, overwrite: true);

            Debug.Log("Saved photo permanently to: " + destinationPath);
            Debug.Log("Persistent data path is: " + Application.persistentDataPath);

        }, maxSize: 4096); // optional larger max size
    }
}
