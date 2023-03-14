using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameMenu : MonoBehaviour
{
    public static GameMenu instance;

    public KeyCode toggleKeyCode = KeyCode.Escape;

    public GameObject canvas;
    public Camera canvasCamera;
    public AudioClip toggleAudioIn;
    public AudioClip toggleAudioOut;

    public UnityEvent OnEnableMenu;
    bool isEnabled = false;
    bool isToggleEnabled = true;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        isEnabled = canvas.activeInHierarchy;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isToggleEnabled) return;
        if (TimelineManager.instance.enableTimeline) return; // disable when there is timeline playing
        if (GameManager.instance.isPlayerMovementPaused && !isEnabled) return;

        if (Input.GetKeyDown(toggleKeyCode)) {
            Toggle();
        }
    }

    public void Toggle()
    {
        isEnabled = !isEnabled;
        EnableMenu(isEnabled);
    }

    public void EnableMenu(bool isEnabled)
    {
        canvas.SetActive(isEnabled);
        GameManager.instance.PausePlayerMovements(isEnabled);
        GameManager.instance.Pause(isEnabled);
        canvasCamera.gameObject.SetActive(isEnabled);

        if (isEnabled) OnEnableMenu.Invoke();
        this.isEnabled = isEnabled;
        
        AudioClip clip = isEnabled ? toggleAudioIn : toggleAudioOut;
        FXSoundSystem.Instance.PlayOneShot(clip);
    }

    public void EnableToggling(bool enable){
        isToggleEnabled = enable;
    }

    public void ExitToMenu() {
        SceneLoader.instance.LoadMainMenu();
    }
}
