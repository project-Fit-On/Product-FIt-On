// using UnityEngine;

// public class AsgardeoAuth : MonoBehaviour
// {
//     private string clientId = "BmzBKJCTzV6gPotwODfyGAcrHp4a";  // Replace with your Asgardeo Client ID
//     private string redirectUri = "com.fiton://oauth2redirect";  // Replace with your redirect URi
//     private string authUrl = "https://id.asgardeo.io/oauth2/authorize";

//     public void StartLogin()
//     {
//         string loginUrl = $"{authUrl}?response_type=code&client_id={clientId}&redirect_uri={redirectUri}&scope=openid";
//         Application.OpenURL(loginUrl);  // Opens browser for authentication
//     }
// }

using UnityEngine;
using System;

public class AsgardeoLogin : MonoBehaviour
{
    private string clientId = "BmzBKJCTzV6gPotwODfyGAcrHp4a";  
    private string redirectUri = "com.fiton://oauth2redirect";  
    private string authUrl = "https://api.asgardeo.io/t/fiton/oauth2/authorize";

    private WebViewObject webViewObject;

    public void Login()
    {
        Debug.Log("Login button clicked!");  // Debug log to check if the button triggers the method.

        string loginUrl = $"{authUrl}?response_type=code&client_id={clientId}&redirect_uri={redirectUri}&scope=openid%20profile%20email";

        // Create WebViewObject
        webViewObject = (new GameObject("WebView")).AddComponent<WebViewObject>();

        // Initialize WebViewObject
        webViewObject.Init((msg) => {
            Debug.Log("WebView Message: " + msg);
            if (msg.Contains("code=")) 
            {
                string authCode = ExtractAuthCode(msg);
                Debug.Log("OAuth Code Received: " + authCode);

                // Close WebView after getting auth code
                webViewObject.SetVisibility(false);
                Destroy(webViewObject.gameObject);
            }
            else
            {
                Debug.LogWarning("Received unexpected message: " + msg);
            }
        });

        // Load the URL in WebView
        webViewObject.LoadURL(loginUrl);

        // Show WebView
        webViewObject.SetVisibility(true);
        Debug.Log("WebView Loaded with URL: " + loginUrl);
    }

    private string ExtractAuthCode(string url)
    {
        Uri uri = new Uri(url);
        string[] queryParts = uri.Query.TrimStart('?').Split('&');
        foreach (string part in queryParts)
        {
            string[] keyValue = part.Split('=');
            if (keyValue.Length == 2 && keyValue[0] == "code")
            {
                return keyValue[1];
            }
        }
        return null;
    }
}

