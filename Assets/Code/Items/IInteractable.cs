using NUnit.Framework;
using UnityEngine;
public interface IInteractable
{
    void Interact(Item item = null);
    void CancelInteraction();

}
