using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeToDaylight : MonoBehaviour
{
    public Light nightDirectionalLight;
    public Light dayDirectionalLight;
    public GameObject daylightVolume;
    public void Activate(){
        nightDirectionalLight.gameObject.SetActive(false);
        dayDirectionalLight.gameObject.SetActive(true);
        daylightVolume.SetActive(true);
    }
}
