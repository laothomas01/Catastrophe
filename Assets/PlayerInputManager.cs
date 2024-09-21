using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    PlayerInput playerInput;
    // Start is called before the first frame update
    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

    }
    public void SetCurrentControlScheme(string controlScheme)
    {
        playerInput.SwitchCurrentControlScheme(controlScheme);
    }
    public string GetCurrentControlScheme()
    {
        return playerInput.currentControlScheme;
    }


}
