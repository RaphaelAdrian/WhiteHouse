
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public Animation anim;
    public Interactable interactable;
    void Start()
    {
        anim = GetComponent<Animation>();
        interactable = GetComponent<Interactable>();
    }

    // Update is called once per frame
    void Update()
    {
        if (interactable.isHovered)
        {
            anim.Play("Chair");
        }

        else if (!interactable.isHovered)
        {
            anim.Stop("Chair");
        }
    }



}
