using UnityEngine;


public class FuelThrustersEvent : InteractableBase
{
    public void ActivateTask()
    {
        Debug.Log("Fire event activated");
    }

    protected override void PerformInteraction()
    {
        throw new System.NotImplementedException();
    }
}
