using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneChanger : MonoBehaviour
{
    // Field to set pause duration in the Inspector
    [SerializeField]
    private float pauseDuration = 2f;

    // Method to load a scene by name with a pause
    public void ChangeSceneWithPause(string sceneName)
    {
        // Start the coroutine to handle the pause and scene change
        StartCoroutine(ChangeSceneAfterPause(sceneName));
    }

    // Coroutine to handle the delay
    private IEnumerator ChangeSceneAfterPause(string sceneName)
    {
        // Wait for the specified duration
        yield return new WaitForSeconds(pauseDuration);

        // Make sure the scene exists in the build settings
        if (Application.CanStreamedLevelBeLoaded(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError("Scene " + sceneName + " not found!");
        }
    }

    // Method to load a scene by index with a pause
    public void ChangeSceneByIndexWithPause(int sceneIndex)
    {
        // Start the coroutine to handle the pause and scene change
        StartCoroutine(ChangeSceneByIndexAfterPause(sceneIndex));
    }

    // Coroutine to handle the delay for scene index
    private IEnumerator ChangeSceneByIndexAfterPause(int sceneIndex)
    {
        // Wait for the specified duration
        yield return new WaitForSeconds(pauseDuration);

        // Make sure the scene index is valid
        if (sceneIndex >= 0 && sceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(sceneIndex);
        }
        else
        {
            Debug.LogError("Invalid scene index!");
        }
    }

    // Method to quit the game
    public void QuitGame()
    {
        // If running in the Unity Editor, stop playing the game
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // If built game, quit the application
        Application.Quit();
#endif
    }
}
