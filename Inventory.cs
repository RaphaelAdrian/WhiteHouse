using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;
using System;
using Michsky.UI.Dark;

public class Inventory : Backpack
{
    public static Inventory instance;

    public Inventory3DDisplay inventory3DDisplay;
    public TMP_Text displayTextName;
    public TMP_Text displayTextDescription;
    public Button useItemButton;
    public MainPanelManager inventoryPanelManager;
    

    internal InventoryItem selectedItem = null;
    internal Vector3 init3DDisplayScale;

    InteractableUsingItem activeInteractableForUseItem;


    public override void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        base.Awake();
        init3DDisplayScale = inventory3DDisplay.GetComponentInChildren<MeshRenderer>().transform.localScale;
    }

    void Start(){
        GameMenu.instance.OnEnableMenu.AddListener(OnEnableMenu);
        useItemButton.GetComponent<Button>().onClick.AddListener(OnUseItem);
    }

    public void OnUseItem()
    {
        if (selectedItem == null || activeInteractableForUseItem == null) return;
        if (activeInteractableForUseItem.CheckIfMatched(selectedItem.item)) {
            // call OnUseItem on interactable
            activeInteractableForUseItem.OnUseItem();

            // Remove from inventory
            if (activeInteractableForUseItem.requireItemOnce) Inventory.instance.MarkAsUsed(selectedItem);

            // set selection to null immediately
            selectedItem = null;
            activeInteractableForUseItem = null;

            //close menu
            GameMenu.instance.EnableMenu(false);
        } else {
            string message = activeInteractableForUseItem.itemNotMatchedMessage;

            if (string.IsNullOrEmpty(message)) message = "You can't use this item in here";
            TooltipManager.instance.ShowTooltipMessage(message, 2f);
        }
    }

    private void OnEnableMenu()
    {
        useItemButton.gameObject.SetActive(false);
        activeInteractableForUseItem = null;
    }

    public void SelectFirstItem() {
        if (items.Count != 0) {
            slotPrefabs[0].GetComponent<Toggle>().onValueChanged.Invoke(true);
        }
    }

    public void ShowInventoryForInteractable(InteractableUsingItem interactable) {
        GameMenu.instance.EnableMenu(true);
        useItemButton.gameObject.SetActive(true);
        activeInteractableForUseItem = interactable;
    }

    public void GoToPages(float delay) {
        StartCoroutine(ShowPagesDelayed(delay));
    }

    private IEnumerator ShowPagesDelayed(float delay = 0)
    {
        yield return new WaitForSecondsRealtime(delay);
        GameMenu.instance.EnableMenu(true);
        inventoryPanelManager.NextPage();
    }
}
