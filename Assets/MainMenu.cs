using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public float UIAnimationTime;
    public float maxUIAnimationTime;
    public float UIAnimationSpeed_X;
    public float UIAnimationSpeed_Y;
   
    public GameObject play_button;
    public GameObject exit_button;
    public GameObject option_button;
    public GameObject title_card;
    bool playGame;
    bool exitGame;
    bool options;
    bool canSelect;
        private void Start() {
                playGame = false;
                exitGame = false;
                options = false;
                canSelect = true;
    }
    public void PlayGame()
    {

            // playGame = true;
            // canSelect = false;
            // if(playButtonAnimationTime > maxPlayButtonAnimationTime)
            // {
            // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        
        if(canSelect)
        {
            playGame = true;
            canSelect = false;
        }
        // else
        // {
        //      Debug.Log("CANNOT SELECT");
        // }
            // }
            //  playButtonAnimationTime += Time.deltaTime;

            // Debug.Log(playButtonAnimationTime);
            
    }
    public void Update()
    {
          if(playGame)
          {
            play_button.transform.position += new Vector3(1 * UIAnimationSpeed_X * Time.deltaTime,-1 * UIAnimationSpeed_Y * Time.deltaTime,0);
            exit_button.transform.position += new Vector3(0,-1 * UIAnimationSpeed_Y * Time.deltaTime,0);
            title_card.transform.position += new Vector3(0,1 * UIAnimationSpeed_Y * Time.deltaTime,0);

            if(UIAnimationTime > maxUIAnimationTime)
            {
                playGame = false;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            UIAnimationTime += Time.deltaTime;
          }

    }
    public void ExitGame()
    {
            if(canSelect)
            {

            }
            // else
            // {
            //     Debug.Log("CANNOT SELECT");
            // }
    }
    public void Options()
    {

    }
}
