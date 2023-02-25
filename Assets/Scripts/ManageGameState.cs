using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageGameState : MonoBehaviour
{
    public GameObject pauseMenu = null;
    bool visibleCursor;
    
    bool isPaused;
    void Start()
    {
        visibleCursor = false;
        Cursor.visible = visibleCursor;
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
            Cursor.visible = visibleCursor;
            isPaused = !isPaused;
            visibleCursor = !visibleCursor;
            Time.timeScale = isPaused ? 0 : 1;       
           pauseMenu.SetActive(isPaused);
            
    }
       
}
