using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public string device_Name;
    ScoreManager scoreManager;

    enum GameState
    {
        GAME_WIN,
        GAME_OVER,
        GAME_PAUSE,
        GAME_PLAY
    }
    GameState gameState;
    void Start()
    {
        gameState = GameState.GAME_PLAY;
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
        switch (gameState)
        {
            case GameState.GAME_PAUSE:
                
                break;
            case GameState.GAME_OVER:
                break;
            case GameState.GAME_WIN:
                break;
            default:
                break;
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            gameState = GameState.GAME_PAUSE;
            Debug.Log("Paused!");
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
