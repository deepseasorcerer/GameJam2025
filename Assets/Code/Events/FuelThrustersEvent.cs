using System;
using UnityEngine;
using UnityEngine.SceneManagement;


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

        FuelChangedNarrative?.Invoke("Ion thruster reserves running low. This menial- I mean Vital, task must be conducted by you. Walk towards the fuel storage. I'll meet you there. \n Virtually, of course. (Add Gas to engine)");
        fuelEventLever.ActivateEvent();
        isActive = true;
        timeLeft = EventDuration;
    }

    protected override void PerformInteraction()
    {
        fuelEventLever.ActivateLever();
        FuelChangedNarrative?.Invoke("Now that it's loaded there is another step. Complicated for you, I know. Do your optic receptors see that large [descriptive word] lever? Pull it down.\r\n (Activate the lever)");
        Debug.Log("Activate the lever and full speed ahead");
    }

    public void FailedTask()
    {
        Debug.Log("Fuel Event failed");
        isActive = false;
        SceneManager.LoadScene("DefeatScreen");
    }
    public void CompleteTask()
    {
        isActive = false;
        Debug.Log("Fuel Event completed");
    }

}
