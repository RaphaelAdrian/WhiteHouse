using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueTrigger : MonoBehaviour
{
    public bool showOnce = true;
    public bool activateOnEnable = false;


   [Header("Optional")]
    public Transform lookAt;
    
    [Tooltip("Default is Player")]
    public Collider triggerObject;
    public float activateDelay = 0f;

   [Header("Dialog")]
    public Dialogue dialog;

   [Header("Events")]
   public UnityEvent onClose;
   public UnityEvent onEnable;

    private TypewriterEffect typerWriter;

    private bool isDisabled = false;

    public virtual TypewriterEffect GetTypeWriter() {
        return DialogueManager.instance;
    }  

    public virtual void Start(){
        if (triggerObject == null) triggerObject = GameManager.instance.player.GetComponent<CharacterController>();
        if (activateOnEnable) Activate();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other == triggerObject) {
            Activate();
        } 
    }

    public virtual void Activate(){
        if (isDisabled) return;
        onEnable.Invoke();
        GetTypeWriter().StartDialogue(dialog, activateDelay);
        if (lookAt) GameManager.instance.player.ForceLookAt(lookAt);
        StartCoroutine(WaitForEndOfDialog());
    }

    private IEnumerator WaitForEndOfDialog()
    {
        while (!GetTypeWriter().isDone) yield return null;
        GetTypeWriter().isDone = false;
        onClose.Invoke();
        OnEnd();
        if (showOnce && this.gameObject.activeInHierarchy) this.gameObject.SetActive(false);
    }

    public virtual void OnEnd(){

    }

    public void CloseDialog(){
        GetTypeWriter().isDone = true;
        GetTypeWriter().EndDialogue();
    }

    public void Disable(bool isDisable) {
        isDisabled = isDisable;
    }
}

[System.Serializable]
public class Dialogue
{
    public string name = "";
    [TextArea(3, 10)]
    public string[] sentences;
    public AudioClip[] clipsToPlay;

}
