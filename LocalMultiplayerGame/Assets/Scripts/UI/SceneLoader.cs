using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Load a scene by name
    public void LoadScene(string sceneName)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);
    }

    // Reload the current scene
    public void ReloadCurrentScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Pause the game
    public void PauseGame()
    {
        Time.timeScale = 0f; // Set time scale to 0 to pause the game
    }

    // Resume the game
    public void ResumeGame()
    {
        Time.timeScale = 1f; // Set time scale to 1 to resume the game
    }
}