using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Screenshot))]
public class SaveManager : MonoBehaviour
{
    private static SaveManager _instance;
    public static SaveManager instance { get { return _instance; } }

    int currentLoadSlot = 0;

    internal List<SavePresence> objectPresencePool;
    internal List<SaveState> interactableSaverPool;

    [Header("Debug Purposes")]
    public List<string> guids;

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
        objectPresencePool = new List<SavePresence>();
        interactableSaverPool = new List<SaveState>();
        currentLoadSlot = Methods.GetCurrentSlotNumber();
        LoadSavedData();
    }
    
    private void LoadSavedData()
    {
        SlotData slotData = Methods.GetCurrentSlot();


        // For debug purposes
        foreach (Guid guid in slotData.savedStates.Keys)
        {
            guids.Add(guid.ToString());
        }
    }

    public void SaveSlot(SlotData slot) {
        slot.name = "Saved Slot " + (currentLoadSlot + 1);
        SaveSystem.SaveSlot(slot, currentLoadSlot);
    }

    public void SaveAll(SlotData slotData = null) {
        Player player = GameManager.instance.player;
        if (slotData == null) slotData = Methods.GetCurrentSlot();

        // Position
        slotData.savePosition[0] = player.transform.position.x;
        slotData.savePosition[1] = player.transform.position.y;
        slotData.savePosition[2] = player.transform.position.z;

        slotData.ownedItems = GetItemsOnInventory();
        slotData.ownedPages = GetPagesOnInventory();
        slotData.usedItems = GetUsedItemsOnInventory();
        slotData.savedGameObjectsPresence = GetObjectsPresence();
        slotData.savedStates = GetInteractableState();

        SaveManager.instance.SaveSlot(slotData);

        GetComponent<Screenshot>().TakeScreenshot(slotData.slotNumber);
    }

    public void SaveAll(){
        SaveAll(null);
    }


    private Dictionary<int, SlotData.ObjectPresence> GetObjectsPresence()
    {
        Dictionary<int, SlotData.ObjectPresence> objectPresence = new Dictionary<int, SlotData.ObjectPresence>();
        foreach (SavePresence savedPresence in objectPresencePool)
        {
            objectPresence.Add(savedPresence.uniqueName.GetHashCode(), savedPresence.presence);
        }
        return objectPresence;
    }
    private Dictionary<Guid, SlotData.ObjectPresence> GetInteractableState()
    {
        Dictionary<Guid, SlotData.ObjectPresence> objectPresence = new Dictionary<Guid, SlotData.ObjectPresence>();
        foreach (SaveState savedState in interactableSaverPool)
        {
            objectPresence.Add(savedState.GetGuid(), savedState.state);
        }
        return objectPresence;
    }

    private List<int> GetItemsOnInventory()
    {
        // Inventory
        // Save data to slot
        List<int> itemsOnInventory = new List<int>();
        foreach (InventoryItem inventoryItem in Inventory.instance.items)
        {
            itemsOnInventory.Add(inventoryItem.item.itemName.GetHashCode());
        }
        return itemsOnInventory;
    }
    private List<int> GetUsedItemsOnInventory()
    {
        // Inventory
        // Save data to slot
        List<int> itemsOnInventory = new List<int>();
        foreach (InventoryItem inventoryItem in Inventory.instance.usedItems)
        {
            itemsOnInventory.Add(inventoryItem.item.itemName.GetHashCode());
        }
        return itemsOnInventory;
    }
    private List<int> GetPagesOnInventory()
    {
        // Inventory
        // Save data to slot
        List<int> pagesOnInventory = new List<int>();
        foreach (InventoryItem inventoryItem in Pages.instance.items)
        {
            pagesOnInventory.Add(inventoryItem.item.itemName.GetHashCode());
        }
        return pagesOnInventory;
    }
}
