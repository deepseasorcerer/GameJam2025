using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class InteractableBase : MonoBehaviour, IInteractable
{
    public enum InteractionType { Instant, Hold }
    public InteractionType interactionType = InteractionType.Instant;
    public float holdTime = 5f;

    protected bool isInteracting;
    private float interactionTimer;
    [SerializeField] private List<Item> requiredItems;

    public void Interact()
    {
        if (!CheckRequiredItems())
        {
            return;
        }

        if (!isInteracting)
        {
            isInteracting = true;
            interactionTimer = 0f;           
            if (interactionType == InteractionType.Instant)
            {
                PerformInteraction();
                isInteracting = false;
            }
            else if (interactionType == InteractionType.Hold)
            {
                StartCoroutine(HoldInteraction());
            }
        }
    }

    public void CancelInteraction()
    {
        isInteracting = false;
        StopAllCoroutines();
        OnInteractionCancelled();
    }

    private IEnumerator HoldInteraction()
    {
        while (isInteracting && interactionTimer < holdTime)
        {
            interactionTimer += Time.deltaTime;
            yield return null;
        }

        if (isInteracting)
        {
            PerformInteraction();
        }
        isInteracting = false;
    }

    private bool CheckRequiredItems()
    {
        bool allItemsPresent = requiredItems.All(item => PlayerInventory.Instance.GetInventoryItemList().Contains(item));
        if(allItemsPresent)
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