using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveFalse : MonoBehaviour
{
    // Start is called before the first frame update\

    public GameObject flashBack;
    private Interactable interactable;

    private void Start()
    {
        interactable = FindObjectOfType<Interactable>();
    }
    public void OffGameObject()
    {
     
        this.gameObject.SetActive(false);
    
    }
    public void FlashBack()
    {

            flashBack.SetActive(true);

    }





}
