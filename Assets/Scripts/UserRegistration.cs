using UnityEngine;
using TMPro; // For TMP_InputField and TMP_Dropdown
using Firebase.Auth;
using Firebase.Firestore;
using Firebase.Extensions;
using System.Collections.Generic;

public class UserRegistration : MonoBehaviour
{
    [Header("UI References")]
    public TMP_InputField emailInput;
    public TMP_InputField passwordInput;
    public TMP_Dropdown genderDropdown;
    // If you have a "re-enter password" field, define another TMP_InputField

    public string bucketName = "fit-on-bucket"; // Replace with your actual bucket name

    // Called by the Register Button's OnClick event
    public void OnRegisterButtonClicked()
    {
        Debug.Log("Register button was clicked!");

        // Gather inputs
        string email = emailInput.text;
        string password = passwordInput.text;
        string gender = genderDropdown.options[genderDropdown.value].text;

        Debug.Log($"Attempting registration: email={email}, gender={gender}");

        // Get Firebase Auth reference
        var auth = FirebaseManager.Instance.Auth;
        if (auth == null)
        {
            Debug.LogError("Firebase Auth not initialized!");
            return;
        }

        // Create the user in Firebase Auth
        auth.CreateUserWithEmailAndPasswordAsync(email, password)
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted)
                {
                    Debug.LogError("CreateUserWithEmailAndPasswordAsync FAILED: " + task.Exception);
                    return;
                }

                // Success
                AuthResult authResult = task.Result;
                FirebaseUser newUser = authResult.User;

                Debug.Log("User created successfully! UID: " + newUser.UserId);

                // Now store extra info in Firestore
                StoreUserDataInFirestore(newUser, gender);
            });
    }

    private void StoreUserDataInFirestore(FirebaseUser user, string gender)
    {
        Debug.Log("Storing user data in Firestore...");
        var db = FirebaseManager.Instance.Db;
        if (db == null)
        {
            Debug.LogError("Firestore not initialized!");
            return;
        }

        // We'll store a path pointing to "gs://your-bucket-name/users/<UID>/"
        // This does NOT create a folder automatically in GCS – you'd need to upload a file for that folder to appear.
        string storagePath = $"gs://{bucketName}/users/{user.UserId}/";

        var userData = new Dictionary<string, object>
        {
            { "email", user.Email },
            { "gender", gender },
            { "storageRef", storagePath }
        };
        Debug.Log("Writing to Firestore at: users/" + user.UserId);

        db.Collection("users").Document(user.UserId)
          .SetAsync(userData)
          .ContinueWithOnMainThread(setTask =>
          {
              Debug.Log("Firestore callback triggered!");
              if (setTask.IsFaulted)
              {
                  Debug.LogError("Error saving user data to Firestore: " + setTask.Exception);
              }
              else
              {
                  Debug.Log("User data stored successfully in Firestore!");
              }
          });
    }
}
