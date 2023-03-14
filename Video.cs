using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;

[RequireComponent(typeof(VideoPlayer))]
public class Video : MonoBehaviour
{
    public UnityEvent onVideoEnd;
    private VideoPlayer videoPlayer;
    public AudioSource[] audioSources;
    public bool playOnStart = false;
    // Start is called before the first frame update
    void Awake() {
        audioSources = FindObjectsOfType<AudioSource>();
        videoPlayer = GetComponent<VideoPlayer>();
        if (playOnStart) Play();
        
    }

    public void Play()
    {
        foreach(AudioSource audioSource in audioSources) {
            audioSource.mute = true;
        }
        TooltipManager.instance.EnableSelector(false);
        videoPlayer.Play();
        StartCoroutine(OnVideoEndCoroutine());
    }

    private IEnumerator OnVideoEndCoroutine()
    {
        while (videoPlayer.isPlaying)
        {
            yield return null;
        }
        TooltipManager.instance.EnableSelector(true);
        onVideoEnd.Invoke();
        foreach(AudioSource audioSource in audioSources) {
            audioSource.mute = false;
        }
    }


}
