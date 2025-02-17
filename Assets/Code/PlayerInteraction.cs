using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float interactionDistance = 3f;

    private List<IInteractable> interactablesInRange = new List<IInteractable>();
    private IInteractable currentInteractable;

    private void Update()
    {
        IInteractable lookedAtInteractable = GetLookedAtInteractable();

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (lookedAtInteractable != null)
            {
                lookedAtInteractable.Interact();
                currentInteractable = lookedAtInteractable;
            }
        }

        if (Input.GetKeyUp(KeyCode.F))
        {
            if (currentInteractable != null)
            {
                currentInteractable.CancelInteraction();
                currentInteractable = null;
            }
        }

        if (currentInteractable != null && lookedAtInteractable != currentInteractable)
        {
            currentInteractable.CancelInteraction();
            currentInteractable = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IInteractable>(out IInteractable interactable))
        {
            interactablesInRange.Add(interactable);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<IInteractable>(out IInteractable interactable))
        {
            interactablesInRange.Remove(interactable);
        }
    }

    private IInteractable GetLookedAtInteractable()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, interactionDistance))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();
            if (interactable != null && interactablesInRange.Contains(interactable))
            {
                return interactable;
            }
        }
        return null;
    }
}