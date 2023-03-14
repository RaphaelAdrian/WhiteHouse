using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoom : MonoBehaviour
{
    // Start is called before the first frame update


    private float baseFOV;


    void Start()
    {
        baseFOV = Camera.main.fieldOfView;
    }


    void Update()
    {
        if (Input.GetKey(KeyCode.E))
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 50, 0.1f);
        else
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, baseFOV, 0.1f);
    }
}
