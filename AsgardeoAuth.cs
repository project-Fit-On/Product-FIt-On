
using UnityEngine;
using System;
using System.Security.Cryptography;
using System.Text;  

public class AsgardeoAuth : MonoBehaviour
{
    private string clientId = "BmzBKJCTzV6gPotwODfyGAcrHp4a"; 
    private string redirectUri = "com.fitton://oauth2redirect";
    private string authUrl = "https://api.asgardeo.io/t/fit0n/oauth2/authorize"; 
    private string tokenUrl = "https://api.asgardeo.io/t/fit0n/oauth2/token";

    private WebViewObject webViewObject;
    private string codeVerifier;
    private string authCode;
    private AsgardeoTokenManager tokenManager;

    void Start()
    {
    // Find existing token manager or create a new one
    tokenManager = FindObjectOfType<AsgardeoTokenManager>();
    if (tokenManager == null)
    {
        GameObject tokenManagerObj = new GameObject("TokenManager");
        tokenManager = tokenManagerObj.AddComponent<AsgardeoTokenManager>();
    }
    }

    public void Login()
    {
        // Generate PKCE code verifier and code challenge
        codeVerifier = GenerateCodeVerifier();
        string codeChallenge = GenerateCodeChallenge(codeVerifier);
        
        string loginUrl = $"{authUrl}?response_type=code" +
                          $"&client_id={clientId}" +
                          $"&redirect_uri={redirectUri}" +
                          $"&scope=openid%20profile%20email" +
                          $"&code_challenge={codeChallenge}" +
                          $"&code_challenge_method=S256";

        webViewObject = (new GameObject("WebView")).AddComponent<WebViewObject>();
        webViewObject.Init((msg) => {
            Debug.Log("WebView Message: " + msg);
            if (msg.Contains("code=")) 
            {
                // Extract auth code from the URL
                authCode = ExtractAuthCode(msg);
                Debug.Log("OAuth Code Received: " + authCode);
                
                // Close WebView after getting auth code
                webViewObject.SetVisibility(false);
                Destroy(webViewObject.gameObject);

                // Exchange auth code for access token
                StartCoroutine(tokenManager.GetAccessToken(authCode, (accessToken) => {
                    Debug.Log("Access Token Received: " + accessToken);
                })); 
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


    private string GenerateCodeVerifier()
    {
        byte[] randomBytes = new byte[32];
        using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomBytes);
        }
        return Convert.ToBase64String(randomBytes).TrimEnd('=').Replace('+', '-').Replace('/', '_');
    }


    private string GenerateCodeChallenge(string verifier)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] challengeBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(verifier));
            return Convert.ToBase64String(challengeBytes).TrimEnd('=').Replace('+', '-').Replace('/', '_');
        }
    }
     
    // public void Logout()
    // {
    // string logoutUrl = "https://api.asgardeo.io/t/fit0n/oidc/logout";
    // Application.OpenURL(logoutUrl);
    // }
}
