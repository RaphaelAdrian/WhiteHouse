using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SlotData
{
    
    public enum ObjectPresence {
        ENABLED,
        DISABLED,
        UNSET
    }
    public string name;
    public bool isEmpty;
    public int slotNumber;
    public int currentChapterNumber;
    public float[] savePosition;
    public List<int> ownedItems;
    public List<int> ownedPages;
    public List<int> usedItems;
    public Dictionary<Guid, ObjectPresence> savedStates;
    public Dictionary<int, ObjectPresence> savedGameObjectsPresence;
    
    public SlotData(){
        this.name = "Empty Slot";
        this.ownedItems = new List<int>();
        this.ownedPages = new List<int>();
        this.usedItems = new List<int>();
        this.savedGameObjectsPresence = new Dictionary<int, ObjectPresence>();
        this.savedStates = new Dictionary<Guid, ObjectPresence>();
        this.savePosition = new float[3];
        this.currentChapterNumber = 0;
        this.isEmpty = true;
        this.slotNumber = 0;
    }

    public SlotData(SlotData slotData) {
        this.savePosition = slotData.savePosition;
        this.ownedItems = slotData.ownedItems;
        this.ownedPages = slotData.ownedPages;
        this.usedItems = slotData.usedItems;
        this.savedGameObjectsPresence = slotData.savedGameObjectsPresence;
        this.savedStates = slotData.savedStates;
        this.currentChapterNumber = slotData.currentChapterNumber;
        this.name = slotData.name;
        this.slotNumber = slotData.slotNumber;
        this.isEmpty = false;
    }

    
    public bool CheckIfOwned(InventoryItem item) {
        int hash = item.item.itemName.GetHashCode();
        return ownedItems.Contains(hash) || ownedPages.Contains(hash);
    }

    public bool CheckIfUsed(InventoryItem item) {
        int hash = item.item.itemName.GetHashCode();
        return usedItems.Contains(hash);
    }

    public ObjectPresence CheckObjectPresence(string uniqueName) {
        int hash = uniqueName.GetHashCode();
        if (!savedGameObjectsPresence.ContainsKey(hash)) return ObjectPresence.UNSET;
        return savedGameObjectsPresence[hash];
    }

    public ObjectPresence CheckState(SaveState savedState) {
        if (savedState.GetGuid() == Guid.Empty) return ObjectPresence.UNSET;
        if (!savedStates.ContainsKey(savedState.GetGuid())) return ObjectPresence.UNSET;
        return savedStates[savedState.GetGuid()];
    }
}