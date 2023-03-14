using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
    public float mouseSensitivity = 100;
    public Transform playerBody;
    float yRotation;
    float xRotation;

    void Start(){
        xRotation = transform.localEulerAngles.x;

        yRotation = playerBody ? 0f : transform.localEulerAngles.y;
    }
    // Start is called before the first frame update

    // Update is called once per frame
    void LateUpdate()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        if (playerBody) {
            playerBody.Rotate(Vector3.up * mouseX);
            yRotation = playerBody.localEulerAngles.y;
        }
        else {
            yRotation += mouseX;
            yRotation = Mathf.Clamp(yRotation, -80f, 90f);
        }
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }
}
    