using System;
using UnityEngine;

public class FuelEventLever : InteractableBase
{
    [SerializeField] float countdownToLose = 30;
    private bool isActiveLever = false;
    private bool isEventActive = false;
    [SerializeField] FuelThrustersEvent fuelThrustersEvent;
    public static event Action<string> FuelLeverChangedNarrative;
    protected override void PerformInteraction()
    {
        if(!isEventActive)
        {
            Debug.Log("NotActive");
            FuelLeverChangedNarrative?.Invoke("It's not time for the boost. At least you tried");
            return;
        }

        if (!isActiveLever)
        {
            FuelLeverChangedNarrative?.Invoke("It's not time for the boost. At least you tried");
        }
        else
        {
            isActiveLever = false;
            isEventActive = false;
            fuelThrustersEvent.CompleteTask();
            FuelLeverChangedNarrative?.Invoke("Well Done!");
            Debug.Log("Activating boost!");
        }
    }


    public void ActivateLever()
    {
        Debug.Log("LeverActivated");
        isActiveLever = true;
    }

    public void ActivateEvent()
    {
        isEventActive = true;
    }
}
