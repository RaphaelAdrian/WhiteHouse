using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TiyanakSceneManager : MonoBehaviour
{
    public AudioClip brokenGlassSfx;
    public TiyanakAI tiyanak;
    public float babyCryDelay = 0f;
    public Door basementDoor;
    public UnityEvent OnGlassBroke;
    public UnityEvent OnTiyanakCry;


    public void PlayBrokenGlass(AudioSource audioSource){
        OnGlassBroke.Invoke();
        audioSource.PlayOneShot(brokenGlassSfx);
        basementDoor.GetComponent<DialogueTrigger>().dialog.sentences = new string[]{"Ahhh, still not working"};
        StartCoroutine(StartCry());
    }

    private IEnumerator StartCry()
    {
        yield return new WaitForSeconds(babyCryDelay);
        GetComponent<TimelineActivator>().PlayTimeline(0);
        OnTiyanakCry.Invoke();
    }
}
