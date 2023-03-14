using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Events;

public class InspectInteractable : Interactable
{
    public bool enableGetItem = true;
    public Vector3 rotationOffset = Vector3.zero;
    public float distanceMultiplier = 0.5f;
    public UnityEvent OnGetItem;
    ParentConstraint constraint;
    Vector3 rotation;
    Vector3 initPos;
    protected Quaternion initRotation;
    LayerMask initLayer;

    LayerMask setToLayer;

    Collider col;

    Vector3 offsetToCenter;
    protected bool isInInspectMode = false;
    Camera cam;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        setToLayer = Methods.GetLayer(Globals.instance.inspectLayer);
        initLayer = gameObject.layer;
        initPos = transform.position;
        rotation = transform.rotation.eulerAngles;
        initRotation = transform.rotation;
        constraint = GetComponent<ParentConstraint>();
        col = GetComponent<Collider>();

        offsetToCenter = transform.position - col.bounds.center;
        cam = GameManager.instance.player.playerCamera;
    }

    public override void ActivateInteractable(bool activate)
    {
        base.ActivateInteractable(activate);
        StartCoroutine(Inspect());
    }

    private IEnumerator Inspect()
    {
        FXSoundSystem.Instance.PlayOneShot(FXSoundSystem.Instance.inspectItemSFX, 0.2f);
        if (constraint) constraint.enabled = false;

        Player player = GameManager.instance.player;
        cam = player.playerCamera;
        
        // some mesh are not in center, so force it

        InspectManager.instance.EnableInspect(true, enableGetItem);
        GameManager.instance.PausePlayerMovements(true);
        RotateToCamera();
        gameObject.layer = setToLayer;
        isInInspectMode = true;

        
        while(isInInspectMode) {
            UpdateObjectRotation();
            transform.position = cam.transform.position + (cam.transform.forward * distanceMultiplier) + offsetToCenter;
            if (Input.GetKeyDown(InspectManager.instance.closeKey)) {
                ExitInspect();
            }
            if (Input.GetKeyDown(InspectManager.instance.getKey) && enableGetItem) {
                InvokeAction();
                ExitInspect();
            }
            yield return null;
        }
        
    }

    private void RotateToCamera()
    {
        transform.LookAt(cam.transform);
        rotation = transform.eulerAngles + rotationOffset;
    }

    public Quaternion GetDisplayOrientation() {
        cam = GameManager.instance.player.playerCamera;
        Vector3 relativePosition = cam.transform.position - transform.position;
        return Quaternion.LookRotation(relativePosition) * Quaternion.Euler(rotationOffset);
    }

    protected void ExitInspect()
    {
        if (constraint) constraint.enabled = true;
        isInInspectMode = false;
        InspectManager.instance.EnableInspect(false, enableGetItem);
        GameManager.instance.PausePlayerMovements(false);
        transform.position = initPos;
        transform.rotation = initRotation;
        gameObject.layer = initLayer;
    }

    private void UpdateObjectRotation()
    {
        Camera cam = GameManager.instance.player.playerCamera;
        rotation.y += Input.GetAxis("Mouse X") * 3;
        rotation.x = rotationOffset.x + Quaternion.Euler(cam.transform.eulerAngles).x;
        // rotation.x += Input.GetAxis("Mouse Y") * 2;
        transform.eulerAngles = rotation;
    }

    public void InvokeAction(){
        FXSoundSystem.Instance.PlayOneShot(FXSoundSystem.Instance.getItemSFX);
        OnGetItem.Invoke();
    }
}
