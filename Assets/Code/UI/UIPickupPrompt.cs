using UnityEngine;
using TMPro; // If using TextMeshPro

public class PickupPrompt : MonoBehaviour
{
    private GameObject pickupUIInstance;
    private Transform player;
    [SerializeField] private GameObject pickupUIPrefab;

    public float displayDistance = 3f; // Distance to show UI
    public Vector3 uiOffset = new Vector3(0, 0.7f, 0); // Offset above object

    public PlayerHands playerHands;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHands = player.GetComponent<PlayerHands>();

        if (pickupUIPrefab == null)
        {
            Debug.LogError("Pickup UI Prefab not found!");
        }

    }

    void Update()
    {
        if (player == null || pickupUIPrefab == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= displayDistance)
        {
            ShowUI();
        }
        else
        {
            HideUI();
        }

        if (playerHands.isInHand == true)
        {
            HideUI();
        }

        // Keep UI above the object
        if (pickupUIInstance != null)
        {
            UpdateUIPosition();
        }
    }

    void ShowUI()
    {
        if (pickupUIInstance == null)
        {
            pickupUIInstance = Instantiate(pickupUIPrefab, transform.position + uiOffset, Quaternion.identity);
            pickupUIInstance.transform.SetParent(transform); // Parent to object
            pickupUIInstance.transform.rotation = Quaternion.identity; //Reset the rotation to make sure its not flipped;
            // Optionally, adjust the prefab's local rotation if it’s still flipped
            pickupUIInstance.transform.localRotation = Quaternion.Euler(0, 0f, 0); // Adjust as needed
            Debug.Log("Prompt Spawned");
        }
    }

    void HideUI()
    {
        if (pickupUIInstance != null)
        {
            Destroy(pickupUIInstance); // Deactivate instead of destroying
        }
    }

    void UpdateUIPosition()
    {
        pickupUIInstance.transform.position = transform.position + uiOffset;

        // Make the UI face the player
        Vector3 lookDirection = Camera.main.transform.position - pickupUIInstance.transform.position;
        lookDirection.y = 0; // Ignore vertical rotation

        // Apply LookRotation and counter the potential flip with a manual adjustment
        pickupUIInstance.transform.rotation = Quaternion.LookRotation(lookDirection);

        // Optionally, you can add a small rotation to "flip" it back (e.g., 180 degrees on the Y axis)
        pickupUIInstance.transform.Rotate(0, 180, 0); // This ensures it is not flipped
    }
}
