using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Instructions : MonoBehaviour
{
    public bool freezePlayer = true;
    public Transform lookAt;

    public KeyCode[] nextKeyCodes = {KeyCode.Return, KeyCode.KeypadEnter};
    public GameObject nextInstruction;
    public UnityEvent onPressEnter;
    public UnityEvent onEnable;

    // Update is called once per frame
    void Update()
    {
        if (IsKeyCodeEntered()) {
            gameObject.SetActive(false);
            if (nextInstruction) nextInstruction.SetActive(true);
            if (freezePlayer) GameManager.instance.PausePlayerMovements(false);
            onPressEnter.Invoke();
        }
    }

    private bool IsKeyCodeEntered()
    {
        foreach(KeyCode keyCode in nextKeyCodes) {
            if (Input.GetKeyUp(keyCode)) {
                return true;
            }
        }
        return false;
    }

    void OnEnable(){
        StartCoroutine(CoroutineEnable());
    }

    private IEnumerator CoroutineEnable()
    {
        yield return new WaitForEndOfFrame();
        if (lookAt) GameManager.instance.player.ForceLookAt(lookAt);
        if (freezePlayer) GameManager.instance.PausePlayerMovements(true);
        onEnable.Invoke();
    }
}
