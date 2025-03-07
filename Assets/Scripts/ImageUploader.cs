using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class ImageUploader : MonoBehaviour
{
    // API Endpoint
    private string uploadUrl = "https://my-vercel-api.vercel.app/api/uploadPhoto";

    // Start the upload process
    public void UploadImages()
    {
        StartCoroutine(UploadPhoto("image1.jpg"));
        //StartCoroutine(UploadPhoto("image2.jpg"));
    }

    IEnumerator UploadPhoto(string fileName)
    {
        // Get the full file path in persistentDataPath
        string filePath = Path.Combine(Application.persistentDataPath, fileName);

        // Check if file exists before proceeding
        if (!File.Exists(filePath))
        {
            Debug.LogError("File not found: " + filePath);
            yield break; // Stop coroutine if file doesn't exist
        }
        else
        {
            Debug.Log("File Found");
        }

        // Read the file into a byte array
        byte[] fileData = File.ReadAllBytes(filePath);

        // Create form data for multipart upload
        WWWForm form = new WWWForm();
        form.AddBinaryData("file", fileData, fileName, "image/jpeg");

        // Create a UnityWebRequest POST request
        using (UnityWebRequest request = UnityWebRequest.Post(uploadUrl, form))
        {
            // Send the request
            yield return request.SendWebRequest();

            // Check for errors
            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Upload failed: " + request.error);
            }
            else
            {
                Debug.Log("Upload success: " + request.downloadHandler.text);
            }
        }
    }
}
