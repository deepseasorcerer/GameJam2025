using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance { get; private set; }

    [SerializeField] private List<Item> itemsInInventory;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There's more than one PlayerInventory! " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }


    public List<Item> GetInventoryItemList()
    {
        return itemsInInventory;
    }

}
