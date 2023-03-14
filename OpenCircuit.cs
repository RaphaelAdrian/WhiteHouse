using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCircuit : MonoBehaviour
{
    private Interactable interactable;
    public bool isOpen = false;
    private Animation anim;

    void Start()
    {
        interactable = GetComponent<Interactable>();
        anim = GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        DoorOpen();
    }

    void DoorOpen()
    {
        if (interactable.isHovered && Input.GetKeyDown(KeyCode.F))
        {
            if (!isOpen)
            {
                isOpen = true;
                anim.Play("EB_Door_Open");
                Debug.Log("isOpen");
                interactable.Invoke("Disable", 1f);
            }
            else
            {
                anim.Play("EB_Door_Close");
                Debug.Log("isClose");
                isOpen = false;
            }

        }
    }


}
