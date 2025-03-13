using UnityEngine;
using Firebase;
using System.Threading.Tasks;

public class FirebaseInitializer : MonoBehaviour
{
    private void Awake()
    {
        InitializeFirebase();
    }

    private void InitializeFirebase()
    {
        // Check that all of the necessary dependencies for Firebase are present on the system.
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            var dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                // Create and hold a reference to the FirebaseApp,
                // i.e., the connection to Firebase.
                FirebaseApp app = FirebaseApp.DefaultInstance;
                Debug.Log("Firebase is ready to go!");

                // Any additional Firebase setup can go here.
            }
            else
            {
                Debug.LogError(System.String.Format(
                    "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
            }
        });
    }
}
