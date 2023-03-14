using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TikbalangQuest : MonoBehaviour
{
    private QuestManager questManager;
    public GameObject souls;

    [HideInInspector]
    public bool isPressed;
    public int soulCount;
    ObtainSoul[] obtainSoul;

    public UnityEvent OnSoulComplete;
    void Start()
    {
        questManager = GetComponent<QuestManager>();
        soulCount = 0;
        obtainSoul = souls.GetComponentsInChildren<ObtainSoul>();
    }


    public void IncrementSouls() {
        if (GameManager.instance.isGameOver) return;
        soulCount++;

        if (soulCount == obtainSoul.Length)
        {
            OnSoulComplete.Invoke();
        }
    }
}
