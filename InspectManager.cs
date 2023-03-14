using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class InspectManager : MonoBehaviour
{
    [Header("Keyboard Bindings")]
    public KeyCode closeKey = KeyCode.Escape;
    public KeyCode getKey = KeyCode.F;
    
    public TMP_Text closeKeyText;
    public TMP_Text getKeyText;


    [Header("Required")]
    public GameObject enableObjects;
    public CustomPassVolume inspectCustomPass;
    public GameObject getKeyShortcutPanel;
    public TMP_Text titleText;
    public Transform inspectLight;

    internal bool isActivated;


    private static InspectManager _instance;
    public static InspectManager instance { get { return _instance; } }

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }

    // Start is called before the first frame update
    public void EnableInspect(bool activate, bool enableGetItem = true) {
        isActivated = activate;

        Player player = GameManager.instance.player;
        Camera playerCamera = player.playerCamera;
        player.GetComponent<InteractableDetector>().Enable(!activate);
        enableObjects.SetActive(activate);

        titleText.text = TooltipManager.instance.selectorNameTxt.text;
        inspectCustomPass.customPasses[0].enabled = activate;

        closeKeyText.text = Methods.GetKeyString(closeKey);
        getKeyText.text = Methods.GetKeyString(getKey);
        getKeyShortcutPanel.SetActive(enableGetItem);

        inspectLight.transform.position = playerCamera.transform.position;
    }
}
