using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Michsky.UI.Dark;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameSlot : MonoBehaviour
{
    public TMP_Text slotTitle;
    public RawImage slotImage;
    private SlotData slotData;
    
    Texture initSlotImage;
    MainPanelManager panelManager;

    void Start() {
        initSlotImage = slotImage?.texture;
    }
    public void LoadSlotData(SlotData slotData, MainPanelManager panelManager) {
        this.slotData = slotData;
        this.panelManager = panelManager;
        
        if(slotTitle) slotTitle.text = slotData.name;

        MainPanelButton button = GetComponent<MainPanelButton>();
        if (button) {
            button.buttonText = slotData.name;
        }
        if (!slotData.isEmpty) GetSlotImage();
    }

    private void GetSlotImage()
    {
        if (!slotImage) return;
        if (slotData.isEmpty) return;
        
        string url = Application.persistentDataPath +"/savedslot-" + slotData.slotNumber + ".jpg";
        var bytes = File.ReadAllBytes( url );
        Texture2D texture = new Texture2D( 800, 400 );
        texture.LoadImage( bytes );
        slotImage.texture= texture;
    }

    public void LoadSlot() {
        if (panelManager.currentPanelIndex != slotData.slotNumber) return;
        SceneLoader.instance.LoadGame(slotData);
    }
    public void ResetSlot(){
        if (panelManager.currentPanelIndex != slotData.slotNumber) return; 
        int slotNumber = slotData.slotNumber;
        slotData = new SlotData();
        SaveSystem.SaveSlot(slotData, slotNumber);
        LoadSlotData(slotData, panelManager);
    }
}
