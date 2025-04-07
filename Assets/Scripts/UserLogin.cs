using UnityEngine;
using TMPro;
using Firebase.Auth;
using Firebase.Extensions;

public class UserLogin : MonoBehaviour
{
    [Header("UI References")]
    public TMP_InputField emailInput;
    public TMP_InputField passwordInput;
    public GameObject loginSuccessMessage;
    public GameObject loginFailureMessage;

    [Header("Scene Switcher")]
    public SceneSwitcher sceneSwitcher;  // Assign your SceneSwitcher component here

    public void OnLoginButtonClicked()
    {
        string email = emailInput.text.Trim();
        string password = passwordInput.text;

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            Debug.LogWarning("Email or password is empty!");
            ShowFailure();
            return;
        }

        FirebaseAuth auth = FirebaseManager.Instance.Auth;
        auth.SignInWithEmailAndPasswordAsync(email, password)
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted || task.IsCanceled)
                {
                    Debug.LogError("Login failed: " + task.Exception?.Message);
                    ShowFailure();
                }
                else
                {
                    FirebaseUser user = task.Result.User;
                    Debug.Log("Login successful! User ID: " + user.UserId);
                    ShowSuccess();

                    // Now switch scene only on success
                    if (sceneSwitcher != null)
                    {
                        sceneSwitcher.SwitchSceneWithDelay();
                    }
                }
            });
    }

    void ShowSuccess()
    {
        if (loginSuccessMessage != null) loginSuccessMessage.SetActive(true);
        if (loginFailureMessage != null) loginFailureMessage.SetActive(false);
    }

    void ShowFailure()
    {
        if (loginFailureMessage != null) loginFailureMessage.SetActive(true);
        if (loginSuccessMessage != null) loginSuccessMessage.SetActive(false);
    }
}
