using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXSoundSystem : SoundSystem
{
    public AudioClip clickSound;
    [Header("Door Sounds")]
    public AudioClip doorOpening;
    public AudioClip doorClosing;

    [Header("Light Sounds")]
    public AudioClip flickerSFX;
    public AudioClip switchSFX;

    [Header("Gameplay Sounds")]
    public AudioClip inspectItemSFX;
    public AudioClip getItemSFX;
    protected new static FXSoundSystem _instance;
    public new static FXSoundSystem Instance { get { return _instance; } }

    public override void Awake()
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

    public void PlayClickSound() {
        audioSource.PlayOneShot(clickSound, 1f);
    }

    
    public void PlayRandom(FXAudioSelection audioSelection, bool isOneShot) {
        if (isOneShot) {
            PlayRandomOneShot((int)audioSelection);
        } else {
            PlayRandom((int)audioSelection);
        }
    }
}
public enum FXAudioSelection {
    JUMPSCARES, CREEPY
};
