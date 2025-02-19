using System;
using UnityEngine;

public class SpaceTape : Item, IInteractable
{
    private Vector3 _originalPosition;
    private float _timeSinceMoved = 0f;
    private float _timeToReset = 10f;

    private Rigidbody _rb;
    public override void Use()
    {
        Debug.Log("SpaceTape is used to fix things");
    }
    
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _originalPosition = transform.position;
    }
    
    void Update()
    {
        if (!isInHands && Vector3.Distance(_rb.position, _originalPosition) > 0.1f)
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

    public void Interact()
    {
        Debug.Log("SpaceTape is being interacted with");
        throw new System.NotImplementedException();
    }

    public void CancelInteraction()
    {
        throw new System.NotImplementedException();
    }
}
