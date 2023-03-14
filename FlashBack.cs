using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashBack : MonoBehaviour
{

    public GameObject santelmo;
    public GameObject flashBackPanel;
    public DialogueManager dialogueManager;

    public Interactable interactable;

    void Update()
    {
        if (santelmo.activeInHierarchy)
        {
            if (dialogueManager.isDone)
            {
                    santelmo.SetActive(false);
                    flashBackPanel.SetActive(true);
            }
        }
    }
}
