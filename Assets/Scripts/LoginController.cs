using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LoginController : MonoBehaviour
{

    public TMP_InputField emailField;
    public TMP_InputField passwordField;
    public Button registerButton;

    void Start()
    {
        // Subscribe to input field changes

        emailField.onValueChanged.AddListener(delegate { CheckFields(); });
        passwordField.onValueChanged.AddListener(delegate { CheckFields(); });

        // Initial check
        CheckFields();
    }

    void CheckFields()
    {
        // Trim() to prevent spaces-only input from counting
        bool allFilled = !string.IsNullOrWhiteSpace(emailField.text)
                      && !string.IsNullOrWhiteSpace(passwordField.text);

        registerButton.interactable = allFilled;
    }
}
