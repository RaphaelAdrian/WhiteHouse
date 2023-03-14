using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModalTrigger : DialogueTrigger
{
    // Start is called before the first frame update
    public override TypewriterEffect GetTypeWriter()
    {
        return ModalManager.instance;
    }
}