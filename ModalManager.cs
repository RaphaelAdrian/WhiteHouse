using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModalManager : TypewriterEffect
{
    public static ModalManager instance;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
    }
}
