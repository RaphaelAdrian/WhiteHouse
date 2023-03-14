using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class TypewriterEffect : MonoBehaviour
{


    [Header("Text")]
    public Canvas canvas;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI sentenceText;
    public Queue<string> sentences;
    public Queue<AudioClip> clips;

    private Dialogue pendingDialog;
    AudioSource audioSource;
    internal bool isDone;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        sentences = new Queue<string>();
        clips = new Queue<AudioClip>();
        canvas.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (pendingDialog == null) return;
        if (Input.GetKeyDown(KeyCode.Return)) 
        {
            DisplayNextSentence();
        }
    }
    
    // Update is called once per frame
    public void StartDialogue(Dialogue dialogue, float delay)
    {
        StartCoroutine(StartDialogueCoroutine(dialogue, delay));
        
        pendingDialog = dialogue;
    }

    private IEnumerator StartDialogueCoroutine(Dialogue dialogue, float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        GameManager.instance.PausePlayerMovements(true, false);
        
        canvas.gameObject.SetActive(true);
        isDone = false;

        nameText.text = dialogue.name.ToString();
        sentences.Clear();
        clips.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        foreach (AudioClip clip in dialogue.clipsToPlay)
        {
            clips.Enqueue(clip);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            isDone = true;
            
            return;
        }
        string sentence = sentences.Dequeue();
        if (clips.Count > 0 ) {
            AudioClip clip = clips.Dequeue();
            if (clip) {
                audioSource.clip = clip;
                audioSource.Play();
            }
        } 
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        sentenceText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            sentenceText.text += letter;
            yield return null;  // calls each letter 1 frame per seconds
        }
    }

    public void EndDialogue()
    {
        audioSource.Stop();
        // UNFREEZE PLAYER
        GameManager.instance.PausePlayerMovements(false, false);
        canvas.gameObject.SetActive(false);
        pendingDialog = null;
    }

    public void SetDone(bool done) {
        this.isDone = done;
    }
}
