
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class QuestItem : MonoBehaviour
{
    // public Items needItem;
    // [HideInInspector]
    // public Inventory inventory;
    // [HideInInspector]
    // public Interactable interactable;
    // public string questName;
    public int questNumber = 1;

    // public UnityEvent eventOnTrigger;



    // // Start is called before the first frame update
    // void Start()
    // {
    //     inventory = GameObject.FindObjectOfType<Inventory>();
    //     interactable = GetComponent<Interactable>();
    // }

    // // Update is called once per frame
    // void Update()
    // {
    //     if (interactable.isHovered)
    //     {
    //         if (Input.GetKeyDown(KeyCode.F))
    //         {
    //             CheckInventory();
    //         }
    //     }

    // }

    // void CheckInventory()
    // {

    //     for (int r = 0; r < inventory.items.Length; r++)
    //     {

    //         if (inventory.items[r].GetComponentInChildren<TextMeshProUGUI>().text == needItem.name)
    //         {
    //            FindObjectOfType<QuestManager>().CheckQuest(questName,true); 
    //            Debug.Log("Quest Done");
    //            inventory.GetDetails(r);
    //            defaultSlot();
    //            inventory.items[r].tag = "Empty";
    //             questNumber++;
    //             eventOnTrigger.Invoke();
    //         }

    //     }



    // }

    // void defaultSlot()
    // {
    //     inventory.itemName.text= "";
    //     inventory.itemImage.sprite = inventory.defaultSprite;
    //     inventory.itemText.itemText= "";
     
    // }


    
}
