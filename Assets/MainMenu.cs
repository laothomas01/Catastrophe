using UnityEngine;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour
{
    public Button play;
    public Button exit;
    public Button option;


    void Start()
    {
        if (play)
        {
            play.onClick.AddListener(PlayGame);
        }
        if (exit)
        {
            exit.onClick.AddListener(ExitGame);
        }
        if(option)
        {
            option = GetComponentInChildren<Button>();
        }

    }
    void PlayGame()
    {
        Debug.Log("You have clicked the play button");
    }
    void ExitGame()
    {
        Debug.Log("Exit Button");
    }

}
