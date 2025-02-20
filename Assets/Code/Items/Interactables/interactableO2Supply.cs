using System;
using System.Collections;
using UnityEngine;

public class interactableO2Supply : InteractableBase
{
    [SerializeField] const float O2MaxCapacity = 100;
    [SerializeField] float O2LeftAmount = O2MaxCapacity;
    [SerializeField] float O2LossSpeedPerHalfSecond = 0.1f;

    public event Action<float> OxygenChanged;

    private void Start()
    {
        // Optionally, notify UI immediately about the starting value.
        OxygenChanged?.Invoke(O2LeftAmount);
        StartCoroutine(OxygenDepletionCoroutine());
    }

    protected override void PerformInteraction()
    {
        O2LeftAmount += 20;
        // Clamp if needed
        if (O2LeftAmount > O2MaxCapacity)
            O2LeftAmount = O2MaxCapacity;

        // Notify UI after the change.
        OxygenChanged?.Invoke(O2LeftAmount);
    }

    private IEnumerator OxygenDepletionCoroutine()
    {
        while (O2LeftAmount > 0)
        {
            O2LeftAmount -= O2LossSpeedPerHalfSecond;
            // Notify subscribers every time the value updates.
            OxygenChanged?.Invoke(O2LeftAmount);

            yield return new WaitForSeconds(0.5f);
        }
    }


}
