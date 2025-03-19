using UnityEngine;
using System.Collections;

public class TransitionManager : MonoBehaviour
{
    public CanvasGroup Starter; // Drag the Starter CanvasGroup here
    public CanvasGroup LoginForm; // Drag the Login_form CanvasGroup here
    public float transitionDuration = 1f; // Duration of the fade transition

    private bool isStarterActive = true; // Tracks the active state (handled externally)

    public void TriggerTransition()
    {
        if (isStarterActive)
        {
            StartCoroutine(FadeOut(Starter, LoginForm));
        }
        else
        {
            StartCoroutine(FadeOut(LoginForm, Starter));
        }
        isStarterActive = !isStarterActive; // Toggle state
    }

    private IEnumerator FadeOut(CanvasGroup from, CanvasGroup to)
    {
        float elapsedTime = 0f;

        // Fade out the current CanvasGroup
        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            from.alpha = Mathf.Lerp(1, 0, elapsedTime / transitionDuration);
            yield return null;
        }
        from.alpha = 0;
        from.interactable = false;
        from.blocksRaycasts = false;

        // Reset elapsed time for fade-in
        elapsedTime = 0f;

        // Fade in the next CanvasGroup
        to.interactable = true; // Allow interaction
        to.blocksRaycasts = true; // Allow raycasts
        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            to.alpha = Mathf.Lerp(0, 1, elapsedTime / transitionDuration);
            yield return null;
        }
        to.alpha = 1;
    }
}
