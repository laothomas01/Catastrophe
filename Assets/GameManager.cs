using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    MainMenu,
    Playing,
    Paused,
    GameOver,
    Win
}
public class GameManager : MonoBehaviour
{

    public GameState currentState;

    void Start()
    {
        currentState = GameState.MainMenu;
    }

    // Update is called once per frame
    void Update()
    {
         switch (currentState)
        {
            case GameState.MainMenu:
                // Handle main menu logic
                break;
            case GameState.Playing:
                // Handle playing logic
                break;
            case GameState.Paused:
                // Handle paused logic
                break;
            case GameState.GameOver:
                // Handle game over logic
                break;
            case GameState.Win:
                // Handle win logic
                break;
            default:
                break;
        }
    }

}
