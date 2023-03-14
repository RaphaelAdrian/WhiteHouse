using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class BlinkingLight : MonoBehaviour
{
    [Range(0.01f, 1.0f)]
    public float flickerSpeed = 0.05f;

    [Range(0.01f, 10.0f)]
    public float idleDuration = 2f;
    private new Light light;

    public bool isFlickerEnabled = true;
    public bool isInvert = false;
    public bool playFlickerSFX = false;
    AudioSource audioSource;
    AudioClip flickerClip;

    float initIntensity;
    
    void Awake() {
        if (playFlickerSFX) {
            audioSource = GetComponent<AudioSource>();
            flickerClip = FXSoundSystem.Instance.flickerSFX;
            audioSource.clip = flickerClip;
        }

        light = GetComponent<Light>();
        initIntensity = light.intensity;
    }
    // Start is called before the first frame update
    void OnEnable()
    {
        StartCoroutine(StartFlicker());
    }

    IEnumerator StartFlicker()
    {
        while (isFlickerEnabled) {
            int randNumOfFlickers = Random.Range(1, 4);

            while (randNumOfFlickers > 0) {
                light.intensity = !isInvert ? initIntensity : 0;
                if (playFlickerSFX) {
                    if (!isInvert) audioSource.Play();
                    else audioSource.Stop();
                }

                float flickerRateTimer = Random.Range(Mathf.Clamp(flickerSpeed - 0.2f, 0.01f, 1f), flickerSpeed + 0.2f);
                while (flickerRateTimer > 0) {
                    flickerRateTimer -= Time.deltaTime;
                    yield return null;
                }

                light.intensity = !isInvert ? 0 : initIntensity;
                if (playFlickerSFX) {
                    if (isInvert) audioSource.Play();
                    else audioSource.Stop();
                }

                flickerRateTimer = Random.Range(Mathf.Clamp(flickerSpeed - 0.2f, 0.01f, 1f), flickerSpeed + 0.2f);
                while (flickerRateTimer > 0) {
                    flickerRateTimer -= Time.deltaTime;
                    yield return null;
                }
                light.intensity = !isInvert ? initIntensity : 0;
                if (playFlickerSFX) {
                    if (!isInvert) audioSource.Play();
                    else audioSource.Stop();
                }

                randNumOfFlickers--;
                yield return null;
            }
            

            float flickerDurationTimer = Random.Range(Mathf.Clamp(idleDuration - 1f, 0.01f, 10f), idleDuration + 1f);
            while (flickerDurationTimer > 0) {
                flickerDurationTimer -= Time.deltaTime;
                yield return null;
            }

            yield return null;
        }
    }
}
