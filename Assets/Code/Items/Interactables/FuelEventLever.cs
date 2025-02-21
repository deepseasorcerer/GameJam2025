using UnityEngine;

public class FuelEventLever : InteractableBase
{
    [SerializeField] float countdownToLose = 30;
    private bool isActiveLever = false;
    private bool isEventActive = false;
    [SerializeField] FuelThrustersEvent fuelThrustersEvent;
    protected override void PerformInteraction()
    {
        if (!isActiveLever)
        {
            Debug.Log("We need more fuel!");
        }
        else
        {
            isActiveLever = false;
            isEventActive = false;
            fuelThrustersEvent.CompleteTask();
            Debug.Log("Activating boost!");
        }
    }


    public void ActivateLever()
    {
        isActiveLever = true;
    }

    public void ActivateEvent()
    {
        isEventActive = true;
    }
}
