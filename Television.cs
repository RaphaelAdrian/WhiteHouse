using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Television : ElectricalComponent 
{
    [Header("TV")]
    public VideoPlayer videoPlayer;
    public Renderer display;
    public GameObject power;
    public Material turnedOnMaterial;
    Material initMaterial;

    void Awake() {
        initMaterial = display.material;
    }

    public override void OnToggle(bool isOn, bool isCircuitBreakerOn)
    {
        base.OnToggle(isOn, isCircuitBreakerOn);

        if (isOn && isCircuitBreakerOn) {
            // videoPlayer.Play();
            GetComponent<AudioSource>().Play();
            power.GetComponent<MeshRenderer>().material.color = Color.green;
            display.material = turnedOnMaterial;
        } else {
            // videoPlayer.Stop();
            GetComponent<AudioSource>().Stop();
            power.GetComponent<MeshRenderer>().material.color = Color.red;
            display.material = initMaterial;
        }

    }
}
