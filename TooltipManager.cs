using System;
using System.Collections;
using System.Collections.Generic;
using Michsky.UI.Dark;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TooltipManager : MonoBehaviour
{
    public static TooltipManager instance;

    [Header("TOOLTIP SELECTOR")]
    public Transform selectorPanel;
    public TMP_Text selectorHotkeyTxt;
    public TMP_Text selectorNameTxt;
    public GameObject selectorNamePanel;

    [Header("TOOLTIP MESSAGE")]
    public UIDissolveEffect messagePanel;
    public TMP_Text messageTxt;
    public RectTransform messageContentTransform;

    
    Collider targetObjectCollider;
    private Camera cam;
    Interactable currentInteractable;

    bool isTooltipSelectorEnabled = true;

    float delayBeforeActivation = 1f;
    bool isDelayFinished = false;

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
    
    // Start is called before the first frame update
    void Start()
    {
        cam = GameManager.instance.player.playerCamera;
        selectorPanel.gameObject.SetActive(false);

        // because of the save system, some tooltip messages were shown on start due to saved components
        // thus, we need to delay before we activate tooltips in the beginning
        StartCoroutine(StartDelay());
    }

    private IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(delayBeforeActivation);
        isDelayFinished = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!targetObjectCollider) return;
        selectorPanel.position = cam.WorldToScreenPoint(targetObjectCollider.bounds.center) + Vector3.up * 10f;
    }

    public void ShowTooltipSelector(Interactable interactable) {
        if (!isTooltipSelectorEnabled) return;

        targetObjectCollider = interactable.GetComponent<Collider>();

        selectorPanel.gameObject.SetActive(true);
        selectorHotkeyTxt.text = interactable.activationKey.ToString();

        currentInteractable = interactable;
        UpdateSelectorTexts();
    }

    public void UpdateSelectorTexts()
    {
        if (!currentInteractable) return;
        bool isNameEmpty = string.IsNullOrEmpty(currentInteractable.interactableName);
        
        selectorNamePanel.gameObject.SetActive(!isNameEmpty);
        selectorNameTxt.text = currentInteractable.interactableName;
    }

    public void HideTooltipSelector() {
        selectorPanel.gameObject.SetActive(false);
        targetObjectCollider = null;
        currentInteractable = null;
    }

    public void ShowTooltipMessage(string message, float disappearTime = 2f) {
        if (!isDelayFinished) return;
        messageTxt.text = message;
        LayoutRebuilder.ForceRebuildLayoutImmediate(messageContentTransform);
        StartCoroutine(TooltipMessageCoroutine(disappearTime));
    }

    public void ShowTooltipMessage(string message) {
        ShowTooltipMessage(message, 2f);
    }

    private IEnumerator TooltipMessageCoroutine(float disappearTime)
    {
        messagePanel.DissolveIn();
        yield return new WaitForSecondsRealtime(disappearTime);
        messagePanel.DissolveOut();
    }

    public void EnableSelector(bool enable) {
        this.isTooltipSelectorEnabled = enable;
        if (!enable){
            if (currentInteractable) currentInteractable.Disable();
        }
    }
}
