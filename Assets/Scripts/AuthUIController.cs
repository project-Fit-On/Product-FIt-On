using UnityEngine;
using UnityEngine.UI;

public class AuthUIController : MonoBehaviour
{
    public InputField emailField;
    public InputField passwordField;
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
