using System;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class AirLeakEvent : InteractableBase
{
    [SerializeField] string GameEventName = "AirLeak";
    [SerializeField] private GameEventType eventType = GameEventType.AirLeak;
    [SerializeField] private float EventDuration = 10f;
    [SerializeField] private bool isFixed = false;
    public float timeLeft;
    [SerializeField] private ParticleSystem airParticles;
    private AudioSource audioSource;
    
    [SerializeField] private GameObject fixedTape;

    public static event Action<string> airLeakEventChangedNarrative;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        isActive = false;
    }

    void Start()
    {
        airParticles = GetComponent<ParticleSystem>();
        StopParticleSystem();
        timeLeft = EventDuration;
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
        StopParticleSystem();
        Debug.Log("Air leak event failed");
        //TODO Failed Screen
    }

    public void ActivateTask()
    {
        airLeakEventChangedNarrative?.Invoke("A breach on the ventilation system is occurring. Grab the avian adhesive to repair. \r\n...\r\nWhat? I'm 87% certain that's how it's pronounced.");
        isActive = true;
        isFixed = false;
        audioSource.enabled = true;
        fixedTape.SetActive(false);
        StartParticleSystem();
        timeLeft = EventDuration;
    }
    
    private void StartParticleSystem()
    {
        airParticles.Play();
    }
    
    private void StopParticleSystem()
    {
        airParticles.Stop();
    }

    public new void Interact(PlayerHands playerHands, Item item = null)
    {
        if (item is SpaceTape)
        {
            Debug.Log("worked!");
        }
    }

    public void CancelInteraction()
    {
        throw new NotImplementedException();
    }

    protected override void PerformInteraction()
    {
        if (isActive)
        {
            isFixed = true;
            StopParticleSystem();
            isActive = false;
            audioSource.enabled = false;
            fixedTape.SetActive(true);
            Debug.Log("Air leak event fixed");
        }
    }
}
