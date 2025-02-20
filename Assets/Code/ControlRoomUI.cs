using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ControlRoomUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI oxygenText;

    [SerializeField] private interactableO2Supply oxygenSupply;

    private void OnEnable()
    {
        if (oxygenSupply != null)
            oxygenSupply.OxygenChanged += UpdateOxygenUI;
    }

    private void OnDisable()
    {
        if (oxygenSupply != null)
            oxygenSupply.OxygenChanged -= UpdateOxygenUI;
    }

    private void UpdateOxygenUI(float currentOxygen)
    {
        // Update your UI element (e.g., Text or Slider) here.
        oxygenText.text = $"Oxygen: {currentOxygen:F1}%";
    }
}
