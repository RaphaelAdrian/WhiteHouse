using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SoundSystem : Audio
{
    protected static SoundSystem _instance;
    public static SoundSystem Instance { get { return _instance; } }

    protected AudioClip initClip;
    protected float initVolume = 1;


    // Start is called before the first frame update
    public virtual void Awake()
    {
        if (_instance == null) {
            _instance = this;
            audioSource = GetComponent<AudioSource>();
            OnInit();
            DontDestroyOnLoad(this.gameObject);
        }
        else {
            audioSource = _instance.gameObject.GetComponent<AudioSource>();
            OnInit();
        }
    }

    public override void Start(){
        // don't delete because we need to override start
    }

    public virtual void OnInit() { 
        transform.parent = null;
        initClip = GetComponent<AudioSource>().clip;
        initVolume = GetComponent<AudioSource>().volume;
    }

    public void PlayOneShot(AudioClip audioClip, float volume = 1f) {
        audioSource.PlayOneShot(audioClip, volume);
    }

    public void PlayLooped(AudioClip audioClip, float delay = 0, float volume = 1) {
        audioSource.clip = audioClip;
        audioSource.PlayDelayed(delay);
        audioSource.loop = true;
        audioSource.volume = volume;
    }
    public void PlaySoundDelayed(AudioClip audioClip, float delay) {
        audioSource.clip = audioClip;
        audioSource.PlayDelayed(delay);
    }


    public void SetSoundVolume(float value) {
        audioSource.volume = value;
    }

    public void AddPitch(float value) {
        audioSource.pitch += value;
    }

    public void SetPitch(float value) {
        audioSource.pitch = value;

    }

    public float GetCurrentVolume() {
        return audioSource.volume;
    }

    public void Stop() {
        audioSource.loop = false;
        audioSource.Stop();
    }

    public void Pause(bool isPause) {
        if (isPause) audioSource.Pause();
        else audioSource.UnPause();
    }
}