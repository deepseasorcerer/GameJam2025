using UnityEngine;

public class PulsateEffect : MonoBehaviour
{
    public float pulseSpeed = 0.5f;  // Duration of one pulse cycle
    public float pulseAmount = 0.2f; // Scale increase/decrease amount
    private Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;
        StartPulse();
    }

    void StartPulse()
    {
        LeanTween.scale(gameObject, originalScale * (1 + pulseAmount), pulseSpeed)
                 .setEaseInOutSine()
                 .setLoopPingPong(); // Loops back and forth for a smooth pulse
    }
}
