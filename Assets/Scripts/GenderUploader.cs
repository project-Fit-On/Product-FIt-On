using UnityEngine;
using TMPro;                         // For TMP_Dropdown
using UnityEngine.Networking;        // For UnityWebRequest
using System.Collections;
using System.Text;                   // For Encoding.UTF8

[System.Serializable]
public class GenderPayload
{
    public string gender;
}

public class GenderUploader : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown genderDropdown;
    // Replace with your actual endpoint:
    [SerializeField] private string uploadGenderUrl = "https://fit-on-3d-115648783380.us-central1.run.app/upload/gender ";

    // Called by a UI button
    public void OnUploadGenderButtonClicked()
    {
        // Grab the currently selected value from the TMP_Dropdown
        string selectedGender = genderDropdown.options[genderDropdown.value].text;

        // Start the coroutine to upload the gender
        StartCoroutine(UploadGender(selectedGender));
    }

    private IEnumerator UploadGender(string gender)
    {
        // 1) Create the payload object
        GenderPayload payload = new GenderPayload();
        payload.gender = gender;   // Note the lowercase 'gender' to match your Python model

        // 2) Convert to JSON
        string jsonBody = JsonUtility.ToJson(payload);

        // 3) Construct the UnityWebRequest for a POST with application/json
        using (UnityWebRequest request = new UnityWebRequest(uploadGenderUrl, "POST"))
        {
            // Convert JSON string to raw bytes
            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonBody);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            // 4) Send the request
            yield return request.SendWebRequest();

            // 5) Check the result
            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Gender upload success: " + request.downloadHandler.text);
            }
            else
            {
                Debug.LogError("Error uploading gender: " + request.error);
            }
        }
    }
}
