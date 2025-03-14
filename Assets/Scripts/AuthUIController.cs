using UnityEngine;
using TMPro;  // <-- Important!
using UnityEngine.UI;

public class AuthUIController : MonoBehaviour
{
    [Header("TMP Input Fields")]
    public TMP_InputField emailField;
    public TMP_InputField passwordField;

    [Header("Reference to AuthManager")]
    public AuthManager authManager;

    public void OnRegisterButtonClicked()
    {
        string email = emailField.text;
        string password = passwordField.text;

        // Call your AuthManager's RegisterUser method
        authManager.RegisterUser(email, password);
    }

    public void OnLoginButtonClicked()
    {
        string email = emailField.text;
        string password = passwordField.text;

        // Call your AuthManager's LoginUser method
        authManager.LoginUser(email, password);
    }
}
