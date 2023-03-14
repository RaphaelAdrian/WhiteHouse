using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public bool isOn = false;
    public List<ElectricalComponent> controlledElectricalComponents;
    public string switchedOnMessage = "Switched on.";
    public string switchedOffMessage = "Switched off.";
    public string noElectricityMessage = "There is no electricity.";

    SaveState saveState;

    protected Interactable switchInteractable;
    // Start is called before the first frame update
    void Awake()
    {
        switchInteractable = GetComponentInChildren<Interactable>();
        saveState = GetComponent<SaveState>();
        
        foreach (ElectricalComponent electricalComponent in controlledElectricalComponents) {
            electricalComponent.isOn = isOn;
        }
    }
    
    void Start() {
        switchInteractable.onActivate += OnActivate;
    }

    public virtual void OnActivate(bool isActivate) {
        isOn = isActivate;
        FXSoundSystem.Instance.PlayOneShot(FXSoundSystem.Instance.switchSFX);

        if (!GameManager.instance.isElectricityOn) {
            TooltipManager.instance.ShowTooltipMessage(noElectricityMessage);
            return;
        }

        
        if (isOn) {
            TooltipManager.instance.ShowTooltipMessage(switchedOnMessage);
        } else {
            TooltipManager.instance.ShowTooltipMessage(switchedOffMessage);
        }
        
        foreach (ElectricalComponent electricalComponent in controlledElectricalComponents) {
            electricalComponent.Toggle(isOn);
        }
        
    }
}
