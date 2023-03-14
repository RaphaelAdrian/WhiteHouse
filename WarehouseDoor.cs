using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarehouseDoor : MonoBehaviour
{
    private Rigidbody  rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

  
    void Update()
    {
        if (this.GetComponent<Interactable>())
        {
            Interactable interactable = this.GetComponent<Interactable>();
            if (interactable.isHovered && Input.GetKeyDown(KeyCode.F))
            { rb.isKinematic = false; }
        }
            


    }
}
