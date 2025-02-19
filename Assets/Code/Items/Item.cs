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

    public virtual void Destroy()
    {
        Destroy(this.gameObject);
    }
}
