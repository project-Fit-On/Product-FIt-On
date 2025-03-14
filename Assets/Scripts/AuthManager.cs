using UnityEngine;
using Firebase.Auth;
using System;

public class AuthManager : MonoBehaviour
{
    private FirebaseAuth auth;

    private void Awake()
    {
        // Check if Firebase is already ready
        if (FirebaseInitializer.IsFirebaseReady)
        {
            SetupFirebaseAuth();
        }
        else
        {
            // If not, subscribe to the event
            FirebaseInitializer.OnFirebaseInitialized += SetupFirebaseAuth;
        }
    }

    private void OnDestroy()
    {
        // Unsubscribe to avoid memory leaks when this object is destroyed
        FirebaseInitializer.OnFirebaseInitialized -= SetupFirebaseAuth;
    }

    private void SetupFirebaseAuth()
    {
        // Now we know Firebase is initialized
        auth = FirebaseAuth.DefaultInstance;
        Debug.Log("AuthManager: FirebaseAuth is now available.");
    }

    /// <summary>
    /// Register a new user with email/password
    /// </summary>
    public void RegisterUser(string email, string password)
    {
        if (!FirebaseInitializer.IsFirebaseReady)
        {
            Debug.LogWarning("Firebase is not ready yet. Can't register user.");
            return;
        }

        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
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

            var authResult = task.Result;
            var newUser = authResult.User;
            Debug.Log($"Firebase user created successfully: {newUser.Email} (UID: {newUser.UserId})");
        });
    }

    /// <summary>
    /// Login with email/password
    /// </summary>
    public void LoginUser(string email, string password)
    {
        if (!FirebaseInitializer.IsFirebaseReady)
        {
            Debug.LogWarning("Firebase is not ready yet. Can't login.");
            return;
        }

        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
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

            var authResult = task.Result;
            var user = authResult.User;
            Debug.Log($"User signed in successfully: {user.Email} (UID: {user.UserId})");
        });
    }
}
