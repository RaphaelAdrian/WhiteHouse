using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(InspectInteractable))]
public class InventoryItem : MonoBehaviour
{
    public enum InventoryItemType {
        ITEM, PAGE
    }
    // Start is called before the first frame update]
    public InventoryItemType itemType = InventoryItemType.ITEM;
    public Items item;
    public float scaleMultiplierOnInventoryDisplay = 1f;

    Inventory inventory;
    Pages pages;

    public bool isObtained = false;
    bool isLoadedFromSavedData = false;
    public UnityEvent OnStoreToInventory;
    void Start()
    {
        inventory = Inventory.instance;
        pages = Pages.instance;
        
        // check if already obtained on the saved data
        // then immediately put it to inventory
        SlotData slotData = Methods.GetCurrentSlot();
        bool isUsed = slotData.CheckIfUsed(this);
        bool isOwned = slotData.CheckIfOwned(this);
        
        // Check if used
        if (isUsed)  {
            inventory.MarkAsUsed(this);
            return;
        }

        // Check if obtained
        if (isOwned) {
            isObtained = true;
        }
        if (isObtained)  {
            isLoadedFromSavedData = true;
            StoreToInventory();
            return;
        }

        GetComponent<InspectInteractable>().OnGetItem.AddListener(OnGetItem);
    }

    public void OnGetItem()
    {
        StoreToInventory();
    }

    private void StoreToInventory(){
        OnStoreToInventory.Invoke();
        if (itemType == InventoryItemType.ITEM){
            inventory.Store(this);
            if (isLoadedFromSavedData) return;
            TooltipManager.instance.ShowTooltipMessage("Stored to Inventory <b>[ESC]</b> ");
        } 
        else if (itemType == InventoryItemType.PAGE) {
            pages.Store(this);
            if (isLoadedFromSavedData) return;
            TooltipManager.instance.ShowTooltipMessage("Stored to Clues");
            Inventory.instance.GoToPages(.4f);
        }
    }
}