using UnityEngine;
using System;
using System.Security.Cryptography;
using System.Text;
// public class AsgardeoLogin : MonoBehaviour
// {
//     private string clientId = "BmzBKJCTzV6gPotwODfyGAcrHp4a";  
//     private string redirectUri = "com.fiton://oauth2redirect";  
//     private string authUrl = "https://api.asgardeo.io/t/fiton/oauth2/authorize";

//     private WebViewObject webViewObject;

//     public void Login()
//     {
//         Debug.Log("Login button clicked!");  // Debug log to check if the button triggers the method.

//         string loginUrl = $"{authUrl}?response_type=code&client_id={clientId}&redirect_uri={redirectUri}&scope=openid%20profile%20email";

//         // Create WebViewObject
//         webViewObject = (new GameObject("WebView")).AddComponent<WebViewObject>();

//         // Initialize WebViewObject
//         webViewObject.Init((msg) => {
//             Debug.Log("WebView Message: " + msg);
//             if (msg.Contains("code=")) 
//             {
//                 string authCode = ExtractAuthCode(msg);
//                 Debug.Log("OAuth Code Received: " + authCode);

//                 // Close WebView after getting auth code
//                 webViewObject.SetVisibility(false);
//                 Destroy(webViewObject.gameObject);
//             }
//             else
//             {
//                 Debug.LogWarning("Received unexpected message: " + msg);
//             }
//         });

//         // Load the URL in WebView
//         webViewObject.LoadURL(loginUrl);

//         // Show WebView
//         webViewObject.SetVisibility(true);
//         Debug.Log("WebView Loaded with URL: " + loginUrl);
//     }

//     private string ExtractAuthCode(string url)
//     {
//         Uri uri = new Uri(url);
//         string[] queryParts = uri.Query.TrimStart('?').Split('&');
//         foreach (string part in queryParts)
//         {
//             string[] keyValue = part.Split('=');
//             if (keyValue.Length == 2 && keyValue[0] == "code")
//             {
//                 return keyValue[1];
//             }
//         }
//         return null;
//     }
// }

// using UnityEngine;

public class AsgardeoLogin : MonoBehaviour
{
    private string clientId = "BmzBKJCTzV6gPotwODfyGAcrHp4a"; 
    private string redirectUri = "com.fitton://oauth2redirect";
    private string authUrl = "https://api.asgardeo.io/t/fit0n/oauth2/authorize"; 

    private WebViewObject webViewObject;

    public void Login()
    {
        var (codeVerifier, codeChallenge) = PKCE.GeneratePKCE();
        string loginUrl = $"{authUrl}?response_type=code&client_id={clientId}&redirect_uri={redirectUri}&scope=openid%20profile%20email&code_challenge={codeChallenge}&code_challenge_method=S256";

        webViewObject = (new GameObject("WebView")).AddComponent<WebViewObject>();
        webViewObject.Init((msg) => {
            Debug.Log("WebView Message: " + msg);
            if (msg.Contains("code=")) 
            {
                // Extract auth code from the URL
                string authCode = ExtractAuthCode(msg);
                Debug.Log("OAuth Code Received: " + authCode);
                
                // Close WebView after getting auth code
                webViewObject.SetVisibility(false);
            }
        });
        webViewObject.LoadURL(loginUrl);
        webViewObject.SetVisibility(true);
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
