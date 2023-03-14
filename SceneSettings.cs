using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Scene Settings", menuName = "Scene Settings", order = 1)]
public class SceneSettings : ScriptableObject
{
    // We need to wrap ifUnityEditor to SceneAssets since we cannot use them on build
    #if UNITY_EDITOR
    public SceneAsset mainMenuScene;
    public SceneAsset endingScene;
    #endif
    
    [HideInInspector]
    public string mainMenuSceneName;
    [HideInInspector]
    public string endingSceneName;

    [Header("Quests in order")]
    public List<QuestScene> questScenes;

    #if UNITY_EDITOR
    void OnValidate() {
        mainMenuSceneName = mainMenuScene.name;
        endingSceneName = endingScene.name;

        foreach(QuestScene questScene in questScenes) {
            questScene.sceneName = questScene.scene.name;
        }
    }
    #endif
}

[System.Serializable]
public class QuestScene {
    public string questName;
    [HideInInspector]
    public string sceneName;

    #if UNITY_EDITOR
    public SceneAsset scene;
    #endif
}