using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ToggleAnimation : MonoBehaviour
{
    public KeyCode keyCode = KeyCode.E;
    public string toggleAnimatorString = "";

    bool isActivated = false;


    void CheckActivated(){
        isActivated = GetComponent<Animator>().GetBool(toggleAnimatorString);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.isPaused) return;
        if (Input.GetKeyUp(keyCode)) {
            CheckActivated();
            Activate(!isActivated);
        }
    }

    public void Activate(bool activate) {
        GetComponent<Animator>().SetBool(toggleAnimatorString, activate);
    }
}
