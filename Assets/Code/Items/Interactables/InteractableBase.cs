using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class InteractableBase : MonoBehaviour, IInteractable
{
    public enum InteractionType { Instant, Hold }
    public InteractionType interactionType = InteractionType.Instant;
    public float holdTime = 5f;

    protected bool isInteracting = false;
    private float interactionTimer;
    [SerializeField] private Item requiredItem;

    [SerializeField] private float interactionCooldown = 0.1f;
    private float lastInteractionTime = 0f;

    PlayerHands playerHands;
    public void Interact(PlayerHands playerHands, Item usedItem = null)
    {
        this.playerHands = playerHands;
        if(!CheckRequiredItems(usedItem))
        {
            return;
        }

        if (Time.time - lastInteractionTime < interactionCooldown)
        {
            return;
        }

        lastInteractionTime = Time.time;

        if (interactionType == InteractionType.Instant)
        {
            PerformInteraction();
            Debug.Log(requiredItem);
            playerHands.RemoveItemFromHand(requiredItem);
            isInteracting = false;
        }
        else if (interactionType == InteractionType.Hold)
        {
            Debug.Log("Start");
            if (isInteracting)
            {
                return;
            }
            isInteracting = true;
            StartCoroutine(HoldInteraction());

        }

    }

    public void CancelInteraction()
    {
        isInteracting = false;
        StopAllCoroutines();
        OnInteractionCancelled();
        interactionTimer = 0;
    }

    private IEnumerator HoldInteraction()
    {
        while (isInteracting && interactionTimer < holdTime)
        {
            Debug.Log(interactionTimer);
            interactionTimer += Time.deltaTime;
            yield return null;
        }

        if (isInteracting)
        {
            PerformInteraction();
            playerHands.RemoveItemFromHand(requiredItem);
            interactionTimer = 0;
        }
    }


    private bool CheckRequiredItems(Item item)
    {
        if (requiredItem == item || requiredItem == null)
        {
            Debug.Log("ItemPresent");
            return true;
        }
        Debug.Log("ItemNotPresent");
        return false;
    }

    protected abstract void PerformInteraction();
    protected virtual void OnInteractionCancelled() { }
}