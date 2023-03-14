using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ItemSlotClick : MonoBehaviour
{
    // Start is called before the first frame update
    
    private ItemText itemText;
    private Button btnDescription;
    internal Inventory inventory;
    void Start()
    {
        inventory = GameObject.FindObjectOfType<Inventory>();
        btnDescription = GetComponent<Button>();
        btnDescription.onClick.AddListener(ShowDescription);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ShowDescription()
    {
        itemText = GetComponentInChildren<ItemText>();
        // inventory.itemDescription.text = itemText.itemText;
    }
}
