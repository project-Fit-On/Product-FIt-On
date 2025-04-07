using UnityEngine;
using System.IO;
using Dummiesman;  // Make sure you have the Dummiesman OBJ importer in your project

public class ModelDisplayManager : MonoBehaviour
{
    [SerializeField] private string localObjFilename = "optimized_model.obj";

    // Position & scale for your model
    private Vector3 modelPosition = new Vector3(0f, -1.6f, 5.8f);
    private Vector3 modelScale = Vector3.one;

    // Set the initial rotation to 180° on the Y axis
    private Vector3 initialModelRotation = new Vector3(0f, 180f, 0f);

    // How fast to continuously spin around Y-axis (degrees per second)
    [SerializeField] private float constantSpinSpeed = 20f;

    private GameObject loadedModel;

    private void Start()
    {
        // 1) Construct full path to the .obj file
        string fullPath = Path.Combine(Application.persistentDataPath, localObjFilename);

        if (!File.Exists(fullPath))
        {
            Debug.LogError("OBJ file not found at path: " + fullPath);
            return;
        }

        // 2) Load with Dummiesman
        loadedModel = new OBJLoader().Load(fullPath);

        if (loadedModel != null)
        {
            // 3) Position / rotate / scale
            loadedModel.transform.position = modelPosition;
            loadedModel.transform.rotation = Quaternion.Euler(initialModelRotation);
            loadedModel.transform.localScale = modelScale;

            Debug.Log("Model loaded and positioned successfully!");
        }
        else
        {
            Debug.LogError("Dummiesman failed to load the .obj");
        }
    }

    private void Update()
    {
        // 4) Constant rotation around the Y-axis
        if (loadedModel != null)
        {
            loadedModel.transform.Rotate(Vector3.up, constantSpinSpeed * Time.deltaTime, Space.World);
        }
    }
}
