using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    Button restart;
    Button exit;
    void Start()
    {
        //disable the camera shake 

        FindObjectOfType<MainCamera>().CanShake(false);
        if (restart)
        {
            restart.onClick.AddListener(RestartGame);
        }
        if (exit)
        {
            exit.onClick.AddListener(ExitGame);
        }
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        FindObjectOfType<GameManager>().SetIsPlayerFound(false);
    }
    public void ExitGame()
    {

    }
}
