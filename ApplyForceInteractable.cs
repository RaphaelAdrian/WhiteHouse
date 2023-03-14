using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ApplyForceInteractable : Interactable
{
    Rigidbody rb;
    public LayerMask defaultLayerMask;
    // Start is called before the first frame update
    public override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }

    public override void ActivateInteractable(bool activate)
    {
        rb.isKinematic = false;
        base.ActivateInteractable(activate);
        Vector3 distance = this.transform.position - GameManager.instance.player.transform.position;
        // Debug.Log(distance.normalized);
        rb.AddForce(distance * 5f, ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision collision)
    {
        if(rb.isKinematic) return;
        if (!collision.gameObject.GetComponent<ApplyForceInteractable>()) return;
        collision.rigidbody.isKinematic = false;
    }

    public void EnableRigidbodies(bool isEnable) {
        this.gameObject.layer = defaultLayerMask;
    }

    
}
