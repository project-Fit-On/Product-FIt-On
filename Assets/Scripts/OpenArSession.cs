using UnityEngine;

public class StartARSession : MonoBehaviour
{
    [Header("Assign in Inspector")]
    public GameObject arSession;   // The AR Session GameObject
    public GameObject xrOrigin;    // The XR Origin GameObject

    public GameObject arMesh;


    // This function will be called when the user clicks the "Get Started" button
    public void OnStartButtonClicked()
    {
        if (arSession != null) arSession.SetActive(true);
        if (xrOrigin != null) xrOrigin.SetActive(true);
        if (arMesh != null) arMesh.SetActive(true);


        // Optionally, you could do other setup tasks here, e.g.:
        // ARSession.SetMatchFrameRate(true);
    }
}


