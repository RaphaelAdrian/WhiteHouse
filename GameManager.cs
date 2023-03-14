using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager instance { get { return _instance; } }

    public HUDManager HUD;
    public FootstepSwapper footstepSwapper;
    public Player player;
    public bool isPaused;
    public bool isGameOver;
    public bool hideCursorOnStart = true;
    internal bool isPlayerMovementPaused = false;


    [Header("Electricity")]
    public UnityEvent OnCircuitBreakerOn;
    public UnityEvent OnCircuitBreakerOff;
    public CircuitBreaker circuitBreaker;
    public List<ElectricalComponent> electricalComponentPool = new List<ElectricalComponent>();

    [Header("Debug")]
    public bool setElectricityOn = false;
    internal bool isElectricityOn = false;

    void Awake()
    {
        Debug.Log("Awake Game Manager");
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }

    void Start() {
        if (hideCursorOnStart)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        if(setElectricityOn) {
            circuitBreaker.circuitSwitch.ActivateInteractable(true);
            circuitBreaker.circuitSwitch.GetComponent<InteractableSaver>().UpdateState(true);
        }
    }

    void Update() {
        if (Input.GetKeyUp(KeyCode.F2)) {
            SceneLoader.instance.ProgressToNextQuest();
        }
    }


    public void Pause(bool isPause)
    {
        Time.timeScale = isPause ? 0f : 1f;
        isPaused = isPause;
        FXSoundSystem.Instance.Pause(isPause);
        BGSoundSystem.Instance.Pause(isPause);
        EnableCursor(isPause);
    }

    public void PausePlayerMovements(bool isEnable)
    {
        // Pause(isEnable);
        player.DisableCameraLook(isEnable);
        player.DisableMovement(isEnable);
        player.GetComponent<InteractableDetector>().Enable(!isEnable);
        isPlayerMovementPaused = isEnable;
    }

    public void PausePlayerMovements(bool isEnable, bool updateTimeScale)
    {
        PausePlayerMovements(isEnable);
        if (updateTimeScale) Time.timeScale = isEnable ? 0 : 1;
    }

    public void EnableCursor(bool isEnable)
    {
        Cursor.lockState = isEnable ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = isEnable;
    }
}