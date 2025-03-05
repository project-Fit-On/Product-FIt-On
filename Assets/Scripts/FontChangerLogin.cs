using System.Collections;
using TMPro;
using UnityEngine;

public class FontChangerLogin : MonoBehaviour
{
    public GameObject Starter; // Reference to Starter GameObject
    public GameObject Login; // Reference to Login_form GameObject
    public TextMeshProUGUI Phrase; // Reference to the TextMeshPro object

    public float FadeDuration = 1.0f; // Duration for fade-in and fade-out
    public float DelayBeforeTyping = 2.0f; // Delay before typewriter animation starts
    public float TypingSpeed = 0.05f; // Speed for typing effect
    public float DelayAfterTyping = 2.0f; // Delay before fade-out starts
    public float DelayBeforeRestart = 2.0f; // Additional delay before the animation starts again

    private CanvasGroup PhraseCanvasGroup; // To control fade animations
    private string fullText; // Full text of the phrase
    private bool isAnimating = false; // To prevent multiple coroutine invocations

    void Start()
    {
        // Validate references
        if (Starter == null || Login == null || Phrase == null)
        {
            Debug.LogError("Ensure Starter, Login_form, and Phrase objects are assigned.");
            return;
        }

        // Get or add CanvasGroup
        PhraseCanvasGroup = Phrase.GetComponent<CanvasGroup>();
        if (PhraseCanvasGroup == null)
        {
            PhraseCanvasGroup = Phrase.gameObject.AddComponent<CanvasGroup>();
        }

        // Initialize text and alpha
        fullText = Phrase.text; // Store the full text
        Phrase.text = ""; // Clear text initially
        PhraseCanvasGroup.alpha = 0; // Set initial alpha to 0
    }

    void Update()
    {
        // Continuously check if Login_form is active
        if (Login.activeInHierarchy && !Starter.activeInHierarchy)
        {
            if (!isAnimating)
            {
                isAnimating = true;
                StartCoroutine(AnimateSequence());
            }
        }
        else
        {
            // Stop animations if Login_form becomes inactive
            if (isAnimating)
            {
                StopAllCoroutines();
                ResetPhrase();
                isAnimating = false;
            }
        }
    }

    private IEnumerator AnimateSequence()
    {
        while (Login.activeInHierarchy)
        {
            yield return StartCoroutine(FadeIn());
            yield return StartCoroutine(TypeWriterEffect());
            yield return new WaitForSeconds(DelayAfterTyping);
            yield return StartCoroutine(FadeOut());

            // Wait before restarting the animation
            yield return new WaitForSeconds(DelayBeforeRestart);
        }
        isAnimating = false;
    }

    private IEnumerator FadeIn()
    {
        float timer = 0;
        while (timer < FadeDuration)
        {
            timer += Time.deltaTime;
            PhraseCanvasGroup.alpha = Mathf.Clamp01(timer / FadeDuration);
            yield return null;
        }
    }

    private IEnumerator FadeOut()
    {
        float timer = 0;
        while (timer < FadeDuration)
        {
            timer += Time.deltaTime;
            PhraseCanvasGroup.alpha = Mathf.Clamp01(1 - timer / FadeDuration);
            yield return null;
        }
    }

    private IEnumerator TypeWriterEffect()
    {
        Phrase.text = ""; // Clear the text
        foreach (char letter in fullText.ToCharArray())
        {
            Phrase.text += letter;
            yield return new WaitForSeconds(TypingSpeed);
        }
    }

    private void ResetPhrase()
    {
        PhraseCanvasGroup.alpha = 0; // Reset fade
        Phrase.text = ""; // Clear text
    }
}
