using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PageSlotPrefab : SlotPrefab
{

    public TMP_Text[] nameTexts;
    public TMP_Text[] descriptionTexts;

    public string descriptionUnlocked = "READ CLUE";
    // Start is called before the first frame update
    
    public override void SetItem(InventoryItem item, Backpack backpack)
    {
        base.SetItem(item, backpack);

        foreach(TMP_Text text in nameTexts) {
            text.text = item.item.itemName;
        }
        foreach(TMP_Text text in descriptionTexts) {
            text.text = descriptionUnlocked;
        }

        GetComponent<Button>().onClick.AddListener(()=>OnClick(item, (Pages)backpack));
    }

    private void OnClick(InventoryItem item, Pages pages)
    {
        pages.displayModal.ModalWindowIn();
        pages.displayImage.sprite = ((PageScriptable)item.item).fullscreenImage;
        GameMenu.instance.EnableToggling(false);
    }
}
