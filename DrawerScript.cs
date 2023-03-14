using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawerScript : MonoBehaviour
{
    private GameObject drawer;
    private Interactable interactable;
    private bool isOpen;
    private Animation anim;
    public string animOpen;
    public string animClose;
    void Start()
    {
        interactable = GetComponent<Interactable>();
        drawer = this.gameObject;
        anim = GetComponent<Animation>();
    }


    void Update()
    {
        if (interactable.isHovered )
        {
            if (!isOpen && Input.GetKeyDown(KeyCode.F))
            {
                /*transform.position = Vector3.Lerp(transform.position,, Time.deltaTime);*/
                isOpen = true;
                anim.Play(animOpen);
                Debug.Log("IsOpen");
          }
            else if (isOpen && Input.GetKeyDown(KeyCode.F))
            {
                anim.Play(animClose);
                Debug.Log("IsClose");
                isOpen = false;
            }
        }
    }

}

