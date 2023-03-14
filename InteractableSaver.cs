using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static SlotData;

[RequireComponent(typeof(Interactable))]
public class InteractableSaver : SaveState
{    
    Interactable interactable;

    public UnityEvent ifEnabledOnLoad;
    public UnityEvent ifDisabledOnLoad;
    // Start is called before the first frame update
    public override void Awake()
    {
        interactable = GetComponent<Interactable>();
        base.Awake();
    }

    public override void Start() {
        base.Start();
        interactable.onActivate += ActivateInteractable;
    }

    public override void OnLoadState(ObjectPresence presence)
    {
        base.OnLoadState(presence);
        
        if (presence == ObjectPresence.ENABLED) {
            ifEnabledOnLoad.Invoke();
            interactable.ActivateInteractable(true);
        }
        else if (presence == ObjectPresence.DISABLED){
            ifDisabledOnLoad.Invoke();
            interactable.ActivateInteractable(false);
        } 

    }

    // Update is called once per frame
    public void ActivateInteractable(bool activate)
    {
        UpdateState(activate);
    }
}
