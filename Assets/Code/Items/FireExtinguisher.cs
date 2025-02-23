using System;
using UnityEngine;

public class FireExtinguisher : Item
{
    public ParticleSystem foamParticles;

    private float lastEventTime = 0f;
    public float eventCooldown = 30f;

    public static event Action<string> fireExtinguisherCallChangedNarrative;

    private void Start()
    {
        foamParticles.Stop();
    }

    public override void Use()
    {
        if (!foamParticles.isPlaying)
        {
            foamParticles.Play();
        }

        if (Time.time - lastEventTime >= eventCooldown)
        {
            if (UnityEngine.Random.Range(0, 2500) == 1)
            {
                Debug.Log("Proced fireextin");
                lastEventTime = Time.time;
                fireExtinguisherCallChangedNarrative?.Invoke("Are you trained to spray that way?");
            }
            
        }

        CancelInvoke(nameof(StopFoam));
    }

    private void StopFoam()
    {
        if (foamParticles.isPlaying)
        {
            foamParticles.Stop();
        }
    }

    private void LateUpdate()
    {
        Invoke(nameof(StopFoam), 0.05f);
    }

}
