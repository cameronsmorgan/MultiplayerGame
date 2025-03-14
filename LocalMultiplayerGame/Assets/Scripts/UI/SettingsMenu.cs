using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    public GameObject settingsPanel; // Reference to the settings panel UI

    private SceneLoader sceneLoader;

    private void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false); // Hide the settings panel at start
        }
    }

    // Open the settings menu and pause the game
    public void OpenSettings()
    {
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(true);
            sceneLoader.PauseGame();
        }
    }

    // Close the settings menu and resume the game
    public void CloseSettings()
    {
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false);
            sceneLoader.ResumeGame();
        }
    }

    // Go back to the Home scene
    public void GoToHome()
    {
        sceneLoader.ResumeGame(); // Ensure the game is resumed before loading the Home scene
        sceneLoader.LoadScene("Home");
    }

    // Restart the current scene
    public void RestartLevel()
    {
        sceneLoader.ResumeGame(); // Ensure the game is resumed before reloading the scene
        sceneLoader.ReloadCurrentScene();
    }
}