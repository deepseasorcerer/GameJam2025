using System;
using Unity.VisualScripting;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    private bool playerIsNear = false;
    [SerializeField] float doorOffset = 0.64f;
    [SerializeField] float doorSpeed = 1f;
    
    private Vector3 leftDoorStartPos;
    private Vector3 rightDoorStartPos;
    private GameObject leftDoor;
    private GameObject rightDoor;

    private SoundManager _soundManager;
    
    private void Awake()
    {
        var doors = transform.childCount;
        Debug.Log(doors);
        leftDoor = transform.GetChild(1).gameObject;
        rightDoor = transform.GetChild(0).gameObject;
        leftDoorStartPos = leftDoor.transform.position;
        rightDoorStartPos = rightDoor.transform.position;
    }

    private void Start()
    {
        _soundManager = SoundManager.Instance;
        if (_soundManager == null)
        {
            Debug.LogError("No SoundManager found in scene");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _soundManager.PlaySound("Door");
            playerIsNear = true;
        } 
    }
    
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsNear = false;
        }
    }
    
    private void Update()
    {
        MoveDoor();
    }

    void MoveDoor()
    {
        var rightGoalPosition = playerIsNear ? rightDoorStartPos + rightDoor.transform.right * doorOffset : rightDoorStartPos;
        var leftGoalPosition = playerIsNear ? leftDoorStartPos + leftDoor.transform.right * -doorOffset : leftDoorStartPos;
        float distanceRight = Vector3.Distance(rightDoor.transform.position, rightGoalPosition);
        if(distanceRight > 0.01f)
        {
            rightDoor.transform.position = Vector3.Lerp(rightDoor.transform.position, rightGoalPosition, doorSpeed * Time.deltaTime);
            leftDoor.transform.position = Vector3.Lerp(leftDoor.transform.position, leftGoalPosition, doorSpeed * Time.deltaTime);
        }
    }
}
