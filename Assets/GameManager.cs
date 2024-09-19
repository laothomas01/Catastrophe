using UnityEngine;
using UnityEngine.InputSystem;
public class GameManager : MonoBehaviour
{

    public GameObject player;
    PlayerInput playerInput;
    public GameObject mobileControlsUi;
    void Start()
    {
        playerInput = player.GetComponent<PlayerInput>();
        if (Application.isMobilePlatform)
        {   
            playerInput.SwitchCurrentControlScheme("Gamepad");
            if (mobileControlsUi != null)
            {
                Debug.Log("Mobile");
                mobileControlsUi.SetActive(true);
            }
        }

    }

}
