using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public string device_Name;
    ScoreManager scoreManager;

    public GameObject pauseScreen;
    public enum GameState
    {

        GAME_OVER,
        GAME_PAUSE,
        GAME_PLAY
    }
   public GameState currentGameState;
    public GameState previousGameState;
    void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
        // Debug.Log(scoreManager.name);
        // ============================ HANDLING GAME STATES =======================================



        // =========================================================================================



        //===================== HANDLING DEVICE GAME CONTROLS ON LOAD =======================================================

        //This is the Text for the Label at the top of the screen
        //Check if the device running this is a console
        // if(SystemInfo.deviceType == DeviceType.Desktop)
        // {
        //     Keyboard_PlayerMovement keyboard = FindObjectOfType<Keyboard_PlayerMovement>(includeInactive: true);

        //     keyboard.gameObject.SetActive(true);
        // }

        // if (SystemInfo.deviceType == DeviceType.Console)
        // {
        //     //Change the text of the label
        //     m_DeviceType = "Console";
        // }

        // // //Check if the device running this is a handheld
        // if (SystemInfo.deviceType == DeviceType.Handheld)
        // {
        //     // GameObject handHeldUI = FindInActiveObjectByName("handHeldUI");
        //     GameObject handHeldDeviceUI = GameObject.Find("HandheldDeviceUI");
        //     Debug.Log(handHeldDeviceUI);

        // }

        // //Check if the device running this is unknown
        // if (SystemInfo.deviceType == DeviceType.Unknown)
        // {
        //     m_DeviceType = "Unknown";
        // }

        //========================================
    }
    void Update()
    {
        switch (currentGameState)
        {
            case GameState.GAME_PAUSE:

            //this is for PC controls
                CheckForPauseAndResume();
                break;
            case GameState.GAME_OVER:
                break;
            case GameState.GAME_PLAY:

                // this is for PC controls
                CheckForPauseAndResume();
                break;
            default:
                Debug.LogWarning("STATE DOES NOT EXIST!");
                break;
        }

        // if(Input.GetKeyDown(KeyCode.Escape))
        // {
        //     currentGameState = GameState.GAME_PAUSE;
        // }
        // lastGameState = currentGameState;

    }
    public void PauseGame()
    {
        previousGameState = currentGameState;
        ChangeState(GameState.GAME_PAUSE);
        Time.timeScale = 0f; // Stop the game
        pauseScreen.SetActive(true);
        Debug.Log("Game is paused");
    }
    public void ResumeGame()
    {
        ChangeState(previousGameState);
        Time.timeScale = 1f;
        pauseScreen.SetActive(false);
        Debug.Log("Game is resumed");
    }
    public void ChangeState(GameState newState)
    {
        currentGameState = newState;
    }
    void CheckForPauseAndResume()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentGameState == GameState.GAME_PAUSE)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }



    //     GameObject FindInActiveObjectByName(string name)
    // {
    //     Transform[] objs = Resources.FindObjectsOfTypeAll<Transform>() as Transform[];
    //     for (int i = 0; i < objs.Length; i++)
    //     {
    //         if (objs[i].hideFlags == HideFlags.None)
    //         {
    //             if (objs[i].name == name)
    //             {
    //                 return objs[i].gameObject;
    //             }
    //         }
    //     }
    //     return null;
    // }

}
