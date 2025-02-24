using System;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PipeBreakEvent: InteractableBase
{
    [SerializeField] string GameEventName = "PipeBreak";
    [SerializeField] private GameEventType eventType = GameEventType.PipeBreak;
    [SerializeField] private float EventDuration = 60f;
    [SerializeField] private bool isFixed = false;
    public float timeLeft;
    private SoundManager _soundManager;
    [SerializeField] private GameObject particleEffect;

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
        SceneManager.LoadScene("DefeatScreen");
    }

    public void ActivateTask()
    {
        pipeBreakEventChangedNarrative?.Invoke("Pluming certification detected. You are able to repair our pipe systems. (weld the broken pipe) ");
        transform.localRotation = Quaternion.Euler(0, 180, 0); 
        _soundManager.PlaySound("PipeBurst");
        isActive = true;
        isFixed = false;
        timeLeft = EventDuration;
        particleEffect.SetActive(true);
    }

    protected override void PerformInteraction()
    {
        if (isActive)
        {
            isFixed = true;
            isActive = false;
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            _soundManager.PlaySound("PipeBang");
            particleEffect.SetActive(false);
            Debug.Log("Power Outage event fixed");
        }
    }
}
