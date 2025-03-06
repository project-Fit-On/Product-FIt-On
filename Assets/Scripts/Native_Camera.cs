using UnityEngine;
using UnityEngine.UI;       // for RawImage, if you want to preview
using System.Collections;   // for IEnumerator
using System.IO;

// Import the plugin’s namespace
using NativeCameraNamespace;

public class Native_Camera : MonoBehaviour
{
    public RawImage photoPreviewUI; // optional, if you want to preview the photo in a UI
    public int maxPhotoSize = 2048; // maximum dimension for the loaded Texture2D

    public void OnTakePhotoButtonClicked()
    {
        // Ask for permission & capture photo
        TakePhoto();
    }

    private void TakePhoto()
    {
        // Check permission first (optional)
        NativeCamera.Permission permission = NativeCamera.CheckPermission(true); ;
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

            // Do something with the texture:
            // e.g. display it in a RawImage
            if (photoPreviewUI != null)
            {
                photoPreviewUI.texture = texture;
                photoPreviewUI.gameObject.SetActive(true);
            }

            // If you want to save the texture somewhere in your app:
            //   File.Copy(path, yourDesiredLocation);
            // Or upload it to a server, etc.
        });
    }
}
