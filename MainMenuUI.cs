using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public Menu[] menu;
    public GameObject panelFade;
    public GameObject menuSettings;
    public GameObject loadSettings;
    public LayerMask layerMask;
    // Start is called before the first frame update
    void Start()
    {

    }
    
    // Update is called once per frame
    void Update()
    {
  
     /*   if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                Debug.Log(hit.collider.name);
                foreach (Menu m in menu)
                {

                    m.defaultMat = m.menuButton.GetComponent<Material>();
                    if (hit.collider.name == m.buttonName)
                    {
                        
                        if (m.buttonName == "NewGame")
                        {
                           
                            m.menuButton.GetComponent<MeshRenderer>().material = m.onClick;
                            StartCoroutine(StartGame(2f));

                        }

                        if (m.buttonName == "Load")
                        { 
                            Load();
                        }

                        if (m.buttonName == "Settings")
                        {
                            SettingsMain();
                        }

                        if (m.buttonName == "Exit")
                        {
                            ExitGame();
                        }
                       
                    }
                   
                }
            }
        }*/
    }

    public void Load()
    {
        loadSettings.SetActive(true);
    }

    public IEnumerator StartGame(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        int buildIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(buildIndex+1);
        
    }
    public IEnumerator Wait(float waitTime)
    {

        yield return new WaitForSeconds(waitTime);
    }

    public void NewGame()
    {
        StartCoroutine(Wait(1f));
        int buildIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(buildIndex + 1);
    }

    public void SettingsMain()
    {
        menuSettings.SetActive(true);
    }



    IEnumerator PlayAnimation( string animName, float waitTime)
    {
        
        yield return new WaitForSeconds(waitTime);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void CloseSettings()
    {
        menuSettings.SetActive(false);
    }

}
