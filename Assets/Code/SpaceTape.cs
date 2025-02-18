using System;
using UnityEngine;

public class SpaceTape : Item, IInteractable
{
    public override void Use()
    {
        Debug.Log("SpaceTape is used to fix things");
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
