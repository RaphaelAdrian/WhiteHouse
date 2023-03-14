using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SavePresence : MonoBehaviour
{
    internal SlotData.ObjectPresence presence = SlotData.ObjectPresence.UNSET;
    public string uniqueName = "Set Unique Name Here";
    public bool saveOnChangeState = false;
    public bool initiallyDisabled = false;
    public UnityEvent eventIfUnset;
    public UnityEvent eventOnEnable;
    public UnityEvent eventOnDisable;

    bool isAddedToPool = false;

    void Awake()
    {
        if (initiallyDisabled) gameObject.SetActive(false);

        SlotData slotData = Methods.GetCurrentSlot();
        SlotData.ObjectPresence presence = slotData.CheckObjectPresence(uniqueName) ;
        if (presence == SlotData.ObjectPresence.DISABLED) Enable(false);
        else if (presence == SlotData.ObjectPresence.ENABLED) Enable(true);
        else eventIfUnset.Invoke();
    }

    public void SetEnabled(bool isEnable)
    {
        Enable(isEnable);
        if (saveOnChangeState) SaveManager.instance.SaveAll();
    }

    void Enable(bool isEnable) {
        if (!isAddedToPool) {
            SaveManager.instance.objectPresencePool.Add(this);
            isAddedToPool = true;
        }
        this.gameObject.SetActive(isEnable);
        presence = isEnable ? SlotData.ObjectPresence.ENABLED : SlotData.ObjectPresence.DISABLED;

        if (isEnable) eventOnEnable.Invoke();
        else eventOnDisable.Invoke();
    }
}
