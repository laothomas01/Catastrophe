using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageGameState : MonoBehaviour
{
    public GameObject pauseMenu = null;
    
    bool isPaused;
    void Start()
    {
        isPaused = false;
    }
    private void Update() {
  
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            togglePause();
            
        }
    }    

    public bool getIsPaused()
    {
        return isPaused;
    }
    public void togglePause()
    {
            isPaused = !isPaused;
            Time.timeScale = isPaused ? 0 : 1;       
           pauseMenu.SetActive(isPaused);
            
    }
       
}
