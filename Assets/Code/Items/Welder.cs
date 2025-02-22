using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Welder : Item
{
    private Vector3 _originalPosition;
    private float _timeSinceMoved = 0f;
    private float _timeToReset = 30f;
    
    [SerializeField] private GameObject flame;
    
    private Rigidbody _rb;
    private AudioSource _audioSource;
    
    private bool isStillWelding = false;
        
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _rb = GetComponent<Rigidbody>();
        Destructable = false;
        _originalPosition = transform.position;
    }

    private void Update()
    {
        
        if(isInHands && Input.GetMouseButton(0) && Hand == Hand.Left || isInHands && Input.GetMouseButton(1) && Hand == Hand.Right)
        {
            _audioSource.enabled = true;
            if(!isStillWelding)
            {
                _audioSource.time = UnityEngine.Random.Range(0, _audioSource.clip.length);
            }
            isStillWelding = true;
            flame.SetActive(true);
        }
        else
        {
            isStillWelding = false;
            _audioSource.enabled = false;
            flame.SetActive(false);
        }
        
        if (!isInHands && Vector3.Distance(_rb.position, _originalPosition) > 0.3f)
        {
            _timeSinceMoved += Time.deltaTime;
            if (_timeSinceMoved >= _timeToReset)
            {
                _rb.angularVelocity = Vector3.zero;
                _rb.linearVelocity = Vector3.zero;
                transform.position = _originalPosition;
                _timeSinceMoved = 0f;
            }
        }
    }
}
