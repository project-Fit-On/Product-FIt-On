using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Collections;
using System.IO;
using TMPro; // if you're using TMP UI

public class LoadingManager : MonoBehaviour
{

    // Your pipeline endpoints:
    [SerializeField] private string downloadModelUrl = "https://fit-on-3d-115648783380.us-central1.run.app/download";
    [SerializeField] private string localObjFilename = "optimized_model.obj";

    // Called when user clicks "Process" button on Loading Scene UI
    public void OnProcessButtonClicked()
    {
        StartCoroutine(ProcessRoutine());
    }

    private IEnumerator ProcessRoutine()
    {


        // 2) [Optional] Wait or poll for pipeline readiness 
        //    Here we just wait 2 seconds for demonstration.
        Debug.Log("Waiting for pipeline to process...");
        yield return new WaitForSeconds(3f);

        // 3) Download the OBJ file
        Debug.Log("Downloading OBJ model...");
        yield return StartCoroutine(DownloadObjFile(downloadModelUrl, localObjFilename));

        // 4) Once done, load the next scene
        Debug.Log("Loading the next scene...");
        SceneManager.LoadScene("Features");
    }

    // Example of an upload method if needed


    // Downloads the .obj file bytes to persistentDataPath
    private IEnumerator DownloadObjFile(string url, string filename)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                byte[] objFileData = webRequest.downloadHandler.data;
                string fullPath = Path.Combine(Application.persistentDataPath, filename);
                File.WriteAllBytes(fullPath, objFileData);

                Debug.Log("OBJ file downloaded and saved to: " + fullPath);
            }
            else
            {
                Debug.LogError("Failed to download OBJ: " + webRequest.error);
                Debug.Log("Failed to download OBJ file!");
            }
        }
    }

    // Helper to update a status text (UI) if you want

}
