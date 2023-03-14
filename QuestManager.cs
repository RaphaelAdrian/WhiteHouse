using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class QuestManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Quest[] quests;
    public GameObject chapter = null;
    public TextMeshProUGUI chapterTitleText;
    public TextMeshProUGUI chapterNumberText;
    bool isComplete;

    void Start()
    {
        isComplete = false;
        chapter.SetActive(false);
        foreach (Quest q in quests)
        {
            q.isDone = false;
        }
    }

    public void CheckQuest(string name , bool isDone)
    {
        foreach (Quest q in quests)
        {
            if (q.questName == name)
            {
                q.isDone = isDone;
                chapterTitleText.text = q.chapterTitle;
                chapterNumberText.text = q.chapterNumber;
                if (q.isDone && !isComplete)
                {
                    StartCoroutine(ShowChapter(4f, chapter));
                }
            }
        }

    }

    IEnumerator ShowChapter(float waitTime, GameObject chapterPanel)
    {


        chapterPanel.SetActive(true);
        yield return new WaitForSeconds(waitTime);
        this.chapter.SetActive(false);
        isComplete = true;

    }

}
