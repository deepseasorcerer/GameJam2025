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
    }

    public void ActivateTask()
    {
        fuelEventLever.ActivateEvent();
        isActive = true;
        timeLeft = EventDuration;
    }

    protected override void PerformInteraction()
    {
        fuelEventLever.ActivateLever();
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
