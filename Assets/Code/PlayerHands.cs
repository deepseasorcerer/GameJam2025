using UnityEngine;

public class PlayerHands : MonoBehaviour
{
    public Item RightHandItem;
    public Item LeftHandItem;
    [SerializeField] private Transform rightHand;
    [SerializeField] private Transform leftHand;
    [SerializeField] private float pickupRange = 2f;
    [SerializeField] bool canUseRightHand = true;
    [SerializeField] float timeBetweenInteractions = 1f;

    
    private Rigidbody rightRb;
    private Rigidbody leftRb;
    private float timeSinceInteraction = 0f; 
    
    private Camera mainCamera;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceInteraction += Time.deltaTime;
        if(Input.GetMouseButton(0))
        {
            ProcessLeftClick();
        }
        if(Input.GetMouseButton(1) && canUseRightHand)
        {
            ProcessRightClick();
        }
        if (LeftHandItem != null)
        {
            LeftHandItem.transform.position = leftHand.position;
            LeftHandItem.transform.rotation = leftHand.rotation;
        }
        if (RightHandItem != null)
        {
            RightHandItem.transform.position = rightHand.position;
            RightHandItem.transform.rotation = rightHand.rotation;
        }
    }

    private void ProcessLeftClick()
    {
        if(LeftHandItem != null && timeSinceInteraction >= timeBetweenInteractions)
        {
            DropLeftHandItem();
        }
            
        Debug.DrawRay(mainCamera.transform.position, mainCamera.transform.forward * pickupRange, Color.red, pickupRange);
            
        Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, pickupRange) && timeSinceInteraction >= timeBetweenInteractions)
        {
            var hitObject = hit.collider.GetComponent<Item>();
            if (hitObject != null)
            {
                hitObject.isInHands = true;
                timeSinceInteraction = 0f;
                Item itemHit = hitObject;
                Debug.Log("picked up " + itemHit.name);    
                LeftHandItem = itemHit;
                leftRb = LeftHandItem.GetComponent<Rigidbody>();
                leftRb.useGravity = false;
                leftRb.isKinematic = true;
            }
        }
    }
    
    private void ProcessRightClick()
    {
        if(RightHandItem != null && timeSinceInteraction >= timeBetweenInteractions)
        {
            DropRightHandItem();
        }
            
        Debug.DrawRay(mainCamera.transform.position, mainCamera.transform.forward * pickupRange, Color.green, pickupRange);
            
        Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, pickupRange) && timeSinceInteraction >= timeBetweenInteractions)
        {
            var hitObject = hit.collider.GetComponent<Item>();
            if (hitObject != null)
            {
                hitObject.isInHands = true;
                timeSinceInteraction = 0f;
                Item itemHit = hitObject;
                Debug.Log("picked up " + itemHit.name);    
                RightHandItem = itemHit;
                rightRb = RightHandItem.GetComponent<Rigidbody>();
                rightRb.useGravity = false;
                rightRb.isKinematic = true;
            }
        }
    }
    
    private void DropRightHandItem()
    {
        //RightHandItem.transform.position = mainCamera.transform.position + mainCamera.transform.forward * 2;
        //RightHandItem.transform.rotation = Quaternion.identity;
        RightHandItem.isInHands = false;
        rightRb.useGravity = true;
        rightRb.isKinematic = false;
        rightRb = null;
        RightHandItem = null;
        timeSinceInteraction = 0f;
    }
    
    private void DropLeftHandItem()
    {    
        //LeftHandItem.transform.position = mainCamera.transform.position + mainCamera.transform.forward * 2;
        //LeftHandItem.transform.rotation = Quaternion.identity;
        LeftHandItem.isInHands = false;
        leftRb.useGravity = true;
        leftRb.isKinematic = false;
        LeftHandItem = null;
        leftRb = null;
        timeSinceInteraction = 0f;  
    }
}
