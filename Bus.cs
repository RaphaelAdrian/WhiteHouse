using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bus : MonoBehaviour
{
    public float speed = 3f;
    public Transform busInitPos;
    public Transform triggerPos;

    bool isBreak;
    Rigidbody rb;
    int index;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();

        if (busInitPos) {
            transform.position = busInitPos.position;
        }
    }

    void FixedUpdate(){
        rb.velocity = new Vector3(speed, rb.velocity.y, rb.velocity.z);

        if (isBreak) {
            speed = Mathf.Lerp(speed, 0, 0.05f);

            if (speed <= 0) {
                // this.gameObject.SetActive(false);
            }
        }
    }
    
    void Update() {
        if (Input.GetKeyUp(KeyCode.Equals)) {
            transform.position = triggerPos.position;
        }
    }

    public void Stop(){
        isBreak = true;    
    }

}
