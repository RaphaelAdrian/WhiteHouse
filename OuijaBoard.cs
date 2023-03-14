using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class OuijaBoard : MonoBehaviour
{
    public OuijaQuestion[] ouijaQuestions;
    public OuijaCanvas ouijaCanvas;
    public GameObject glass;
    public GameObject lettersRoot;
    public float moveSpeedInSeconds = 2f;
    public float idleTime = 1f;
    public UnityEvent<string> OnEnd;
    private GameObject glassPos;
    private Vector3 initialPos;
    private string firstLetter;
    
    // private char[] letters = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
    private Transform[] letterTransforms;
    private bool isYes;

    private Dictionary<string, Transform> characters;


    void Start()
    {
        characters = new Dictionary<string, Transform>();
        letterTransforms = lettersRoot.GetComponentsInChildren<Transform>();
        initialPos = glass.transform.position;

        foreach(Transform transform in letterTransforms) {
            characters.Add(transform.name, transform);
        }

        foreach(OuijaQuestion ouijaQuestion in ouijaQuestions) {
            ouijaQuestion.questionButton.onClick.AddListener(()=>ShowMessageFromGlass(ouijaQuestion));
        }
    }

    
    void GlassMovement()
    {
        glass.transform.position = Vector3.Lerp(initialPos,
            glassPos.transform.position,1*Time.deltaTime);
    }
    public void ShowMessageFromGlass(OuijaQuestion ouijaQuestion)
    {
        StartCoroutine(StartGlassMovement(ouijaQuestion));
    }

    private IEnumerator StartGlassMovement(OuijaQuestion ouijaQuestion)
    {
        string message = ouijaQuestion.answer.Replace(" ", "");
        ouijaCanvas.Show(false);
        GameManager.instance.PausePlayerMovements(true);
        GameManager.instance.player.GetComponent<FlashLightController>().DisableFlashlight(true);
        GetComponent<Collider>().enabled = false;
        glass.SetActive(true);
        GetComponent<TimelineActivator>().PlayTimeline(0);

        // Start loop
        int letterIndex = 0;
        while(letterIndex < message.Length) {
            // IDLE
            yield return new WaitForSeconds(idleTime);

            //START
            //if yes or now, then don't make it an array
            
            string currentLetter = message[letterIndex].ToString().ToLower();
            float timeElapsed = 0;
            float duration = moveSpeedInSeconds;
            Vector3 initialPosition = glass.transform.position;
            Vector3 targetPos = characters[currentLetter].transform.position;
            while(timeElapsed < duration) {
                timeElapsed += Time.deltaTime;
                float time = Easing.Cubic.InOut(timeElapsed / duration);
                glass.transform.position = Vector3.Lerp(initialPosition, targetPos, time);
                yield return null;
            }
            letterIndex++;
            yield return null;
        }
        glass.transform.position = initialPos;
        GetComponent<TimelineActivator>().DisableAllTimelines();
        GetComponent<TimelineActivator>().PlayTimeline(1);
        GameManager.instance.PausePlayerMovements(false);
        GameManager.instance.player.GetComponent<FlashLightController>().DisableFlashlight(false);
        GetComponent<Collider>().enabled = true;
        ouijaQuestion.OnEnd.Invoke();
    }
}


[System.Serializable]
public struct OuijaQuestion {
    public Button questionButton;
    public string answer;
    public UnityEvent OnEnd;
}
