using System;
using UnityEngine;


public class FuelThrustersEvent : InteractableBase
{
    [SerializeField] string GameEventName = "FuelThrusters";
    [SerializeField] private GameEventType eventType = GameEventType.FuelThrusters;
    [SerializeField] private float EventDuration = 20f;
    [SerializeField] private bool isFixed = false;
    [SerializeField] private FuelEventLever fuelEventLever;
    public float timeLeft;
    private AudioSource audioSource;
    public static event Action<string> FuelChangedNarrative;

    private void Update()
    {
        if (isActive)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft <= 0)
            {
                FailedTask();
            }
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            ActivateTask();
        }
    }

    public void ActivateTask()
    {

        FuelChangedNarrative?.Invoke("Ion thruster reserves running low. This menial- I mean Vital, task must be conducted by you. Walk towards the fuel storage. I'll meet you there. \n Virtually, of course. ");
        fuelEventLever.ActivateEvent();
        isActive = true;
        timeLeft = EventDuration;
    }

    protected override void PerformInteraction()
    {
        fuelEventLever.ActivateLever();
        FuelChangedNarrative?.Invoke("Now that it's loaded there is another step. Complicated for you, I know. Do your optic receptors see that large [descriptive word] lever? Pull it down.");
        Debug.Log("Activate the lever and full speed ahead");
    }

    public void FailedTask()
    {
        isActive = false;
        Debug.Log("Fuel event failed");
        //TODO Failed Screen
    }

    public void CompleteTask()
    {
        isActive = false;
        Debug.Log("Fuel Event completed");
    }

}
