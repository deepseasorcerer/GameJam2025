using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameUi : MonoBehaviour
{
    
    [SerializeField] private Image OxygenBar;

    [SerializeField] private interactableO2Supply o2Supply;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private TextMeshProUGUI ArrivalText;
    
    private void Update()
    {
        OxygenBar.fillAmount = o2Supply.O2LeftAmount / 100f;
        ArrivalText.text = "Arrival in: " + Math.Round(gameManager.GameLength - gameManager.timeElapsed, 0) + "s";
    }
}
