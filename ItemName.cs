using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemName : MonoBehaviour
   
{
    public Camera toLookAt;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 v = toLookAt.transform.position - transform.position;
        v.x = v.z = v.y = 0.0f;
        transform.LookAt(toLookAt.transform.position - v);
        transform.Rotate(0, 180, 0);
    }
}
