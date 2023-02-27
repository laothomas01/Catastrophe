using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOver : MonoBehaviour
{
    bool visibleCursor;
    bool gameOver;
    void Start()
    {
     visibleCursor = false;
        gameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
      public void toggleGameOverScreen()
    {
          Cursor.visible = visibleCursor;
            gameOver = !gameOver;
            visibleCursor = !visibleCursor;
            Time.timeScale = gameOver ? 0 : 1;  
            this.gameObject.SetActive(gameOver);    
    }
    public void retryButton()
    {
         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void goToMainMenu()
    {
        SceneManager.LoadScene("Main_Menu");
    }
}
