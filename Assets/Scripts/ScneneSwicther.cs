using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class SceneSwitcher : MonoBehaviour

{

    [Header("Scene to Load")]
    public string sceneToLoad = "SceneName";    // The name of the scene to switch to

    [Header("Delay Settings")]
    public float delay = 3f;                    // How many seconds to wait before switching

    public GameObject LoginUnsuccesfull;

    // Call this function from the button’s OnClick event
    public void SwitchSceneWithDelay()
    {
        StartCoroutine(SwitchSceneCoroutine());
    }

    private IEnumerator SwitchSceneCoroutine()
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Then load the scene
        SceneManager.LoadScene(sceneToLoad);
    }
}
