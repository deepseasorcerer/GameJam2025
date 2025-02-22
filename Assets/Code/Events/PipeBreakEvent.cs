using UnityEngine;


public class PipeBreakEvent: InteractableBase
{
    [SerializeField] string GameEventName = "PipeBreak";
    [SerializeField] private GameEventType eventType = GameEventType.PipeBreak;
    [SerializeField] private float EventDuration = 60f;
    [SerializeField] private bool isFixed = false;
    public float timeLeft;
    private SoundManager _soundManager;

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
        transform.localRotation = Quaternion.Euler(0, 180, 0); 
        _soundManager.PlaySound("PowerOutage");
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
            _soundManager.PlaySound("PowerOn");
            Debug.Log("Power Outage event fixed");
        }
    }
}
