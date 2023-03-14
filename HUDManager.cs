using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public Image[] hUDImages;
    
    [SerializeField]
    private Image fadeToBlack;
    [SerializeField]
    private Image fadeInFromBlack;
    Image lastHUD = null;
    // Start is called before the first frame update

    public void ShowHUD(int level) {
        if (lastHUD) lastHUD.gameObject.SetActive(false);
        lastHUD = hUDImages[level - 1];
        lastHUD.gameObject.SetActive(true);
        lastHUD.GetComponent<Animator>().enabled = false;
    }
    public void ShowHUDFadeInOut(int level) {
        if (lastHUD) lastHUD.gameObject.SetActive(false);
        lastHUD = hUDImages[level - 1];
        lastHUD.gameObject.SetActive(true);
        lastHUD.GetComponent<Animator>().enabled = true;
    }

    public void FadeToBlack(bool fadeIn){
        fadeToBlack.gameObject.SetActive(fadeIn);
        fadeInFromBlack.gameObject.SetActive(!fadeIn);
    }
    
    public void HideHUD() {
        if (lastHUD) lastHUD.gameObject.SetActive(false);
    }

}
