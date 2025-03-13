using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AuthUIController : MonoBehaviour
{
    public TMP_InputField emailField;
    public TMP_InputField passwordField;
    public AuthManager authManager;

    public void OnRegisterButtonClicked()
    {
        string email = emailField.text;
        string password = passwordField.text;
        authManager.RegisterUser(email, password);
    }

    public void OnLoginButtonClicked()
    {
        string email = emailField.text;
        string password = passwordField.text;
        authManager.LoginUser(email, password);
    }
}
