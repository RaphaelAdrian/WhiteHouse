using System;
using UnityEngine;

public class FlashLight : MonoBehaviour
{
    internal bool isOn = true;
    void Start(){

    }

    public bool Toggle(){
        isOn = !isOn;
        gameObject.SetActive(isOn);
        return isOn;
    }

    public void Toggle(bool on){
        gameObject.SetActive(on);
        isOn = on;
    }
}
