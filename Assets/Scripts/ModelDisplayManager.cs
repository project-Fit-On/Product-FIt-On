using UnityEngine;
using System.IO;
using Dummiesman;  // Make sure you have the Dummiesman OBJ importer in your project

public class ModelDisplayManager : MonoBehaviour
{
    [Header("OBJ File")]
    [SerializeField] private string localObjFilename = "optimized_model.obj";

    [Header("Transform Settings")]
    [SerializeField] private Vector3 modelPosition = new Vector3(0f, -1.1f, -2313f);
    [SerializeField] private Vector3 modelScale = Vector3.one;
    [SerializeField] private Vector3 initialModelRotation = new Vector3(0f, 180f, 0f);

    [Header("Spin Settings")]
    [SerializeField] private float constantSpinSpeed = 20f;

    [Header("Model Management")]
    public Transform modelParent; // Assign this to a container GameObject in your scene

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
            // 3) Parent it under the model container if specified
            if (modelParent != null)
            {
                loadedModel.transform.SetParent(modelParent);
            }

            // 4) Position / rotate / scale (relative to parent if set)
            loadedModel.transform.localPosition = modelPosition;
            loadedModel.transform.localRotation = Quaternion.Euler(initialModelRotation);
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
        // 5) Constant rotation around the Y-axis
        if (loadedModel != null)
        {
            loadedModel.transform.Rotate(Vector3.up, constantSpinSpeed * Time.deltaTime, Space.World);
        }
    }
}
