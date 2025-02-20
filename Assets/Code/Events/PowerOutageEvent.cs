
using UnityEngine;

public class PowerOutageEvent : InteractableBase
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
