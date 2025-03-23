using System.IO;
using UnityEngine;

public class Save_1 : MonoBehaviour
{
    public void SavePhoto(Texture2D photo)
    {
        // Convert to PNG (or use EncodeToJPG if you prefer)
        byte[] imageBytes = photo.EncodeToPNG();

        // Generate a unique filename (e.g. Photo1.png / Photo2.png)
        // or you can use a timestamp if you prefer
        string fileName = "Photo1.png";

        // Build the full path
        string fullPath = Path.Combine(Application.persistentDataPath, fileName);

        // Write the bytes to the file
        File.WriteAllBytes(fullPath, imageBytes);

        Debug.Log("Saved photo to: " + fullPath);
    }
}
