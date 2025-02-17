using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    public string itemName;

    public virtual void Use()
    {
        Debug.Log(itemName + " does nothing");
    }
}
