using UnityEngine;
using UnityEngine.InputSystem;

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
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        gameOverUI.SetActive(true);

    }

}
