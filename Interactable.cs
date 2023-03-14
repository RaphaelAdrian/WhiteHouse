using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SlotData;

[RequireComponent(typeof(Collider))]
public class Interactable : MonoBehaviour
{
    [Header("Interactable")]
    public string interactableName = "";
    public KeyCode activationKey = KeyCode.F;
    public float showAgainDelay = 0f;


    internal bool isHovered = false;
    protected bool isActivated;

    private LayerMask initialLayer;
    private LayerMask activeLayer;
    public delegate void OnActivate(bool activate);
    public OnActivate onActivate;

    bool isPermanentlyDisabled = false;


    Renderer[] renderers;

    public virtual void Awake() {
        renderers = GetComponentsInChildren<Renderer>();
        onActivate = ActivateInteractable;
    }
    public virtual void Start()
    {
        initialLayer = Methods.GetLayer(Globals.instance.interactablesLayer);
        activeLayer = Methods.GetLayer(Globals.instance.activeLayer);
        gameObject.layer = initialLayer;
    }

    
    void Update() {
        if (!isHovered) return;
        if (Input.GetKeyUp(activationKey)) {
            onActivate(!isActivated);
        }   
    }

    public virtual void ActivateInteractable(bool activate) {
        isActivated = activate;
        Debug.Log("Activated " + this.gameObject.name);
        if (showAgainDelay != 0) StartCoroutine(DelayBeforeShowingAgain());
    }

    public void UpdateName(string name) {
        this.interactableName = name;
        TooltipManager.instance.UpdateSelectorTexts();
    }

    private IEnumerator DelayBeforeShowingAgain()
    {
        GameManager.instance.player.GetComponent<InteractableDetector>().Enable(false);
        yield return new WaitForSecondsRealtime(showAgainDelay);
        GameManager.instance.player.GetComponent<InteractableDetector>().Enable(true);
    }

    internal void Enable()
    {
        foreach(Renderer r in renderers) {
            r.gameObject.layer = activeLayer;
        }
        isHovered = true;
        TooltipManager.instance.ShowTooltipSelector(this);
    }

    internal void Disable()
    {
        foreach(Renderer r in renderers) {
            r.gameObject.layer = initialLayer;
        }
        isHovered = false;
        TooltipManager.instance.HideTooltipSelector();
    }
}


