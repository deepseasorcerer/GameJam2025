using UnityEngine;

public class RedirectShipEvent : InteractableBase
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
