using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ManageGameState : MonoBehaviour
{
    public GameObject pauseMenu = null;
    public GameObject gameOverScreen = null;
    bool visibleCursor;
    
    bool isPaused;
    bool gameOver;
    void Start()
    {
        Time.timeScale = 1;

        visibleCursor = false;
        Cursor.visible = visibleCursor;
        isPaused = false;
        gameOver = false;
    }
    private void Update() {
  
  
        if (Input.GetKeyDown(KeyCode.Escape))
        {
           togglePause();
        }

        Cursor.visible = visibleCursor;
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
    public void toggleGameOverScreen()
    {
            isPaused = !isPaused;
            visibleCursor = !visibleCursor;
             Cursor.visible = visibleCursor;

            Time.timeScale = isPaused ? 0 : 1;  
        gameOverScreen.SetActive(isPaused);
    }
    public void toggleRetry()
    {
       ResetGame();
    }
    public void ResetGame()
    {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    

       
}
