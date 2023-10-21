//================= OBSELETE =============

// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.SceneManagement;
// public class ManageGameState : MonoBehaviour
// {
//     public GameObject pauseMenu = null;
//     bool visibleCursor;
    
//     bool isPaused;

//     void Start()
//     {

//         visibleCursor = false;
//         isPaused = false;
//     }
//     private void Update() {


//         if (Input.GetKeyDown(KeyCode.Escape))
//         {
//             togglePause();
//         }
//     }    

//     public bool getIsPaused()
//     {
//         return isPaused;
//     }
//     public void togglePause()
//     {
           
//             isPaused = !isPaused;
//             visibleCursor = !visibleCursor;
//               Cursor.visible = visibleCursor;
//             Time.timeScale = isPaused ? 0 : 1;       
//             pauseMenu.SetActive(isPaused);
            
//     }

 
  
//     public void backToMainMenu()
//     {
//         SceneManager.LoadScene("Main_Menu");
//     }

    

       
// }
