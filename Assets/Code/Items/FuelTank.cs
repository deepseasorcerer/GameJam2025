using UnityEngine;

public class FuelTank : Item
{
    private AudioSource _audioSource;
    private bool isStillWelding = false;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (isInHands && Input.GetMouseButton(0) && Hand == Hand.Left || isInHands && Input.GetMouseButton(1) && Hand == Hand.Right)
        {
            _audioSource.enabled = true;
            if (!isStillWelding)
            {
                _audioSource.time = UnityEngine.Random.Range(0, _audioSource.clip.length);
            }
            isStillWelding = true;
        }
        else
        {
            isStillWelding = false;
            _audioSource.enabled = false;
        }
    }
}
