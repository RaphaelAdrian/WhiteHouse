using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SparklesEvent : MonoBehaviour
{
    public delegate void PressKeyAction();
    public static PressKeyAction pressKeyAction;


    void Update()
    {
        if(pressKeyAction != null) pressKeyAction();
    }


    // Update is called once per frame

}
