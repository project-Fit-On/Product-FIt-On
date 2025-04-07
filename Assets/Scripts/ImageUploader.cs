using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class ImageUploader : MonoBehaviour
{
    private string uploadUrl = "https://fit-on-3d-115648783380.us-central1.run.app/upload/frontView";
    private string uploadUrlsideview = "https://fit-on-3d-115648783380.us-central1.run.app/upload/sideView";

    public void UploadImages()
    {
        StartCoroutine(UploadPhoto("image1.jpg"));
        StartCoroutine(UploadPhoto2("image2.jpg"));

    }

    IEnumerator UploadPhoto(string fileName)
    {
        string filePath = System.IO.Path.Combine(Application.persistentDataPath, fileName);

        if (!System.IO.File.Exists(filePath))
        {
            Debug.LogError("File not found: " + filePath);
            yield break;
        }

        // Load image as Texture2D
        byte[] fileData = System.IO.File.ReadAllBytes(filePath);
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(fileData); // Load the image

        // Resize image (e.g., scale down to 50% width & height)
        Texture2D resizedTexture = ResizeTexture(texture, texture.width / 2, texture.height / 2);

        // Convert resized texture to JPEG with compression
        byte[] compressedImage = resizedTexture.EncodeToJPG(50); // 50% quality

        // Free memory
        Destroy(texture);
        Destroy(resizedTexture);

        // Upload compressed image
        WWWForm form = new WWWForm();
        form.AddBinaryData("front_view", compressedImage, fileName, "image/jpeg");

        UnityWebRequest request = UnityWebRequest.Post(uploadUrl, form);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Upload failed: " + request.error);
        }
        else
        {
            Debug.Log("Upload success: " + request.downloadHandler.text);
        }
    }

    IEnumerator UploadPhoto2(string fileName)
    {
        string filePath = System.IO.Path.Combine(Application.persistentDataPath, fileName);

        if (!System.IO.File.Exists(filePath))
        {
            Debug.LogError("File not found: " + filePath);
            yield break;
        }

        // Load image as Texture2D
        byte[] fileData = System.IO.File.ReadAllBytes(filePath);
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(fileData); // Load the image

        // Resize image (e.g., scale down to 50% width & height)
        Texture2D resizedTexture = ResizeTexture(texture, texture.width / 2, texture.height / 2);

        // Convert resized texture to JPEG with compression
        byte[] compressedImage = resizedTexture.EncodeToJPG(50); // 50% quality

        // Free memory
        Destroy(texture);
        Destroy(resizedTexture);

        // Upload compressed image
        WWWForm form = new WWWForm();
        form.AddBinaryData("side_view", compressedImage, fileName, "image/jpeg");

        UnityWebRequest request = UnityWebRequest.Post(uploadUrlsideview, form);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Upload failed: " + request.error);
        }
        else
        {
            Debug.Log("Upload success: " + request.downloadHandler.text);
        }
    }

    Texture2D ResizeTexture(Texture2D source, int newWidth, int newHeight)
    {
        RenderTexture rt = new RenderTexture(newWidth, newHeight, 24);
        RenderTexture.active = rt;
        Graphics.Blit(source, rt);
        Texture2D result = new Texture2D(newWidth, newHeight);
        result.ReadPixels(new Rect(0, 0, newWidth, newHeight), 0, 0);
        result.Apply();
        return result;
    }
}
