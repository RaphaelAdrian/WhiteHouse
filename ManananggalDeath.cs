using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManananggalDeath : MonoBehaviour
{
    private Interactable interactable;
    public GameObject upperBody;
    private QuestItem quest;
    private Manananggal manananggal;
    void Start()
    {
        quest = GetComponent<QuestItem>();
        interactable = GetComponent<Interactable>();
        manananggal = upperBody.GetComponent<Manananggal>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (quest.questNumber != 1)
        {
            upperBody.SetActive(false);
            manananggal.Die();
            this.gameObject.SetActive(false);
        }
    }

    
}
