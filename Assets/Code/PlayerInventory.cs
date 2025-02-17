using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance { get; private set; }

    [SerializeField] private List<Item> itemsInInventory;

    private Item currentItem;
    private int currentIndex;


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

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Q))
        {
            CycleInventory();
        }

    }

    private void CycleInventory()
    {
        currentIndex++;
        if(currentIndex >= itemsInInventory.Count)
        {
            currentIndex = 0;
        }
        else
        {
            Debug.Log("Empty Inventory");
        }

        SetCurrentItem();

    }

    private void SetCurrentItem()
    {
        currentItem = itemsInInventory[currentIndex];
    }


    public List<Item> GetInventoryItemList()
    {
        return itemsInInventory;
    }

}
