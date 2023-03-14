using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class FlashLightController : MonoBehaviour
{
    [SerializeField] private bool initiallyOn = false;
    public FlashLight flashLight;
    public Transform flashLightAim;
    public bool isUnlocked;
    public Transform shoulder;
    Vector3 initShoulderRot;
    Player player;
    float animationLayerWeight;
    float targetLayerWeight;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
        initShoulderRot = shoulder.transform.eulerAngles;
        flashLight.Toggle(false);

        if(initiallyOn) ToggleFlashlight();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.isPlayerMovementPaused) return;
        // if right clicked, then toggle flash light
        if (Input.GetMouseButtonDown(1) && isUnlocked)
        {
            ToggleFlashlight();
        }
        
        animationLayerWeight = Mathf.Lerp(animationLayerWeight, targetLayerWeight, 0.2f);
        player.animator.SetLayerWeight(1, animationLayerWeight);
    }

    public void ToggleFlashlight(){
        bool isOn = flashLight.Toggle();
        float weight = isOn ? 0.9f : 0; 
        targetLayerWeight = weight;
    }

    public void ToggleFlashlight(bool on){
        flashLight.Toggle(on);
        targetLayerWeight = on ? 0.9f : 0;
    }

    // Disables flashlight immediately, including the script
    public void DisableFlashlight(){
        player.animator.SetLayerWeight(1, 0);
        flashLight.Toggle(false);
        this.enabled = false;
    }

    public void DisableFlashlight(bool isDisable){
        player.animator.SetLayerWeight(1, isDisable ? 0 : 0.9f);
        flashLight.Toggle(!isDisable);
        this.enabled = !isDisable;
    }


    void LateUpdate() {
        if (flashLight.isOn) {
            // Shoulder must follow camera rotation
            // Vector3 rotation = shoulder.transform.localEulerAngles;
            // rotation.x = player.playerCamera.transform.localEulerAngles.x * -1;
            // shoulder.transform.localEulerAngles = rotation;

            // Vector3 direction = (flashLight.transform.position - shoulder.transform.position).normalized;
            // Vector3 lookRotation = Quaternion.LookRotation(direction).eulerAngles;
            // lookRotation.z = shoulder.transform.rotation.z;
            // shoulder.eulerAngles = lookRotation;

            Vector3 rotation = flashLightAim.transform.eulerAngles + Vector3.up * 180 + Vector3.forward * 90;
            Vector3 direction = (flashLightAim.position - shoulder.transform.position).normalized;

            rotation.x = -Quaternion.LookRotation(direction).eulerAngles.x;
            shoulder.transform.eulerAngles = rotation;

            // Vector3 lookRotation = Quaternion.LookRotation(direction).eulerAngles;
            // lookRotation.z = shoulder.transform.rotation.z;
            // shoulder.eulerAngles = lookRotation;
        }
    }

    public void IsUnlock()
    {
        isUnlocked = true;
    }
}
