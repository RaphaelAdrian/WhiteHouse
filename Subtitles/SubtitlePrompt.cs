using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SubtitlePrompt : MonoBehaviour
{
    private InventoryItem getItem;
    [TextArea(3,10)]
    public string dialogueText;
    public TextMeshProUGUI dialogue;
    private void Start()
    {
        getItem= GetComponentInChildren<InventoryItem>();
     

    }

    void Update()
    {
        if (getItem.isObtained)
        {
                StartCoroutine(Sequence(dialogueText));        
        }
    }
    IEnumerator Sequence(string prompt)
    {
        
        yield return new WaitForSeconds(2);
        dialogue.text = prompt;
        yield return new WaitForSeconds(2);
        dialogue.text = "";
        this.gameObject.SetActive(false);
    }


}