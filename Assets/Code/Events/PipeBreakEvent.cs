using System;
using UnityEngine;


public class PipeBreakEvent: InteractableBase
{
    [SerializeField] string GameEventName = "PipeBreak";
    [SerializeField] private GameEventType eventType = GameEventType.PipeBreak;
    [SerializeField] private float EventDuration = 60f;
    [SerializeField] private bool isFixed = false;
    public float timeLeft;
    private SoundManager _soundManager;

    public static event Action<string> pipeBreakEventChangedNarrative;

    private void Start()
    {
        _soundManager = SoundManager.Instance;
        if (_soundManager == null)
        {
            Debug.LogError("SoundManager is null");
        }
    }
    
    void Update()
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
    
    public void FailedTask()
    {
        isActive = false;
        Debug.Log("Pipebreak event failed");
    }

    public void ActivateTask()
    {
        pipeBreakEventChangedNarrative?.Invoke("Pluming certification detected. You are able to repair our pipe systems. ");
        transform.localRotation = Quaternion.Euler(0, 180, 0); 
        _soundManager.PlaySound("PipeBurst");
        isActive = true;
        isFixed = false;
        timeLeft = EventDuration;
    }

    protected override void PerformInteraction()
    {
        if (isActive)
        {
            isFixed = true;
            isActive = false;
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            _soundManager.PlaySound("PipeBang");
            Debug.Log("Power Outage event fixed");
        }
    }
}
