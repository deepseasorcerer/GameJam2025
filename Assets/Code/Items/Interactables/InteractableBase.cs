using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine.UI;

public abstract class InteractableBase : MonoBehaviour, IInteractable
{
    public enum InteractionType { Instant, Hold }
    public InteractionType interactionType = InteractionType.Instant;
    public float holdTime = 5f;
    public bool isActive = false;
    protected bool isInteracting = false;
    private float interactionTimer;
    [SerializeField] private string requiredItem;

    [SerializeField] private float interactionCooldown = 0.5f;
    private float lastInteractionTime = 0f;

    private Item usedItem;

    PlayerHands playerHands;

    [SerializeField] private Image radialIndicatorUi = null;
    
    
    private void Start()
    {
        if (radialIndicatorUi != null)
        {
            
        }
    }
    public void Interact(PlayerHands playerHands, Item usedItem = null)
    {
        this.playerHands = playerHands;
        this.usedItem = usedItem;
        if (!CheckRequiredItems(usedItem))
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
            playerHands.RemoveItemFromHand(usedItem);
            isInteracting = false;
        }
        else if (interactionType == InteractionType.Hold)
        {
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
        if(radialIndicatorUi != null)
        {
            radialIndicatorUi.fillAmount = 0;
        }
        interactionTimer = 0;
    }

    private IEnumerator HoldInteraction()
    {
        while (isInteracting && interactionTimer < holdTime)
        {
            //Debug.Log(interactionTimer);
            interactionTimer += Time.deltaTime;
            if (radialIndicatorUi != null)
            {
                radialIndicatorUi.fillAmount = interactionTimer / holdTime;
            }
            yield return null;
        }

        if (isInteracting)
        {
            PerformInteraction();
            playerHands.RemoveItemFromHand(usedItem);
            if(radialIndicatorUi != null)
            {
                radialIndicatorUi.fillAmount = 0;
            }
            interactionTimer = 0;
        }
    }


    private bool CheckRequiredItems(Item item)
    {
        if (item?.itemName == requiredItem || requiredItem == "" || requiredItem == null)
        {
            //Debug.Log("ItemPresent");
            return true;
        }
        //Debug.Log("ItemNotPresent");
        return false;
    }

    protected abstract void PerformInteraction();
    protected virtual void OnInteractionCancelled() { }
}