using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backpack : MonoBehaviour
{
    public SlotPrefab slotPrefab;

    public Transform slotsParent;
    public int maxItems = 16;
    internal List<InventoryItem> items;
    internal List<InventoryItem> usedItems;
    internal List<SlotPrefab> slotPrefabs;



    // Start is called before the first frame update
    public virtual void Awake()
    {
        items = new List<InventoryItem>();
        usedItems = new List<InventoryItem>();

        slotPrefabs = new List<SlotPrefab>();
        

        for (int i = 0; i < maxItems; i++) {
            SlotPrefab prefab = Instantiate(slotPrefab, slotsParent);
            slotPrefabs.Add(prefab);
        }
    }

    public virtual void Store(InventoryItem inventoryItem) {
        slotPrefabs[items.Count].SetItem(inventoryItem, this);
        items.Add(inventoryItem);
        inventoryItem.gameObject.SetActive(false);
    }

    public void MarkAsUsed(InventoryItem inventoryItem) {
        usedItems.Add(inventoryItem);

        // Search for the item in inventory and remove it
        foreach(SlotPrefab prefab in slotPrefabs) {
            if (inventoryItem == prefab.item) {
                prefab.gameObject.SetActive(false);
                slotPrefabs.Remove(prefab);
                items.Remove(inventoryItem);

                // add new slot prefab slot to fill the removed slot
                SlotPrefab newPrefab = Instantiate(slotPrefab, slotsParent);
                slotPrefabs.Add(newPrefab);
                break;
            }
        }

        inventoryItem.gameObject.SetActive(false);
    }
}
