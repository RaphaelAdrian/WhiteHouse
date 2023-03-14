using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Kapre : MonoBehaviour
{
    public float timeToFindCigarette = 60f;
    float timeRemaining;

    [Header("UI Assignments")]
    public GameObject timerCanvas;
    public TMP_Text timeText;

    [Header("Events")]
    public UnityEvent OnOutOfTime;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartTimer());
    }

    private IEnumerator StartTimer()
    {
        timeRemaining = timeToFindCigarette;

        while (timeRemaining > 0) {

            if (Input.GetKeyUp(KeyCode.KeypadPlus)) {
                timeRemaining -= 20f;
            }
            timeRemaining -= Time.deltaTime;

            UpdateTexts();
            yield return null;
        }
        timeRemaining = 0;
        UpdateTexts();
        NoTimeRemaining();
    }

    private void NoTimeRemaining()
    {
        GameManager.instance.player.KnockOut(this.transform, this);
        OnOutOfTime.Invoke();
    }

    private void UpdateTexts()
    {
        TimeSpan t = TimeSpan.FromSeconds(timeRemaining);
        string formattedTIme = string.Format("{0:D2}:{1:D2}:{2:D2}:{3:D2}", 
                t.Hours, 
                t.Minutes, 
                t.Seconds, 
                t.Milliseconds);
        timeText.text = formattedTIme;
    }

    public void OnReceiveCigarette(){
        timerCanvas.SetActive(false);
        StopCoroutine(StartTimer());
        GameManager.instance.PausePlayerMovements(true);
    }
}
