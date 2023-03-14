using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

[RequireComponent(typeof(AudioSource))]
public class BlackLadyScare : MonoBehaviour
{
    public float duration = 5f;
    public float speed = 15;
    public bool playScareSound = true;
    public Audio scareAudioPool;
    public AudioClip[] otherClipsToPlaySFX;
    public AudioClip[] otherClipsToPlaySource;
    public UnityEvent OnTimerEnd;
    // Start is called before the first frame update
    void OnEnable()
    {
        StartCoroutine(Run());
    }

    private IEnumerator Run()
    {
        scareAudioPool.audioSource = GetComponent<AudioSource>();
        float timer = duration;
        scareAudioPool.PlayRandomOneShot(0);
        if (playScareSound) FXSoundSystem.Instance.PlayRandomOneShot(1, 0.5f);
        foreach(AudioClip clip in otherClipsToPlaySFX) {
            FXSoundSystem.Instance.PlayOneShot(clip, 0.5f);
        }
        foreach(AudioClip clip in otherClipsToPlaySource) {
            scareAudioPool.audioSource.PlayOneShot(clip, 1f);
        }
        // int direction = Random.Range(0, 360);
        // transform.eulerAngles = transform.forward;

        while (timer > 0) {
            timer -= Time.deltaTime;
            transform.localPosition += transform.forward * Time.deltaTime * speed;
            yield return null;
        }
        OnTimerEnd.Invoke();
        gameObject.SetActive(false);
    }
}
