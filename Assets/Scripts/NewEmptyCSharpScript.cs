using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class NewEmptyCSharpScript : MonoBehaviour
{
    public InputField nameInputField;
    public InputField emailInputField;
    public InputField passwordInputField;

    public Button signUpButton;

    private string filePath;

    void Start()
    {
        // Set up the file path to save the CSV (in Application.persistentDataPath)
        filePath = Path.Combine(Application.persistentDataPath, "SignUpSheet.csv");

        // Ensure the CSV file has headers if it doesn't exist
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "Name,Email,Password\n");
        }

        // Add listener to the Sign-Up button
        signUpButton.onClick.AddListener(SaveUserData);
    }

    void SaveUserData()
    {
        // Get input data
        string name = nameInputField.text;
        string email = emailInputField.text;
        string password = passwordInputField.text;

        // Basic validation
        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
        {
            Debug.LogWarning("All fields must be filled out!");
            return;
        }

        // Save data to the CSV file
        string newLine = $"{name},{email},{password}\n";
        File.AppendAllText(filePath, newLine);

        Debug.Log("Data saved to CSV file: " + filePath);

        // Clear the input fields after saving
        nameInputField.text = "";
        emailInputField.text = "";
        passwordInputField.text = "";
    }
}
