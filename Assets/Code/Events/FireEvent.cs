using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class FireEvent : InteractableBase
{
    [SerializeField] private List<GameObject> FirePoints;

    [SerializeField] private GameObject firePrefab;
    public void ActivateTask()
    {
        Debug.Log("Fire event activated");
    }

    protected override void PerformInteraction()
    {
        throw new System.NotImplementedException();
    }


}
