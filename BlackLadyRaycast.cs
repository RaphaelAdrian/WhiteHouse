using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class BlackLadyRaycast : MonoBehaviour
{
    public GameObject player;
    public GameObject blackLady;
    private float sanityGauge;
    private float sanityCoolDown;
    private bool isDetect;
    private float sanityTime;
    public Volume volumetrics;

    private void Start()
    {
        
        sanityCoolDown = 5f;
        sanityGauge = 100;
    }
    void Update()
    {
        volumetrics.weight = Mathf.Abs(sanityGauge-100);
        isDetect = false;

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 100.0f))
        {
            if (player.GetComponent<FlashLightController>().flashLight.isOn)
            {
                if (hit.transform.gameObject.GetComponent<BlackLady>() && blackLady.GetComponent<BlackLady>().isRendererOn)
                {
                    isDetect = true;
                    sanityGauge-= 20*Time.deltaTime;
                    sanityCoolDown = 5f;
                    if (sanityGauge <= 0)
                    {
                        blackLady.transform.SetPositionAndRotation(player.GetComponent<Player>().attackPos.position, Quaternion.identity);
                        GetComponent<TimelineActivator>().PlayTimeline(0);
                        GameManager.instance.isGameOver = true;
                        GameManager.instance.PausePlayerMovements(true);
                    }
                }
            }
        }

        if (sanityGauge != 100)
        {
            if (!isDetect)
            {
                sanityCoolDown-= 1*Time.deltaTime;
                if (sanityCoolDown <= 0)
                {
                    sanityGauge = sanityGauge > 100 ? 100 : sanityGauge+= 4 *Time.deltaTime;
                    sanityCoolDown = 0f;
                }
            }
        }
        // Debug.Log(sanityGauge + "cd" + sanityCoolDown);
    }
}
