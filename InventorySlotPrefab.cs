using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotPrefab : SlotPrefab
{
    public Toggle toggle;

    // Start is called before the first frame update
    
    void Awake() {
        toggle.group = GetComponentInParent<ToggleGroup>();
    }
    public override void SetItem(InventoryItem item, Backpack backpack)
    {
        base.SetItem(item, backpack);
        toggle.onValueChanged.AddListener(delegate (bool isOn){ Select((Inventory)backpack, item, isOn); });
        toggle.interactable = true;
    }

    public void Select(Inventory inventory, InventoryItem item, bool isOn)
    {
        if (isOn) {
            inventory.selectedItem = item;
            MeshRenderer meshRenderer = inventory.inventory3DDisplay.GetComponentInChildren<MeshRenderer>();
            inventory.inventory3DDisplay.gameObject.SetActive(true);
            inventory.displayTextName.text = item.item.itemName;
            inventory.displayTextDescription.text = item.item.itemDescription;
            meshRenderer.materials = item.GetComponent<MeshRenderer>().materials;
            meshRenderer.GetComponent<MeshFilter>().mesh = item.GetComponent<MeshFilter>().mesh;
            meshRenderer.transform.localScale = inventory.init3DDisplayScale * item.scaleMultiplierOnInventoryDisplay;
            toggle.isOn = true;

            // Fix rotation
            InspectInteractable inspectInteractable = item.GetComponent<InspectInteractable>();
            if (inspectInteractable) inventory.inventory3DDisplay.rotation = inspectInteractable.GetDisplayOrientation().eulerAngles;
        }        
    }

    private void OnUseItem(Inventory inventory)
    {
        throw new NotImplementedException();
    }
}
