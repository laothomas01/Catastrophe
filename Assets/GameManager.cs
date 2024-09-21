using UnityEngine;
using UnityEngine.InputSystem;

/*
on start:
    if mobile:

    if PC:

*/
public class GameManager : MonoBehaviour
{
    public GameObject player;
    public GameObject mobileControlsUi;



    void Awake()
    {
        Debug.Log(player);
        PlayerInputManager playerInputManager = player.GetComponent<PlayerInputManager>();
        if (Application.isMobilePlatform) // Mobile COntrols 
        {
            if (mobileControlsUi != null)
            {
                mobileControlsUi.SetActive(true);
            }
            playerInputManager.SetCurrentControlScheme("Gamepad");
        }
    }

}
