using UnityEngine;

public class FireExtinguisher : Item
{
    public ParticleSystem foamParticles;

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
