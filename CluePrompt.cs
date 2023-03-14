using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CluePrompt  : MonoBehaviour
{

    // Start is called before the first frame update
    [TextArea(10, 3)]
public string dialogueText;
public TextMeshProUGUI subtitle;
void Start()
{
    StartCoroutine(Script());
}

IEnumerator Script()
{
    subtitle.text = dialogueText;
        yield return new WaitForSeconds(2f);
        subtitle.text = "";
}


}
