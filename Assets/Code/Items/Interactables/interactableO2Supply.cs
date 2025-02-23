using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class interactableO2Supply : InteractableBase
{
    [SerializeField] const float O2MaxCapacity = 100;
    [SerializeField] float O2LeftAmount = O2MaxCapacity;
    [SerializeField] float O2LossSpeedPerHalfSecond = 0.1f;

    public static event Action<float> OxygenChanged;

    public static event Action<string> OxygenChangedNarrative;

    private bool hasTriggeredLowOxygenNarrative = false;

    [SerializeField] AudioSource o2Sound;


    private void Start()
    {
        OxygenChanged?.Invoke(O2LeftAmount);
        StartCoroutine(OxygenDepletionCoroutine());

    }

    private void OnDestroy()
    {

    }

    protected override void PerformInteraction()
    {
        o2Sound.Play();
        O2LeftAmount += 60;
        if (O2LeftAmount > O2MaxCapacity)
            O2LeftAmount = O2MaxCapacity;

        OxygenChanged?.Invoke(O2LeftAmount);
        OxygenChangedNarrative?.Invoke("Adequate. Only 487 internal gas cycles to go.\r\nI told them to automate it. Something about LAH800 malfunctions.");

    }

    private IEnumerator OxygenDepletionCoroutine()
    {
        while (O2LeftAmount > 0)
        {
            O2LeftAmount -= O2LossSpeedPerHalfSecond;
            OxygenChanged?.Invoke(O2LeftAmount);
            if (O2LeftAmount <= 30 && !hasTriggeredLowOxygenNarrative)
            {
                OxygenChangedNarrative?.Invoke("The green ones are compressed oxygen. Of course that makes sense. Re-calibrating sensors... Are o2 levels critically low?\r\nJust focus on grabbing the green one. Not orange. That's compressed custard.");
                hasTriggeredLowOxygenNarrative = true;
            }

            if (O2LeftAmount > 35)
            {
                hasTriggeredLowOxygenNarrative = false;
            }

            if (O2LeftAmount <= 0)
            {
                SceneManager.LoadScene("DefeatScreen");
            }

            yield return new WaitForSeconds(0.5f);
        }

        if(O2LeftAmount <= 0)
        {
            SceneManager.LoadScene("DefeatScreen");
        }
    }

    


}
