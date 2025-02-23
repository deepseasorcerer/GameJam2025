using System;using NUnit.Framework;
using UnityEngine;

public enum Hand
{
    Left,
    Right
}

public class PlayerHands : MonoBehaviour
{
    [SerializeField] public Item rightHandItem;
    [SerializeField] public Item leftHandItem;
    [SerializeField] private Transform rightHand;
    [SerializeField] private Transform leftHand;
    [SerializeField] private float pickupRange = 2f;
    [SerializeField] bool canUseRightHand = true;
    [SerializeField] float timeBetweenInteractions = 1f;

    private InteractableBase currentHoldInteractable;

    private Rigidbody rightRb;
    private Rigidbody leftRb;
    private float timeSinceInteraction = 0f;
    private KeyCode currentHoldKey;

    private Camera mainCamera;

    public bool isInHand = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceInteraction += Time.deltaTime;

        if (Input.GetKey(KeyCode.Mouse0))
        {
            ProcessLeftClick();
        }

        if (Input.GetKey(KeyCode.Mouse1))
        {
            ProcessRightClick();
        }

        CheckHoldInteraction();

        if (Input.GetKeyDown(KeyCode.Q))
        {
            DropItem(ref leftHandItem);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            DropItem(ref rightHandItem);
        }

        leftHand.position = mainCamera.transform.position + mainCamera.transform.right * -0.6f + mainCamera.transform.forward * 0.9f + Vector3.up * -0.6f;
        rightHand.position = mainCamera.transform.position + mainCamera.transform.right * 0.6f + mainCamera.transform.forward * 0.9f + Vector3.up * -0.6f;

        leftHand.rotation = Quaternion.LookRotation(mainCamera.transform.forward);
        rightHand.rotation = Quaternion.LookRotation(mainCamera.transform.forward);

    }

    private void ProcessLeftClick()
    {
        PerformInteraction(KeyCode.Mouse0, ref leftHandItem, leftHand, Hand.Left);
    }

    private void ProcessRightClick()
    {
        PerformInteraction(KeyCode.Mouse1, ref rightHandItem, rightHand, Hand.Right);
    }

    private void PerformInteraction(KeyCode key, ref Item handItem, Transform handTransform, Hand hand)
    {
        Debug.DrawRay(mainCamera.transform.position, mainCamera.transform.forward * pickupRange, Color.red, pickupRange);
        Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (handItem != null && timeSinceInteraction >= timeBetweenInteractions)
        {
            // Try interacting with objects
            if (Physics.Raycast(ray, out hit, pickupRange))
            {
                if (hit.collider.CompareTag("Interactable"))
                {
                    InteractableBase hitObject = hit.collider.GetComponent<InteractableBase>();
                    hitObject.Interact(this, handItem);

                    if (hitObject.interactionType == InteractableBase.InteractionType.Hold)
                    {
                        currentHoldInteractable = hitObject;
                        currentHoldKey = key;
                    }
                    return;
                }
            }

            // Use the item in hand
            handItem.Use();
            timeSinceInteraction = 0f;
            return;
        }

        // Try picking up a new item if the hand is empty
        if (Physics.Raycast(ray, out hit, pickupRange))
        {
            if (hit.collider.CompareTag("Item"))
            {
                Item hitItem = hit.collider.GetComponent<Item>();
                if (hitItem != null && hitItem != leftHandItem && hitItem != rightHandItem)
                {
                    AddItemToHand(hitItem, ref handItem, handTransform, hand);
                }
            }
            else if (hit.collider.CompareTag("Interactable"))
            {
                InteractableBase hitObject = hit.collider.GetComponent<InteractableBase>();
                hitObject.Interact(this, handItem);

                if (hitObject.interactionType == InteractableBase.InteractionType.Hold)
                {
                    currentHoldInteractable = hitObject;
                    currentHoldKey = key;
                }
                return;
            }
        }
    }

    private void CheckHoldInteraction()
    {
        if (currentHoldInteractable == null)
            return;

        Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        bool objectStillInView = Physics.Raycast(ray, out hit, pickupRange+2) && hit.collider.gameObject == currentHoldInteractable.gameObject;
        //bool objectInRange = Vector3.Distance(transform.position, currentHoldInteractable.transform.position) <= pickupRange;
        bool isKeyStillPressed = Input.GetKey(currentHoldKey);

        if (!objectStillInView || !isKeyStillPressed)
        {
            Debug.Log("Hold interaction cancelled");
            currentHoldInteractable.CancelInteraction();
            currentHoldInteractable = null;
        }
    }

    private void AddItemToHand(Item item, ref Item handItem, Transform handTransform, Hand hand)
    {
        if (handItem != null)
        {
            return;
        }

        isInHand = true;
        handItem = item; // Assign to correct hand variable
        item.isInHands = true;
        handItem.Hand = hand;
        timeSinceInteraction = 0f;
        Debug.Log("Picked up " + item.name);

        Rigidbody itemRb = item.GetComponent<Rigidbody>();
        if (itemRb)
        {
            itemRb.useGravity = false;
            itemRb.isKinematic = true;
        }

        item.transform.SetParent(handTransform);
        item.transform.localPosition = Vector3.zero;
        item.transform.localRotation = Quaternion.identity;
    }

    public void DropItem(ref Item handItem)
    {
        if (handItem == null) return;

        Rigidbody itemRb = handItem.GetComponent<Rigidbody>();
        if (itemRb)
        {
            itemRb.useGravity = true;
            itemRb.isKinematic = false;
        }
        isInHand = false;
        handItem.isInHands = false;
        handItem.transform.SetParent(null);
        handItem = null;
        timeSinceInteraction = 0f;
    }


    public void RemoveItemFromHand(Item item)
    {
        if (item == null) return;

        if (leftHandItem == item)
        {
            if(item.Destructable)
            {
                leftHandItem.Destroy();
                leftHandItem = null;
            }
        }
        else if (rightHandItem == item)
        {
            if(item.Destructable)
            {
                rightHandItem.Destroy();
                rightHandItem = null;
            }
        }
    }
}

