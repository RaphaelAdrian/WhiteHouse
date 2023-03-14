using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : ElectricalComponent
{
    // Start is called before the first frame update
    public GameObject power;
    Vector3 initPowerPos;

    void Awake() {
        initPowerPos = power.transform.position;
    }
    // Update is called once per frame
    public override void OnToggle(bool isOn, bool isCircuitBreakerOn)
    {
        base.OnToggle(isOn, isCircuitBreakerOn);
        if (isOn && isCircuitBreakerOn) {
            GetComponent<AudioSource>().Play();
            power.transform.Translate(0, 0, 0.032f);
        } else {
            GetComponent<AudioSource>().Stop();
            power.transform.position = initPowerPos;
        }
    }
}
