using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Door : InteractableUsingItem
{
    [Header("Door")]
    public bool isOpen;
    public float doorOpenAngle = -90;
    public Door connectedDoor;
    public bool isBroken = false;
    public float openSpeedInSeconds = 0.5f;

    

    private Quaternion initRotation;
    private Quaternion targetRotation;

    internal bool isLocked = false;
    protected SaveState saver;
    public UnityEvent IsBrokenEvent;

    // need to disable dialogues on start due to save system issue

    AudioSource audioSource;
    [Header("Audio Overrides")]
    public AudioClip audioOpenOverride;
    public AudioClip audioCloseOverride;

    AudioClip openingClip;
    AudioClip closingClip;

    public override void Awake(){
        base.Awake();
        initRotation = transform.localRotation;
        targetRotation = initRotation;
        audioSource = GetComponent<AudioSource>();
        if (matchItem) isLocked = true;
        
        saver = GetComponent<SaveState>();
        if (saver && saver.GetState() != SlotData.ObjectPresence.UNSET) {
            SetLocked(false);
        }
        if (string.IsNullOrEmpty(itemNotMatchedMessage)) itemNotMatchedMessage = "You can't use that in here.";
        
        openingClip = audioOpenOverride ? audioOpenOverride : FXSoundSystem.Instance.doorOpening;
        closingClip = audioCloseOverride ? audioCloseOverride : FXSoundSystem.Instance.doorClosing;
    }

    public override void ActivateInteractable(bool activate)
    {
        if (isBroken && !isOpen) {
            TooltipManager.instance.ShowTooltipMessage("This door is broken. I can't open it.");
        }

        base.ActivateInteractable(activate);
        
        if (!isOpen && CheckIfLocked()) {
            if(saver) RevertState();
            TooltipManager.instance.ShowTooltipMessage("Door is Locked. Please use a key.");
            return;
        }
        if (!isBroken) SetOpen(activate);
        if (isBroken) SetOpen(false);
        StartCoroutine(AnimateDoor());
    }

    public virtual bool CheckIfLocked()
    {
        return isLocked;
    }

    protected void RevertState()
    {
        StartCoroutine(RevertSaveState());
    }

    private IEnumerator RevertSaveState()
    {
        yield return new WaitForEndOfFrame();
        saver.UpdateState(SlotData.ObjectPresence.UNSET);   
    }

    public override void OnUseItem()
    {
        base.OnUseItem();
        if (CheckIfLocked() && isMatched) {
            SetLocked(false);
            ActivateInteractable(true);
            if (saver) saver.UpdateState(true);
            if (connectedDoor) connectedDoor.saver.UpdateState(false);
        }
    }

    public void SetLocked(bool locked) {
        this.isLocked = locked;
        isMatched = !locked;
        if (connectedDoor) {
            connectedDoor.isLocked = locked;
            connectedDoor.isMatched = !locked;
        }
    }

    private IEnumerator AnimateDoor()
    {
        float timeElapsed = 0;
        float duration = openSpeedInSeconds;
        Quaternion initialRot = transform.localRotation;
        while(timeElapsed < duration) {
            timeElapsed += Time.deltaTime;

            float time = Easing.Cubic.InOut(timeElapsed / duration);
            transform.localRotation = Quaternion.Lerp(initialRot, targetRotation, time);
            yield return new WaitForEndOfFrame();
        }
        transform.localRotation = targetRotation;
    }

    public void SetOpen(bool open) {
        isOpen = open;
        targetRotation = isOpen 
        ? initRotation * Quaternion.Euler(0, 0, doorOpenAngle)
        : initRotation;

        if (open) audioSource.clip = openingClip;
        else audioSource.clip = closingClip;

        if (!isBroken) audioSource.Play();
    }

    public void SetBroken(bool isBroken){
        this.isBroken = isBroken;
    }

    public void SetBrokenAndClosed(){
        SetBroken(true);
        if (isOpen) ActivateInteractable(false);
    }
}
