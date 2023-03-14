using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTest : MonoBehaviour
{
    public Material defaultMat;
    public Material onClick;
    void Start()
    {
        defaultMat = GetComponent<Material>();
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            if (Physics.Raycast(ray, out hit, Mathf.Infinity,5))
            {
                this.gameObject.GetComponent<MeshRenderer>().material = onClick;
            }
        }
    }
}
