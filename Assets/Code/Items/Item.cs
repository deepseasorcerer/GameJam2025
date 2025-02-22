using UnityEngine;
using UnityEngine.Serialization;

public class Item : MonoBehaviour
{
    [SerializeField]
    public string itemName;

    public bool Destructable = true;
    public Hand Hand { get; set; }
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
        if (Destructable)
        {
            Destroy(this.gameObject);
        }
    }
}
