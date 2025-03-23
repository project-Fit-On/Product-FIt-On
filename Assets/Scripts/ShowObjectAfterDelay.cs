using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShowObjectAfterDelay : MonoBehaviour
{
    [Header("Object to Show")]
    public GameObject objectToShow;   // Assign your GameObject here in the Inspector

    [Header("Delay Settings")]
    public float delay = 3f;         // Delay in seconds before the object appears

    // Call this function from the button's OnClick event in the Inspector
    public void OnButtonClick()
    {
        StartCoroutine(ShowObjectCoroutine());
    }

    private IEnumerator ShowObjectCoroutine()
    {
        // Optional: Hide the object if it’s visible
        if (objectToShow.activeSelf)
            objectToShow.SetActive(false);

        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Activate the object
        objectToShow.SetActive(true);
    }
}
