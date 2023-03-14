using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodObstacle : Interactable
{
    private Rigidbody rb;
    public override void Start()
    {
        base.Start();
        rb = this.GetComponent<Rigidbody>();
        
    }
    public override void ActivateInteractable(bool activate)
    {
        base.ActivateInteractable(activate);
        rb.isKinematic = false;
        this.enabled = false;
    }


    // Update is called once per frame

}
