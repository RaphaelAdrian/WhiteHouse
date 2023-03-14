using System.Collections;
using System.Collections.Generic;
using Michsky.UI.Dark;
using UnityEngine;
using UnityEngine.UI;
public class Pages : Backpack
{
    public static Pages instance;

    public ModalWindowManager displayModal;
    public Image displayImage;

    public override void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        base.Awake();
    }
}