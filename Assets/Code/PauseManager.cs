using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public GameObject settingsMenu;

    // Opens the settings menu with log
    public void OpenSettings()
    {
        settingsMenu.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsMenu.SetActive(false);
    }

    // Exits the application
    public void ExitGame()
    {
        Debug.Log("Exiting Game...");
        Application.Quit();
    }

    //Go back to Menu
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    //Restart the level
    public void RestartGame()
    {
        SceneManager.LoadScene("MainGame");
    }
}
