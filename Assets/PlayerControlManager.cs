using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControlManager : MonoBehaviour
{
    PlayerInput playerInput;
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        // playerInput.SwitchCurrentActionMap()
    }

}
