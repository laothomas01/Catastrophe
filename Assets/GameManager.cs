using UnityEngine;
using UnityEngine.SceneManagement;

/*
on start:
    if mobile:

    if PC:

*/
public class GameManager : MonoBehaviour
{
    // public GameObject player;
    public GameObject mobileControlsUI;
    public GameObject gameOverUI;
    bool isPlayerFound = false; 
    bool win = false;
    FurnitureManager furnitureManager;
    void Awake()
    {
        Time.timeScale = 1;
        if(Application.isMobilePlatform)
        {
            mobileControlsUI.SetActive(true);
        }
        else
        {
            mobileControlsUI.SetActive(false);
        }
        furnitureManager = FindObjectOfType<FurnitureManager>();

    }
    public void SetIsPlayerFound(bool found)
    {
        isPlayerFound = found; 
    }

    public void Update()
    {
        if(isPlayerFound)
        {
            GameOver();
        }
        if(furnitureManager.GetCurrentHeavyFurnitureCount() == 0)
        {
            win = true;
        }
        if(win)
        {
            WinScreen();
        }
        
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        gameOverUI.SetActive(true);

    }
    public void WinScreen()
    {
        SceneManager.LoadScene("Win_Screen");
    }

}
