using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineManager : MonoBehaviour
{
    public static TimelineManager instance;

    public bool enableTimeline;
    public int currentActiveScene = 0;
    
    [Header("SCENES")]
    public GameObject[] scenes;

    [Header("DISABLED ON TIMELINE PREVIEW")]
    public GameObject[] disabledGameobjects;

    [Header("DISABLED ON TIMELINE (BUT ACTIVATES ON TIMELINE END ONLY)")]
    public GameObject[] enableOnTimelineEnd;


    int lastActiveScene = 0;

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

    void OnValidate(){
        if (!gameObject.activeInHierarchy) return;
        UpdateObjects();
    }

    void UpdateObjects(){
        if (scenes.Length == 0) return;
        if (enableTimeline) {
            if (lastActiveScene != currentActiveScene) {
            // Disable last scene
                scenes[lastActiveScene % scenes.Length]?.SetActive(false);
            } 
            scenes[currentActiveScene % scenes.Length]?.SetActive(true);
            lastActiveScene = currentActiveScene;
            EnableGameplayObjects(false);
        } else {
            // Disable last scene
            scenes[lastActiveScene % scenes.Length]?.SetActive(false);
            EnableGameplayObjects(true);
        }
    }

    // private void UpdateObjects()
    // {
    //     int activeScenes = 0;
    //     foreach(Scene scene in scenes) {
    //         scene.sceneObject.SetActive(scene.isActive);

    //         if (scene.isActive)
    //             activeScenes++;
    //     }

    //     // disable gameobjects if there are no scenes enabled
    //     bool isActive = activeScenes == 0;
    //     EnableGameplayObjects(isActive);
    // }

    public void EnableGameplayObjects(bool isEnable)
    {
        foreach(GameObject obj in disabledGameobjects) {
            obj.SetActive(isEnable);
        }
    }

    public void UpdateGameplayObjectsOnEnd() {
        if (enableTimeline) {
            foreach(GameObject obj in enableOnTimelineEnd) {
                obj.SetActive(false);
            }
        } else {
            foreach(GameObject obj in enableOnTimelineEnd) {
                obj.SetActive(true);
            }
        }

    }

    public void EndAllTimelines(){
        enableTimeline = false;
        UpdateObjects();
        UpdateGameplayObjectsOnEnd();
    }

    public void LoadNextTimeline(){
        currentActiveScene++;
        UpdateObjects();
        UpdateGameplayObjectsOnEnd();
    }

    public void LoadTimeline(int index) {
        currentActiveScene = index;
        enableTimeline = true;
        UpdateObjects();
        UpdateGameplayObjectsOnEnd();
    }
}
