using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Load : MonoBehaviour
{
    public void LoadSlot(int slotNumber)
    {
        PlayerPrefs.SetInt("CurrentSlot", slotNumber);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
