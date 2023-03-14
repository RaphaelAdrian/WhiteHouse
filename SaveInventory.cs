using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryItems
{
    public Items item; 
}

public class SaveInventory : MonoBehaviour
{
    // public Inventory inventory;
    // private Interactable interactable;

    // void Start()
    // {
    //     interactable = GetComponent<Interactable>();
    // }
    // public void Update()
    // {
    //     if (interactable.isHovered)
    //     {
    //         if (Input.GetKeyDown(KeyCode.F))
    //         {
    //             int currentLoadSlot = PlayerPrefs.GetInt("CurrentSlot", 0);
    //             SlotData slotData = SaveSystem.LoadSlot(currentLoadSlot);
    //             for (int i = 0; i <= inventory.emptySlot-1; i++)
    //             {
    //                 /*slotData.ownedItems[i] = inventory.storedItems[i].item;*/
    //             } 

    //             SaveSystem.SaveSlot(slotData, currentLoadSlot);
    //         }
    //     }
    // }
    // public void SavePages()
    // {

    // }

}
