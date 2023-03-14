using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PageClick : MonoBehaviour
{
    // private Pages page;
    // public GameObject target;
    // private Vector2[] defaultPos;
    // private Vector3[] thisObject;
    // private Vector2 targetPos;
    // bool isClicked = false;
    // public GameObject[] pages;
    // public string buttonName;
    // public GameObject selected;
    // bool isReverse;
    
    // void Start()
    // {
        
    //     page = GetComponent<Pages>();
    //     targetPos = new Vector2(target.transform.position.x, target.transform.position.y);
    //     defaultPos= new Vector2[pages.Length];
    //     thisObject= new Vector3[pages.Length];
    //     GetDefaultPos();
    // }
    // void LateUpdate()
    // {
    //     if (isClicked & !isReverse)
    //     {
    //         selected.transform.position = Vector2.Lerp(selected.transform.position, targetPos, 0.1f);
    //         selected.transform.localScale = Vector3.Lerp(selected.transform.localScale, target.transform.localScale, 0.1f);
    //     }
    // }

    // void Update()
    // {
    //     for (int i = 0; i <= pages.Length-1; i++)
    //     {
    //         pages[i].GetComponent<Button>().interactable = page.creature[i].isGet ? true : false;
    //         pages[i].SetActive(pages[i].name != buttonName && isClicked && !isReverse ? false : true);
    //     }
    // }



    // public void OnClick()
    // {
    //     selected = (EventSystem.current.currentSelectedGameObject.gameObject);
    //     buttonName = (EventSystem.current.currentSelectedGameObject.name);
    //     for (int i = 0; i <= pages.Length-1; i++)
    //     {
    //         if (isClicked)
    //         {
    //             if (pages[i].name == buttonName)
    //             {
    //                 pages[i].transform.position = defaultPos[i];
    //                 pages[i].transform.localScale = thisObject[i];
    //                 isClicked = false;
    //                 isReverse = true;
    //                 pages[i].SetActive(true);
    //             }
    //         }

    //         else if (pages[i].name == buttonName && !isClicked)
    //         {
    //             thisObject[i] = pages[i].GetComponent<Transform>().localScale;
    //             defaultPos[i] = pages[i].transform.position;
    //             isClicked = true;
    //             isReverse = false;
    //         }
    //     }


    // }
    // void GetDefaultPos()
    // {
    //     for (int i = 0; i <= pages.Length -1; i++)
    //     {
    //         thisObject[i] = pages[i].GetComponent<Transform>().localScale;
    //         defaultPos[i] = pages[i].transform.position;
    //     }
    // }

}
