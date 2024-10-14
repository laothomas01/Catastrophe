using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExitButton : MonoBehaviour
{
    public Button exit;
    
    public void ExitGame()
    {
        Scene scene = SceneManager.GetActiveScene();
        if(scene.name == "Win_Screen")
        {
            SceneManager.LoadScene("Level_1"); // a temporary line of code maybe??? 
        }
        // else go to main menu 

        //else exit application 
    }
}
