using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class UIControls : MonoBehaviour
{
    public InGameSettings[] inGameSettings;
    public GameObject inventoryMenu;
    public GameObject settingsMenu;
    public GameObject inGameMenu;
    public GameObject pageMenu;
    public GameObject exitMenu;
    public GameObject titleMenu;


    private void Update()
    {
        InventoryControl();
        foreach (InGameSettings i in inGameSettings)
        {
            i.buttonName = i.button.name;
        }

    }



    public void InventoryControl()
    {
        if (InspectManager.instance.isActivated) return;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PanelControl(inGameMenu);
            
            OnInventory();

        }
    }

    void PanelControl(GameObject panel)
    {
        if (GameManager.instance.isPaused)
        {
            GameManager.instance.Pause(false);
            panel.SetActive(false);

        }
        else
        {
            GameManager.instance.Pause(true);
            panel.SetActive(true);
        }

        
    }
    public void OnSettings()
    {
        SelectMenu(settingsMenu);

    }
    public void OnInventory()
    {
        SelectMenu(inventoryMenu);

    }
    public void OnPage()
    {
        SelectMenu(pageMenu);

    }

    public void TitleScreen()
    {
        SelectMenu(titleMenu);

    }

    public void Exit()
    {
        SelectMenu(exitMenu);

    }

    public void ExitConfirm()
    {
        Application.Quit();
    }

    public void TitleMenu()
    {
        int buildIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(buildIndex - 1);
    }



    public void SelectMenu(GameObject menuButton)
    {
        foreach (InGameSettings i in inGameSettings)
        {
            i.button.SetActive((menuButton.name == i.buttonName) ? true:false); 
        }

    }


}
