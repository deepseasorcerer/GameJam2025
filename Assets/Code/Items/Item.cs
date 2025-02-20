using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    public string itemName;

    public bool isInHands = false;
    public virtual void Use()
    {
        Debug.Log(itemName + " does nothing");
    
    }

    public void Update()
    {
        if(isInHands)
        {
            this.GetComponent<BoxCollider>().enabled = false;

        }
        else
        {
            this.GetComponent<BoxCollider>().enabled = true;
        }

    }

    public virtual void Destroy()
    {
        Destroy(this.gameObject);
    }
}
