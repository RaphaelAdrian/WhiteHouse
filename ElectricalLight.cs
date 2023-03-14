using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricalLight : ElectricalComponent
{
    [Header("Electrical Light")]
    public Renderer meshWithEmmisiveMaterial;
    public int emmisiveMaterialIndex = 0;
    public float emmisiveIntensity = 1;
    public bool playFlickerSFX = false;
    Light lightObject;
    Material emmisiveMaterial;
    float initIntensity;
    float flickerSpeed = 0.05f;
    AudioSource audioSource;
    AudioClip flickerClip;

    void Awake() {
        if (playFlickerSFX) {
            audioSource = GetComponent<AudioSource>();
            flickerClip = FXSoundSystem.Instance.flickerSFX;
            audioSource.clip = flickerClip;
        }
        

        lightObject = GetComponentInChildren<Light>();
        initIntensity = lightObject.intensity;

        if (meshWithEmmisiveMaterial) {
            emmisiveMaterial = meshWithEmmisiveMaterial.materials[emmisiveMaterialIndex];
        }
    }

    public override void OnToggle(bool isOn, bool isCircuitBreakerOn)
    {
        if (!lightObject) return;

        // when turning on, add flicker
        if (!isCircuitBreakerOn || !isOn) {
            ActivateEmmisiveLight(false);
            lightObject.gameObject.SetActive(false);
        } else {
            lightObject.gameObject.SetActive(true);
            StartCoroutine(StartFlicker());
        }
    }

    public void ForceLightOn(bool isOn) {
        lightObject.gameObject.SetActive(isOn);
        ActivateEmmisiveLight(true);
    }

    IEnumerator StartFlicker()
    {
        int randNumOfFlickers = Random.Range(1, 4);

        while (randNumOfFlickers > 0) {
            lightObject.intensity = initIntensity;
            ActivateEmmisiveLight(true);
            if (playFlickerSFX) audioSource.Play();

            float flickerRateTimer = Random.Range(Mathf.Clamp(flickerSpeed - 0.2f, 0.01f, 1f), flickerSpeed + 0.2f);
            while (flickerRateTimer > 0) {
                flickerRateTimer -= Time.deltaTime;
                yield return null;
            }

            flickerRateTimer = Random.Range(Mathf.Clamp(flickerSpeed - 0.2f, 0.01f, 1f), flickerSpeed + 0.2f);
            lightObject.intensity = 0;
            ActivateEmmisiveLight(false);
            if (playFlickerSFX) audioSource.Stop();


            while (flickerRateTimer > 0) {
                flickerRateTimer -= Time.deltaTime;
                yield return null;
            }

            randNumOfFlickers--;
            yield return null;
        }
        if (playFlickerSFX) audioSource.Play();
        lightObject.intensity = initIntensity;
        ActivateEmmisiveLight(true);
    }

    void ActivateEmmisiveLight(bool activate){
        Color color = activate ? Color.white * emmisiveIntensity : Color.black;
        if (emmisiveMaterial) {
            emmisiveMaterial.SetColor("_EmissiveColor", color);
        }
    }

}
