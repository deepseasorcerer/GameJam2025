using UnityEngine;

public class PlayerHands : MonoBehaviour
{
    public Item RightHandItem;
    public Item LeftHandItem;
    [SerializeField] private Transform rightHand;
    [SerializeField] private Transform leftHand;
    
    [SerializeField] private float itemDistance = 0.5f;
    [SerializeField] bool canUseRightHand = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
