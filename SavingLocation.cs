using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SavingLocation : Interactable
{
    public UnityEvent OnSave;
    public override void ActivateInteractable(bool activate)
    {
        base.ActivateInteractable(activate);
        SaveManager.instance.SaveAll();
        OnSave.Invoke();
    }
}
