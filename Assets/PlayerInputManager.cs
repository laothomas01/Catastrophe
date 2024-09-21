using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    public void SetCurrentControlScheme(string controlScheme)
    {
        PlayerInput playerInput = GetComponent<PlayerInput>();
        playerInput.SwitchCurrentControlScheme(controlScheme);
    }
    public string GetCurrentControlScheme()
    {
        PlayerInput playerInput = GetComponent<PlayerInput>();
        return playerInput.currentControlScheme;
    }


}
