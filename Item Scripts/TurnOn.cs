using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TurnOn : MonoBehaviour
{

    internal CircuitBreaker circuitBreaker;
    internal Interactable interactable;
    public bool isOn = false;
    internal AudioSource audioSource;
    public TextMeshProUGUI subText;

    void Start()
    {
        circuitBreaker=FindObjectOfType<CircuitBreaker>();
        audioSource = GetComponent<AudioSource>();
        interactable = GetComponent<Interactable>();

    }

    void Update()
    {
        
/*
        else if (!circuitBreaker.isElectricityOn || Item.Radio == item)
        {
            if (interactable.isInteracted && Input.GetKeyDown(KeyCode.F))
            {
                Debug.Log("There is no Electricity");

            }
            if (this.item == Item.Lamp)
            {
                Light light = GetComponentInChildren<Light>();

                light.enabled = false;
                *//*animation.enabled = false;*//*
                isOn = false;
            }

        }

*/



        }


   public IEnumerator ShowInfo(string prompt)
    {
        yield return new WaitForSeconds(0);
        subText.text = prompt;
        yield return new WaitForSeconds(2);
        subText.text = "";
    }















}
