using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableUsingItem : Interactable
{   
    public Items matchItem;
    public bool requireItemOnce = true;
    public string itemNotMatchedMessage;
    public UnityEvent OnBeforeShowInventory;
    public UnityEvent OnUseSuccess;
    protected bool isMatched;


    public override void ActivateInteractable(bool activate)
    {
        base.ActivateInteractable(activate);
        OnBeforeShowInventory.Invoke();
        if (!matchItem) return;
        if(isMatched && requireItemOnce) return;
        Inventory.instance.ShowInventoryForInteractable(this);
    }

    public virtual void OnUseItem(){
        OnUseSuccess.Invoke();
        isMatched = true;
    }

    public virtual bool CheckIfMatched(Items item)
    {
        return item == this.matchItem;
    }
}
