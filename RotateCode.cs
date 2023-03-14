using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RotateCode : Interactable
{
    public static event Action<string, int> Rotated = delegate { };
    private bool coroutineAllow;
    private int number;
    
   public override void Start()
    {
        base.Start();
        coroutineAllow = true;
        number = 10;
    }


    public override void ActivateInteractable(bool activate)
    {
        base.ActivateInteractable(activate);
        if (coroutineAllow)
        {
            StartCoroutine("RotateWheel");
        }
    }



    private IEnumerator RotateWheel()
    {
        coroutineAllow = false;
        for (int i = 0; i <= 11; i++)
        {
            transform.Rotate(0f, 3f, 0f);
            yield return new WaitForSeconds(0.01f);
        }
        coroutineAllow = true;
        number += 1;

        if (number > 10)
        {
            number = 1;
        }

        Rotated(name, number);

    }
}
