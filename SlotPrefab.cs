using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotPrefab : MonoBehaviour
{
    public Image icon;

    internal InventoryItem item;
    // Start is called before the first frame update
     public virtual void SetItem(InventoryItem item, Backpack backpack)
    {
        icon.sprite = item.item.image;
        icon.gameObject.SetActive(true);

        this.item = item;
    }
}
