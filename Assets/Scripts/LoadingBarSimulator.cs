using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Rendering;

public class LoadingBarSimulator : MonoBehaviour
{
    [Header("UI Reference")]
    public Image loadingBar;    // Reference to your UI Image set to Filled

    [Header("Loading Settings")]
    public float loadTime = 5f; // Duration for the loading simulation



    public void LoadtheBar()
    {
        StartCoroutine(SimulateLoading());
    }

    private IEnumerator SimulateLoading()
    {
        float elapsed = 0f;
        // Initialize the loading bar (set to 0)
        if (loadingBar != null)
        {
            loadingBar.fillAmount = 0f;
        }

        while (elapsed < loadTime)
        {
            elapsed += Time.deltaTime;
            // Calculate the progress as a fraction (0 to 1)
            float progress = Mathf.Clamp01(elapsed / loadTime);
            // Update the fill amount of the loading bar
            if (loadingBar != null)
            {
                loadingBar.fillAmount = progress;
            }
            yield return null;
        }

        // Loading complete; you can trigger any follow-up actions here
        Debug.Log("Loading simulation complete!");
    }
}
