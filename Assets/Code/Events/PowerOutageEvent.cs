﻿
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PowerOutageEvent : InteractableBase
{
    
    [SerializeField] string GameEventName = "PowerOutage";
    [SerializeField] private GameEventType eventType = GameEventType.PowerOutage;
    [SerializeField] private float EventDuration = 60f;
    [SerializeField] private bool isFixed = false;

    [SerializeField] private GameObject fixedBattery;
    public float timeLeft;
    private SoundManager _soundManager;
    public static event Action<string> powerOutageEventChangedNarrative;
    private void Start()
    {
        _soundManager = SoundManager.Instance;
        if (_soundManager == null)
        {
            Debug.LogError("SoundManager is null");
        }
        fixedBattery.SetActive(false);
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
        else
        {
            fixedBattery.SetActive(true);
        }
    }
    
    public void FailedTask()
    {
        isActive = false;
        SceneManager.LoadScene("DefeatScreen");
    }

    public void ActivateTask()
    {
        powerOutageEventChangedNarrative?.Invoke("My ocular receptors are dark vision compatible. This is a human frailty.\r\nThe switch is where you would expect. (replace battery)");
        _soundManager.PlaySound("PowerOutage");
        fixedBattery.SetActive(false);
        isActive = true;
        isFixed = false;
        timeLeft = EventDuration;
    }

    protected override void PerformInteraction()
    {
        if (isActive)
        {
            fixedBattery.SetActive(true);
            isFixed = true;
            isActive = false;
            _soundManager.PlaySound("PowerOn");
            Debug.Log("Power Outage event fixed");
        }
    }
}
