using System;
using UnityEngine;

[System.Serializable]
public class AirLeakEvent : MonoBehaviour
{
    [SerializeField] string GameEventName = "AirLeak";
    [SerializeField] private GameEventType currentEvent = GameEventType.AirLeak;
    [SerializeField] private float EventDuration = 10f;
    [SerializeField] private bool isActive;
    public float timeLeft;
    [SerializeField] private ParticleSystem airParticles;
    
    private void Awake()
    {
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
        isActive = true;
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
}
