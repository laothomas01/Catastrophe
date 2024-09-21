using UnityEngine;
using UnityEngine.InputSystem;

/*
on start:
    if mobile:

    if PC:

*/
public class GameManager : MonoBehaviour
{
    // public GameObject player;
    public GameObject mobileControlsUI;

    void Awake()
    {
        // PlayerInputManager playerInputManager = player.GetComponent<PlayerInputManager>();
        // Debug.Log(playerInputManager.GetCurrentControlScheme());
        
        if (mobileControlsUI != null)
        {
            mobileControlsUI.SetActive(true);
        }
        // playerInputManager.SetCurrentControlScheme("Gamepad");



    }

}
