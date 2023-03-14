using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    protected static SceneLoader _instance;
    public static SceneLoader instance { get { return _instance; } }
    public SceneSettings sceneSettingsData;
    public LoadingCanvas loadingCanvas;

    public virtual void Awake()
    {
        if (_instance == null) {
            _instance = this;
            this.transform.parent = null;
            DontDestroyOnLoad(this.gameObject);
        }
        else {
            loadingCanvas = _instance.loadingCanvas;
        }
    }

    public void GoToScene(string sceneName, LoadSceneMode loadSceneMode = LoadSceneMode.Single)
    {
        Time.timeScale = 1;
        StartCoroutine(LoadSceneDelayed(sceneName, loadSceneMode, 1f));
    }
    public void GoToScene(int index, LoadSceneMode loadSceneMode = LoadSceneMode.Single)
    {
        Time.timeScale = 1;
        StartCoroutine(LoadSceneDelayed(index, loadSceneMode, 1f));
    }

    private IEnumerator LoadSceneDelayed(int index, LoadSceneMode loadSceneMode, float delay)
    {
        loadingCanvas.Show(true);
        yield return new WaitForSecondsRealtime(delay);
        AsyncOperation loadingOperation = SceneManager.LoadSceneAsync(index, loadSceneMode);
        loadingCanvas.Activate(loadingOperation);
    }
    private IEnumerator LoadSceneDelayed(string sceneName, LoadSceneMode loadSceneMode, float delay)
    {
        loadingCanvas.Show(true);
        yield return new WaitForSecondsRealtime(delay);
        AsyncOperation loadingOperation = SceneManager.LoadSceneAsync(sceneName, loadSceneMode);
        loadingCanvas.Activate(loadingOperation);
    }

    public void LoadGame(SlotData slotData)
    {
        PlayerPrefs.SetInt("CurrentSlot", slotData.slotNumber);
        GoToScene(sceneSettingsData.questScenes[slotData.currentChapterNumber].sceneName);
    }

    public void LoadMainMenu()
    {
        GoToScene(sceneSettingsData.mainMenuSceneName);
    }

    public void ProgressToNextQuest() {
        Debug.Log("loading next scene");
        // Get max quests
        int maxQuests = sceneSettingsData.questScenes.Count;
        SlotData slotData = Methods.GetCurrentSlot();

        #if UNITY_EDITOR 
        slotData.currentChapterNumber = GetCurrentSceneChapter();

        #endif
        slotData.currentChapterNumber++;

        if (slotData.currentChapterNumber < maxQuests) {
            LoadGame(slotData);
        } else {
            GoToScene(sceneSettingsData.endingSceneName);
        }
        SaveManager.instance.SaveAll(slotData);
    }

    private int GetCurrentSceneChapter()
    {
        foreach (QuestScene questScene in sceneSettingsData.questScenes)
        {
            if (questScene.sceneName != SceneManager.GetActiveScene().name) continue;
            return sceneSettingsData.questScenes.IndexOf(questScene);
        }

        return -1;
    }
}
