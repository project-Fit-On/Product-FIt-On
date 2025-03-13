using System.Threading.Tasks;
using Firebase;
using Firebase.Auth;
using Firebase.Extensions;  // Needed for ContinueWithOnMainThread
using UnityEngine;

public class AuthManager : MonoBehaviour
{
    FirebaseAuth auth;

    void Start()
    {
        auth = FirebaseAuth.DefaultInstance;
    }

    public void RegisterUser(string email, string password)
    {
        auth.CreateUserWithEmailAndPasswordAsync(email, password)
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsCanceled)
                {
                    Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                    return;
                }

                // Now task.Result is an AuthResult, not a FirebaseUser
                AuthResult authResult = task.Result;
                FirebaseUser newUser = authResult.User; // Extract the user
                Debug.LogFormat("User created successfully: {0} ({1})",
                    newUser.DisplayName, newUser.UserId);
            });
    }

    public void LoginUser(string email, string password)
    {
        auth.SignInWithEmailAndPasswordAsync(email, password)
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsCanceled)
                {
                    Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                    return;
                }

                // task.Result is an AuthResult
                AuthResult authResult = task.Result;
                FirebaseUser user = authResult.User; // Extract the user
                Debug.LogFormat("User signed in successfully: {0} ({1})",
                    user.DisplayName, user.UserId);
            });
    }
}
