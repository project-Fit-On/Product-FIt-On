using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class AsgardeoUserProfile : MonoBehaviour
{
    private string userInfoUrl = "https://api.asgardeo.io/t/YOUR_TENANT/oauth2/userinfo";

    public IEnumerator GetUserProfile(string accessToken)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(userInfoUrl))
        {
            request.SetRequestHeader("Authorization", "Bearer " + accessToken);

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("User Info: " + request.downloadHandler.text);
            }
            else
            {
                Debug.LogError("Error Fetching User Info: " + request.error);
            }
        }
    }
}

