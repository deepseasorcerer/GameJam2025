using System;
using UnityEngine;

public class Battery : Item
{
    void Start()
    {
    }

    private void Update()
    {
        if(isInHands)
        {
            //var whichHand = this.gameObject.GetComponentInParent<PlayerHands>();
            transform.localRotation = Quaternion.Euler(0, 45, 45);
           // transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
        else
        {
            //transform.localScale = new Vector3(1f, 1f, 1f);
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
