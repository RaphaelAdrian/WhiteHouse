using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : TurnOn
{
    // Start is called before the first frame update
    private Light lightChild;

    void Awake()
    {
        ChangeEmission(Color.black);
    }
    // Update is called once per frame
    void Update()
    {
        // if (circuitBreaker.isElectricityOn)
        // {
        //     if (interactable.isHovered && Input.GetKeyDown(KeyCode.F))
        //     {
        //         LampOn(isOn);
        //     }
        // }

        // else
        // {
        //     if (isOn)
        //     {
        //         LampOff();
        //     }
        //     else
        //     {
        //         if (interactable.isHovered && Input.GetKeyDown(KeyCode.F))
        //         {
        //             Debug.Log("No Electricity");
        //             StartCoroutine(ShowInfo("There is No Electricity"));
        //         }

        //     }
        // }
    }

    void LampOn(bool powerOn)
    {
        lightChild = GetComponentInChildren<Light>();

        if (!powerOn)
        {

            lightChild.enabled = true;
            /*animation.enabled = true;*/
            isOn = true;
            ChangeEmission(Color.white);
        }
        else
        {
            lightChild.enabled = false;
            /*animation.enabled = false;*/
            isOn = false;
            ChangeEmission(Color.black);
        }
    }
    void LampOff()
    {
        lightChild.enabled = false;
        /*animation.enabled = false;*/
        isOn = false;
        ChangeEmission(Color.black);

    }


    void ChangeEmission(Color color)
    {
        Material mymat = GetComponent<Renderer>().material;
        mymat.SetColor("_EmissiveColor", color);
    }


}
