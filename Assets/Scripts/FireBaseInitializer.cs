using UnityEngine;
using Firebase;
using System.Threading.Tasks;
using System;

public class FirebaseInitializer : MonoBehaviour
{
    // An event to notify other scripts that Firebase is initialized.
    public static event Action OnFirebaseInitialized;

    // A static boolean so scripts can quickly check if Firebase is ready.
    public static bool IsFirebaseReady { get; private set; }

    private void Awake()
    {
        InitializeFirebase();
    }

    private void InitializeFirebase()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            var dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                // Mark Firebase as ready.
                FirebaseApp app = FirebaseApp.DefaultInstance;
                IsFirebaseReady = true;
                Debug.Log("Firebase is initialized and ready!");

                // Fire the event so other scripts know they can proceed
                OnFirebaseInitialized?.Invoke();
            }
            else
            {
                IsFirebaseReady = false;
                Debug.LogError($"Could not resolve all Firebase dependencies: {dependencyStatus}");
            }
        });
    }
}
