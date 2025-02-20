using System.Collections;
using UnityEngine;

public class interactableO2Supply : InteractableBase
{
    [SerializeField] const float O2MaxCapacity = 100;
    [SerializeField] float O2LeftAmount = O2MaxCapacity;
    [SerializeField] float O2LossSpeedPerHalfSecond = 0.1f;


    private void Start()
    {
        StartCoroutine(OxygenDepletionCoroutine());
    }

    protected override void PerformInteraction()
    {
        O2LeftAmount += 20;
    }


    private IEnumerator OxygenDepletionCoroutine()
    {
        while (O2LeftAmount > 0)
        {
            O2LeftAmount -= O2LossSpeedPerHalfSecond;

            yield return new WaitForSeconds(0.5f);
        }

    }


}
