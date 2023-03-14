using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public AudioClip gameOverSFX;
    public void Show()
    {
        // Show gameover panel
        this.gameObject.SetActive(true);
        // Freeze all animations first
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        FXSoundSystem.Instance.PlayOneShot(gameOverSFX);
    }
    
    public void Restart(){
        SceneLoader.instance.GoToScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Quit(){
        SceneLoader.instance.LoadMainMenu();
    }
}
