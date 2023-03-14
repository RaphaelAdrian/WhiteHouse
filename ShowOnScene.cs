using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShowOnScene : MonoBehaviour
{
    public string questName;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        foreach (QuestScene questScene in SceneLoader.instance.sceneSettingsData.questScenes)
        {
            if (questScene.sceneName != SceneManager.GetActiveScene().name) continue;
            if (questScene.questName != questName) continue;
            gameObject.SetActive(true);
            break;
        }
    }
}
