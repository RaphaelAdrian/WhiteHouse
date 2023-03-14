using System;
using System.Collections;
using System.Collections.Generic;
using Michsky.UI.Dark;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class LoadingCanvas : MonoBehaviour
{

    [Header("Loading Bar")]
    public Scrollbar progressBar;
    public UIDissolveEffect uIDissolveEffect;

    [Header("Required")]
    public Image splashImage;
    public TMP_Text hintText;
    public TMP_Text chapterText;

    [Header("Texts")]
    public string[] hints;


    [Header("Splash Images")]
    public Sprite[] splashImages;

    // public float fadeTime = 2f;
    float progress = 0;

    void Start() {
    }

    public void Activate(AsyncOperation loadingOperation)
    {
        StartCoroutine(StartLoadingAnimation(loadingOperation));
    }

    public void Show(bool isShow) {
        if (isShow) {
            uIDissolveEffect.DissolveIn();

            //update texts
            SlotData slotData = Methods.GetCurrentSlot();
            int chapterNumber = slotData.currentChapterNumber;
            chapterText.text = "Chapter " + Methods.ToRoman(chapterNumber + 1);

            int randIndex = Random.Range(0, hints.Length);
            hintText.text = hints[randIndex];

            splashImage.sprite = splashImages[chapterNumber];
        } 
        else uIDissolveEffect.DissolveOut();
    }

    // private IEnumerator FadeAnimation(bool isShow)
    // {
    //     float timer = 0;
    //     float targetValue = isShow ? 1 : 0;
    //     float initValue = canvasGroup.alpha;
    //     while (timer < fadeTime) {
    //         timer += Time.deltaTime;
    //         canvasGroup.alpha = Mathf.Lerp(initValue, targetValue, timer);
    //         yield return null;
    //     }
    //     canvasGroup.alpha = targetValue;
    // }

    private IEnumerator StartLoadingAnimation(AsyncOperation loadingOperation)
    {
        while(loadingOperation.progress < 1) {
            progressBar.size = loadingOperation.progress / 0.9f;
            yield return null;
        }
        this.Show(false);
    }
}
