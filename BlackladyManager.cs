using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BlackladyManager : MonoBehaviour
{
    [Header("Ouija Quest Needed items")]
    public InventoryItem[] items;
    public UnityEvent OnCompleted;

    int itemsCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        foreach(InventoryItem item in items) {
            item.OnStoreToInventory.AddListener(OnGetItem);
        }
    }

    private void OnGetItem()
    {
        itemsCount++;
        Debug.Log("Got item: ");
        if (itemsCount == items.Length) {
            OnCompleted.Invoke();
        }
    }
}
