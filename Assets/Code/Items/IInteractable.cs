using NUnit.Framework;
using UnityEngine;
public interface IInteractable
{
    void Interact(PlayerHands playerHands,Item item = null);
    void CancelInteraction();

}
