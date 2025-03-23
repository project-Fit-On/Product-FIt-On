using UnityEngine;
using TMPro;
using System.Collections;

public class TextSequenceAnimator : MonoBehaviour
{
    [Header("Text References (TextMeshPro)")]
    public TMP_Text text1;
    public TMP_Text text2;
    public TMP_Text text3;

    [Header("Timing Settings")]
    public float fadeDuration = 1f;     // Duration of each fade transition
    public float displayDuration = 3f;  // How long each text stays fully visible

    // Public method to be called on button click
    public void PlayTextSequence()
    {
        // Ensure all texts start as transparent
        SetAlpha(text1, 0);
        SetAlpha(text2, 0);
        SetAlpha(text3, 0);

        // Start the text sequence coroutine
        StartCoroutine(PlaySequence());
    }

    private IEnumerator PlaySequence()
    {
        // Fade in text1
        yield return StartCoroutine(FadeIn(text1, fadeDuration));
        // Wait while text1 is fully visible
        yield return new WaitForSeconds(displayDuration);

        // Crossfade: text1 fades out while text2 fades in
        yield return StartCoroutine(CrossFade(text1, text2, fadeDuration));
        // Wait while text2 is fully visible
        yield return new WaitForSeconds(displayDuration);

        // Crossfade: text2 fades out while text3 fades in
        yield return StartCoroutine(CrossFade(text2, text3, fadeDuration));
        // Wait while text3 is fully visible
        yield return new WaitForSeconds(displayDuration);

        Debug.Log("Text sequence complete!");
    }

    // Fades a text in from transparent (alpha 0) to fully visible (alpha 1)
    private IEnumerator FadeIn(TMP_Text text, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            SetAlpha(text, Mathf.Lerp(0f, 1f, t));
            yield return null;
        }
        SetAlpha(text, 1f);
    }

    // Crossfades: fades one text out while fading another text in simultaneously.
    private IEnumerator CrossFade(TMP_Text fadeOutText, TMP_Text fadeInText, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            SetAlpha(fadeOutText, Mathf.Lerp(1f, 0f, t));
            SetAlpha(fadeInText, Mathf.Lerp(0f, 1f, t));
            yield return null;
        }
        SetAlpha(fadeOutText, 0f);
        SetAlpha(fadeInText, 1f);
    }

    // Helper method to change the alpha of a TMP_Text component.
    private void SetAlpha(TMP_Text text, float alpha)
    {
        if (text != null)
        {
            Color c = text.color;
            c.a = alpha;
            text.color = c;
        }
    }
}
