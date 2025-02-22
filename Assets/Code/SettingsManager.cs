using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsMenuManager : MonoBehaviour
{
    public Slider mouseSensitivitySlider;
    public Slider volumeSlider;

    private void Start()
    {
        mouseSensitivitySlider.value = PlayerPrefs.GetFloat("MouseSensitivity", 1f);
        volumeSlider.value = PlayerPrefs.GetFloat("GameVolume", 1f);

        mouseSensitivitySlider.onValueChanged.AddListener(SetMouseSensitivity);
        volumeSlider.onValueChanged.AddListener(SetGameVolume);
    }

    private void SetMouseSensitivity(float value)
    {
        Debug.Log($"Mouse Sensitivity set to: {value}");
        PlayerPrefs.SetFloat("MouseSensitivity", value);
    }

    private void SetGameVolume(float value)
    {
        Debug.Log($"Game Volume set to: {value}");
        AudioListener.volume = value; // Adjust global volume
        PlayerPrefs.SetFloat("GameVolume", value);
        PlayerPrefs.Save();
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }   
}
