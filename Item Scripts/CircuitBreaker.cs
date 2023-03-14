using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CircuitBreaker : MonoBehaviour
{
    public Interactable circuitSwitch;

    internal bool isOn = false;
    bool isBroken;
    void Awake()
    {
        GameManager.instance.circuitBreaker = this;
    }

    private void Start()
    {
        circuitSwitch.onActivate += ActivateWithTooltip;
    }

    public void ActivateWithTooltip(bool isActivate)
    {
        if (isBroken) {
            TooltipManager.instance.ShowTooltipMessage("The circuit breaker is not working.");
            return;
        }
        Activate(isActivate);
        UpdateTexts();
    }

    public void Activate(bool isActivate)
    {
        if (isBroken) {
            TooltipManager.instance.ShowTooltipMessage("The circuit breaker is not working.");
            return;
        }
        StartCoroutine(UpdateElectricalComponents());
        isOn = isActivate;
        UpdateState();
    }

    private IEnumerator UpdateElectricalComponents()
    {
        foreach(ElectricalComponent electricalComponent in GameManager.instance.electricalComponentPool.ToList()) {
            electricalComponent.Toggle(electricalComponent.isOn);
            yield return new WaitForSeconds(0.02f);
        }
    }

    private void UpdateState()
    {
        GameManager.instance.isElectricityOn = isOn;
        if(isOn) GameManager.instance.OnCircuitBreakerOn.Invoke();
        else GameManager.instance.OnCircuitBreakerOff.Invoke();
    }

    private void UpdateTexts()
    {
        string displayName = isOn ? "TURN OFF" : "TURN ON";
        string displayMessage = isOn ? "The circuit breaker has been switched on" : "The circuit breaker has been switched off";
        circuitSwitch.UpdateName(displayName);
        TooltipManager.instance.ShowTooltipMessage(displayMessage);
    }

    public void SetBroken(){
        GameManager.instance.isElectricityOn = false;
        Activate(false);
        circuitSwitch.GetComponent<InteractableSaver>().UpdateState(false);
        isBroken = true;
    }

    public void SetBroken(bool isBroken){
        GameManager.instance.isElectricityOn = !isBroken;
        Activate(!isBroken);
        circuitSwitch.GetComponent<InteractableSaver>().UpdateState(!isBroken);
        this.isBroken = isBroken;
    }
}
