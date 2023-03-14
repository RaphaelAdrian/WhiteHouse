using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnTriggerEvent : MonoBehaviour {

    public bool destroyOnEnter;
    public Collider triggerObject;
    public UnityEvent triggerEvent;
    public UnityEvent triggerExitEvent;


    void Start(){
        if (triggerObject == null) triggerObject = GameManager.instance.player.GetComponent<CharacterController>();
    }
    void OnTriggerEnter(Collider other) {
        if (other == triggerObject) {
            triggerEvent.Invoke();

            if (destroyOnEnter) {
                gameObject.SetActive(false);
            }
        }
    }

    void OnTriggerExit(Collider other) {
        if (other == triggerObject) {
            triggerExitEvent.Invoke();
        }
    }
}
