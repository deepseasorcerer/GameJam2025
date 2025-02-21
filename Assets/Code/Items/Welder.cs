using System;
using UnityEngine;

public class Welder : Item
{
    private Vector3 _originalPosition;
    private float _timeSinceMoved = 0f;
    private float _timeToReset = 10f;

    private Rigidbody _rb;
    
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _originalPosition = transform.position;
    }

    private void Update()
    {
    }
}
