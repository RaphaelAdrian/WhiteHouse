using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableDetector : MonoBehaviour
{
    public LayerMask layerMask;
    public float rayLength = 0.5f;

    private Interactable lastHit;

    bool isEnabled = true;

    public Camera cam;

    
    // Start is called before the first frame update
    void Start()
    {
        // mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (!cam || !isEnabled) return;
        RaycastHit hit;

        // Create a vector at the center of our camera's viewport
        Vector3 rayOrigin = cam.ViewportToWorldPoint (new Vector3(0.5f, 0.5f, 0.0f));

        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(rayOrigin, cam.transform.forward, out hit, rayLength, layerMask))
        {
            Interactable interactable = hit.collider.GetComponentInParent<Interactable>();
            if (interactable && !lastHit) {
                interactable.Enable();
                lastHit = interactable;
            } else if (lastHit != interactable){
                lastHit.Disable();
                lastHit = null;
            }

        }
        // if hit exit, then disable interactable
        else if(lastHit){
            lastHit.Disable();
            lastHit = null;
        }
    }

    public void Enable(bool isActivate)
    {
        this.isEnabled = isActivate;
        if (!isActivate) {
            this.lastHit?.Disable();
            this.lastHit = null;
        }
    }
}
