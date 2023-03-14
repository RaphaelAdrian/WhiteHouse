using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory3DDisplay : MonoBehaviour
{
    internal Vector3 rotation;


    // Start is called before the first frame update
    void Start()
    {
        rotation = transform.rotation.eulerAngles;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        UpdateObjectRotation();
    }

    private void UpdateObjectRotation()
    {
        rotation.y += Input.GetAxis("Mouse X") * 3;
        // rotation.x += Input.GetAxis("Mouse Y") * 2;
        transform.eulerAngles = rotation;
    }
}
