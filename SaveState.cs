using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SlotData;

[RequireComponent(typeof(GuidComponent))]
public class SaveState : MonoBehaviour
{

    bool isAddedToPool = false;
    internal ObjectPresence state = ObjectPresence.UNSET;

    public virtual void Awake() {
        SlotData slotData = Methods.GetCurrentSlot();
        state = slotData.CheckState(this);
    }

    public virtual void Start() {
        if (state != ObjectPresence.UNSET) OnLoadState(state);
    }


    public virtual void OnLoadState(ObjectPresence presence){
        state = presence;
        AddToPool();
    }

    public virtual void UpdateState(bool activate) {
        state = activate ? ObjectPresence.ENABLED : ObjectPresence.DISABLED;
        AddToPool();
    }

    public virtual void UpdateState(ObjectPresence presence) {
        state = presence;
        AddToPool();
    }

    public void AddToPool() {
        if (!isAddedToPool) {
            SaveManager.instance.interactableSaverPool.Add(this);
            isAddedToPool = true;
        }
    }

    public ObjectPresence GetState() {
        return state;
    }

    
    public System.Guid GetGuid(){
        string ID = GetComponent<GuidComponent>().ID;
        string guidString = string.IsNullOrEmpty(ID) ? Guid.Empty.ToString() : ID;
        return Guid.Parse(guidString);
    }
}