using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampWall : MonoBehaviour
{
    private CircuitBreaker circuitBreaker;
    private new Light light;
    // Start is called before the first frame update
    void Start()
    {
        circuitBreaker = FindObjectOfType<CircuitBreaker>();
        light = this.GetComponentInChildren<Light>();
    }
  

    // Update is called once per frame
    void Update()
    {
        // if(circuitBreaker.isElectricityOn)
        // {
        //     light.enabled = true;
        //     ChangeEmission(Color.white);
        // }
        // if (!circuitBreaker.isElectricityOn)
        // {
        //     light.enabled = false;
        //     ChangeEmission(Color.black);

        // }
    }

    void ChangeEmission(Color color)
    {
        Material mymat = GetComponent<Renderer>().material;

        mymat.SetColor("_EmissiveColor", color);
    }


}
