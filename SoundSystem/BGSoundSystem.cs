using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGSoundSystem : SoundSystem
{

    protected new static BGSoundSystem _instance;
    public new static BGSoundSystem Instance { get { return _instance; } }
    public override void Awake()
    {
        if (_instance == null) {
            _instance = this;
            audioSource = GetComponent<AudioSource>();
            audioSource.Play();
            
            OnInit();
            DontDestroyOnLoad(this.gameObject);
        }
        else {
            OnInit();
            audioSource = _instance.gameObject.GetComponent<AudioSource>();
            StartCoroutine(CrossfadeNewBGMusic());
        }
    }

    private IEnumerator CrossfadeNewBGMusic()
    {
        yield return new WaitForEndOfFrame();
        float timeElapsed = 0;
        float duration = 3f;
        float currentVolume = audioSource.volume;
        // Fade out last audio first
        while (timeElapsed < duration) {
            timeElapsed += Time.unscaledDeltaTime;
            float time = timeElapsed / duration;
            audioSource.volume = Mathf.Lerp(currentVolume, 0, time);
            yield return null;
        }
        audioSource.volume = 0;

        // then fade in new audio
        timeElapsed = 0;
        duration = 3f;
        audioSource.clip = initClip;

        // Fade out last audio first
        while (timeElapsed < duration) {
            timeElapsed += Time.unscaledDeltaTime;
            float time = timeElapsed / duration;
            audioSource.volume = Mathf.Lerp(0, initVolume, time);
            yield return null;
        }
        audioSource.volume = initVolume;
    }

    public void ResetClip() {
        audioSource.clip = initClip;
        audioSource.Play();
    }
    public void PlayRandom(BGAudioSelection audioSelection, bool isOneShot , float volume = 1f) {
        if (isOneShot) {
            PlayRandomOneShot((int)audioSelection,volume);
        } else {
            audioSource.loop = true;
            PlayRandom((int)audioSelection, true, volume);
        }
    }
}

public enum BGAudioSelection {
    HORROR, RISERS
};
