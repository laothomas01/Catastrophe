using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    public void PlayGame()
    {
        SceneManager.LoadScene("Level_1");
    }
    void ExitGame()
    {
    }

}
