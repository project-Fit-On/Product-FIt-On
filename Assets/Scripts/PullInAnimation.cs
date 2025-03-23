using UnityEngine;
using System.Collections;

public class PullInAnimation : MonoBehaviour
{
    [Header("Animation Settings")]
    public Transform targetTransform;         // The object to animate
    public float duration = 1f;               // How long the animation should take
    public Vector3 startScale = Vector3.zero; // Scale at the beginning
    public Vector3 endScale = Vector3.one;    // Final scale

    private void Start()
    {
        // Immediately set the initial scale
        if (targetTransform != null)
        {
            targetTransform.localScale = startScale;
            StartCoroutine(PullIn());
        }
    }

    private IEnumerator PullIn()
    {
        float elapsed = 0f;

        // Animate over 'duration' seconds
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;

            // Lerp from start to end scale
            if (targetTransform != null)
            {
                targetTransform.localScale = Vector3.Lerp(startScale, endScale, t);
            }

            yield return null;
        }

        // Ensure final scale is exactly endScale
        if (targetTransform != null)
        {
            targetTransform.localScale = endScale;
        }
    }
}
