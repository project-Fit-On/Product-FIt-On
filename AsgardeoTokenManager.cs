using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class AsgardeoTokenManager : MonoBehaviour
{
    private string tokenUrl = "https://api.asgardeo.io/t/fit0n/oauth2/token";
    private string clientId = "BmzBKJCTzV6gPotwODfyGAcrHp4a";
    private string redirectUri = "com.fitton://oauth2redirect";
    private string codeVerifier;
    
    public IEnumerator GetAccessToken(string authCode, System.Action<string> onSuccess)
    {
        WWWForm form = new WWWForm();
        form.AddField("grant_type", "authorization_code");
        form.AddField("client_id", clientId);
        form.AddField("redirect_uri", redirectUri);
        form.AddField("code", authCode);
        form.AddField("code_verifier", codeVerifier);

        using (UnityWebRequest request = UnityWebRequest.Post(tokenUrl, form))
        {
            request.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded");

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Access Token Response: " + request.downloadHandler.text);
                onSuccess(request.downloadHandler.text);
            }
            else
            {
                Debug.LogError("Error Getting Token: " + request.error);
            }
        }
    }
}



