using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public Button startButton;
    public Button settingsButton;

    void Start()
    {
        startButton.onClick.AddListener(StartGame);
        settingsButton.onClick.AddListener(OpenSettings);
    }

    // Starts the game with log
    private void StartGame()
    {
        Debug.Log("Start button clicked! Loading Game Scene...");
        SceneManager.LoadScene("Level"); // add first Level here, also change in build menu
    }

    // Opens the settings menu with log
    private void OpenSettings()
    {
        Debug.Log("Settings button clicked! Loading Settings Scene...");
        SceneManager.LoadScene("SettingsMenu");
    }

    // Exits the application
    public void ExitGame()
    {
        Debug.Log("Exiting Game...");
        Application.Quit();
    }

}
