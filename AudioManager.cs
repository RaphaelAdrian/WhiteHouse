using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    
    public static AudioManager instance;
    void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (!instance)
            {
                Destroy(gameObject);
            }
        }


        foreach (Sound s in sounds)
        {
            gameObject.AddComponent<AudioSource>();
            s.audioSource = gameObject.AddComponent<AudioSource>();
            s.audioSource.clip = s.clip;
            s.audioSource.volume = s.volume;
            s.audioSource.pitch = s.pitch;
            s.audioSource.loop = s.loop;
        }
    }

    public void Play(string name)
    {
        foreach (Sound s in sounds)
        {
            if (s.name == name)
            {
                s.audioSource.Play();
            }
        }
    }

}
