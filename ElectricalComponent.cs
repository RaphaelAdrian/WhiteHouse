using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricalComponent : MonoBehaviour
{
    public bool isOn = true;

    

    void Start() {
        GameManager.instance.electricalComponentPool.Add(this);
        CircuitBreaker circuitBreaker = GameManager.instance.circuitBreaker;
        OnToggle(isOn, circuitBreaker.isOn);
    }

    public virtual void OnToggle(bool isOn, bool isCircuitBreakerOn)
    {
        
    }

    
    public void Toggle() {
        CircuitBreaker circuitBreaker = GameManager.instance.circuitBreaker;
        isOn = !isOn;
        OnToggle(isOn, circuitBreaker.isOn);
    }

    public void Toggle(bool isOn) {
        CircuitBreaker circuitBreaker = GameManager.instance.circuitBreaker;
        this.isOn = isOn;
        OnToggle(this.isOn, circuitBreaker.isOn);
    }
}
