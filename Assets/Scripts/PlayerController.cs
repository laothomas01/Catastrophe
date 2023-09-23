using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    PlayerControls playerControls_; // probably should put this in another file/component
                                    //unity input controls

    //specific unity input controls
    private InputAction move_;
    private InputAction sprint_;

    private InputAction attack_;

    void Awake()
    {
        // playerControls_ = new PlayerControls();
        // move_ = playerControls_.Player.Move;
        // sprint_ = playerControls_.Player.Sprint;
        // attack_ = playerControls_.Player.Fire;
        // move();
    }
    public PlayerControls playerControls()
    {
        return playerControls_;
    }
    public InputAction move()
    {
        return move_;
    }

    public InputAction sprint()
    {
        return sprint_;
    }
    public InputAction fire()
    {
        return attack_;
    }
    public Mouse mouse()
    {
        return Mouse.current;
    }

}