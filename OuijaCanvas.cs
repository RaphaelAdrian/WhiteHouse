using System;
using System.Collections;
using System.Collections.Generic;
using Michsky.UI.Dark;
using UnityEngine;

public class OuijaCanvas : MonoBehaviour
{
    public ModalWindowManager windowManager;
    // Start is called before the first frame update

    public void Show(bool isShow) {

        StartCoroutine(DelayShow(isShow));
    }

    private IEnumerator DelayShow(bool isShow)
    {
        yield return new WaitForEndOfFrame();        
        GameMenu.instance.EnableToggling(!isShow);
        GameManager.instance.PausePlayerMovements(isShow);
        GameManager.instance.Pause(isShow);

        if (isShow) windowManager.ModalWindowIn();
        else windowManager.ModalWindowOut();
    }

    public void SetModalWindowManager(ModalWindowManager windowManager) {
        this.windowManager = windowManager;
    }
}
