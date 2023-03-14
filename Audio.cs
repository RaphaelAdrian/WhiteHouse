using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Audio : MonoBehaviour
{

    public AudioClipNamed[] audioClips;
    public RandomClips[] randomClips;
    internal AudioSource audioSource;
    // Start is called before the first frame update
    public virtual void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayAudioClipOneShot(int index, float volume = 1f) {
        audioSource.PlayOneShot(audioClips[index].clip, volume);
    }
    public void PlayAudioClip(int index, bool interruptPrevious = true, float volume = 1f, float delay = 0f) {
        PlayAudioClip(audioClips[index].clip, interruptPrevious, volume, delay);
    }
    public void PlayAudioClip(AudioClip audioClip, bool interruptPrevious = true, float volume = 1f, float delay = 0f) {
        audioSource.volume = volume;
        audioSource.clip = audioClip;
        if (interruptPrevious) {
            audioSource.PlayDelayed(delay);
        } else if (!audioSource.isPlaying){
            audioSource.PlayDelayed(delay);
        } 
    }

    public void PlayRandom(int collectionIndex, bool interruptPrevious = true, float volume = 1f, float delay = 0f, bool isLooped = false) {
        audioSource.loop = isLooped;
        RandomClips collection = randomClips[collectionIndex];
        int randIndex = Random.Range(0, collection.clips.Length);
        PlayAudioClip(collection.clips[randIndex], interruptPrevious, volume, delay);
    }

    public void PlayRandomFromCollection(List<AudioClip> clips, float volume = 1f) {
        int randIndex = Random.Range(0, clips.Count);
        audioSource.PlayOneShot(clips[randIndex], volume);
    }

    public void PlayRandomOneShot(int collectionIndex, float volume = 1f) {
        RandomClips collection = randomClips[collectionIndex];
        int randIndex = Random.Range(0, collection.clips.Length);
        audioSource.PlayOneShot(collection.clips[randIndex], volume);
    }

    public void PlayRandomLooped(int collectionIndex, float volume = 1f, float delay = 0) {
        PlayRandom(collectionIndex, false, volume, delay, true);
    }

    public void Stop(){
        audioSource.loop = false;
        audioSource.Stop();
    }
}

[System.Serializable]
public class RandomClips {
    public string name;
    public AudioClip[] clips;
}

[System.Serializable] 
public class AudioClipNamed {
    public string name;
    public AudioClip clip;
}