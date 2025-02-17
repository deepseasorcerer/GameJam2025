using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsMenuManager : MonoBehaviour
{
    public Slider mouseSensitivitySlider;

    private void Start()
    {
        // Load saved sensitivity
        mouseSensitivitySlider.value = PlayerPrefs.GetFloat("MouseSensitivity", 5f);

        // Add listener for slider changes
        mouseSensitivitySlider.onValueChanged.AddListener(SetMouseSensitivity);
    }

    private void SetMouseSensitivity(float value)
    {
        Debug.Log($"Mouse Sensitivity set to: {value}");
        PlayerPrefs.SetFloat("MouseSensitivity", value);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
