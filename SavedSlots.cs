using System.Collections;
using System.Collections.Generic;
using Michsky.UI.Dark;
using UnityEngine;
using UnityEngine.UI;

public class SavedSlots : MonoBehaviour
{
    public int maxNumOfSlots = 4;
    public Transform slotTabsParent;
    public Transform slotDetailsParent;
    public GameSlot savedSlotTabPrefab;
    public GameSlot savedSlotDetailsPrefab;

    MainPanelManager panelManager;
    GameSlot[] slotTabs;
    GameSlot[] slotDetails;
    // GameSlot[] slots;
    // Start is called before the first frame update
    void Start()
    {
        panelManager = GetComponent<MainPanelManager>();
        slotTabs = new GameSlot[maxNumOfSlots];
        slotDetails = new GameSlot[maxNumOfSlots];
        for (int i = 0; i < maxNumOfSlots; i++) {
            SlotData slotData = SaveSystem.LoadSlot(i);
            slotTabs[i] = Instantiate(savedSlotTabPrefab, slotTabsParent);
            slotDetails[i] = Instantiate(savedSlotDetailsPrefab, slotDetailsParent);

            slotTabs[i].LoadSlotData(slotData, panelManager);
            slotDetails[i].LoadSlotData(slotData, panelManager);

            panelManager.AddNewItem(slotData.name + i, slotDetails[i].gameObject, slotTabs[i].gameObject);
        }
        panelManager.UpdateClickListeners();
        panelManager.UpdatePanels();
    }

}
