using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.OnScreen;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    PlayerInput playerInput;
    // Start is called before the first frame update
    public string device_Name;

    public GameObject pauseScreen;
    public GameObject mobilePlatform;
    public GameObject desktopPlatform;

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
        playerInput = GetComponent<PlayerInput>();
        switch(SystemInfo.deviceType)
        {
            case DeviceType.Desktop:
                desktopPlatform.SetActive(true);
                // pauseScreen.GetComponent<OnScreenButton>().enabled = false;
                pauseScreen.GetComponentInChildren<OnScreenButton>().enabled = false;
                break;
            case DeviceType.Handheld:
                mobilePlatform.SetActive(true);
                pauseScreen.GetComponentInChildren<Button>().enabled = false;

                break;
            default:
             Debug.LogError("Device Not Found!");
             break;
        }
        // // scoreManager = FindObjectOfType<ScoreManager>();
        // // Debug.Log(scoreManager.name);
        // // ============================ HANDLING GAME STATES =======================================



        // // =========================================================================================



        // //===================== HANDLING DEVICE GAME CONTROLS ON LOAD =======================================================

        // //@TODO: use a switch statement!

        // if (SystemInfo.deviceType == DeviceType.Desktop)
        // {

        //     // Keyboard_PlayerMovement keyboard = FindObjectOfType<Keyboard_PlayerMovement>(includeInactive: true);
        //     desktopPlatform.SetActive(true);
        //     pauseScreen.GetComponent<OnScreenButton>().enabled = false;
        // }

        // // if (SystemInfo.deviceType == DeviceType.Handheld)
        // // {
        // //     if(mobilePlatform != null)
        // //     {
        // //         mobilePlatform.SetActive(true);
        // //     }
        // //     else
        // //     {
        // //         Debug.LogError("mobileDeviceUI does not exist!");
        // //     }
            
        // // }


        // //game does not support such device
        // if (SystemInfo.deviceType == DeviceType.Unknown)
        // {
        //     Debug.LogError("Device Not Found!");
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
        //if escape button pressed, toggle pause
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

        if (playerInput.actions["TogglePauseResume"].triggered)
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

        //if touched menu button via screen touch, toggle pause
        /*
        1) touch menu button
        2) check current game state
        3) handle touch event based on current game state
        */
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
