using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FlipPicture : InspectInteractable
{
    bool isFlipped = false;
    bool isInverted = false;
    float minRotationY;
    float maxRotationY;
    public UnityEvent OnFlip;
    public UnityEvent OnFirstTimeFlip;
    public UnityEvent OnInspect;
    private MeshRenderer[] meshChildren;
    Camera cam;
    bool hasFlippedAlready = false;

    
    public override void Awake(){
        base.Awake();
        meshChildren = GetComponentsInChildren<MeshRenderer>();
        cam = GameManager.instance.player.playerCamera;

    }
    void LateUpdate() {
        if (!isInInspectMode) return;
        if (isFlipped) return;
        var direction = (cam.transform.position - transform.position).normalized;
        if (Vector3.Dot(transform.right, direction) > 0.89f) {
            isFlipped = true;
            isInverted = !isInverted;
            Flipped();
        }
    }

    public override void ActivateInteractable(bool activate)
    {
        base.ActivateInteractable(activate);
        isFlipped = false;
        OnInspect.Invoke();
    }

    private void Flipped()
    {
        ExitInspect();
        foreach(MeshRenderer meshChild in meshChildren) {
            meshChild.transform.localEulerAngles += Vector3.up * 180f;
        }
        if (!hasFlippedAlready) OnFirstTimeFlip.Invoke();
        OnFlip.Invoke();
        hasFlippedAlready = true;
    }
}
