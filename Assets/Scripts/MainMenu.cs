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
    public string play_scene_name;
    bool playGame;
    bool exitGame;
    bool options;
    bool canSelect;
        private void Start() {
            // UIAnimationTime = 0;
            //     playGame = false;
            //     exitGame = false;
            //     options = false;
            //     canSelect = true;


    }
    public void PlayGame()
    {

    //    Debug.Log(playGame);
    //     playGame = !playGame;

                SceneManager.LoadScene(play_scene_name);
     
            
    }
    public void Update()
    {


        //tongue and cheek animation
            // if(playGame)
            // {
            //       play_button.transform.position += new Vector3(1 * UIAnimationSpeed_X * Time.deltaTime,-1 * UIAnimationSpeed_Y * Time.deltaTime,0);
            // exit_button.transform.position += new Vector3(0,-1 * UIAnimationSpeed_Y * Time.deltaTime,0);
            // title_card.transform.position += new Vector3(0,1 * UIAnimationSpeed_Y * Time.deltaTime,0);

            // if( UIAnimationTime > maxUIAnimationTime)
            // {
            // }
            // UIAnimationTime += Time.deltaTime;

            // }

    }
    public void ExitGame()
    {
            Application.Quit();
    }
    public void Options()
    {

    }
}
