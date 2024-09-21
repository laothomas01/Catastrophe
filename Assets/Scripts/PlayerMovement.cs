
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.InputSystem;


/// <summary>
/// 
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private LayerMask playerLayerMask;
    [SerializeField] private float moveSpeed = 1f;
    private float moveSpeedMultiplier = 1f;
    [SerializeField] private float rotationSpeed = 1f;
    private Vector2 mousePosition = Vector2.zero;
    private bool isMovingForward = false;          // To track if the player is holding "W" to move
    private bool isSprinting = false; // To track if player is holding "Shift" for sprint 
    private Rigidbody playerRigidbody;             // Reference to the player's Rigidbody
    // PlayerInput playerInput;
    // private string currentControlScheme;
    private Vector2 joystickInput = Vector2.zero;
    public float walkSpeedMultiplier = 300f;
    public float sprintSpeedMultiplier = 500f;

    PlayerInputManager playerInputManager;

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        playerInputManager = GetComponent<PlayerInputManager>();
    }
    private void Update()
    {
        // Debug.Log("IsSprintin:" + isSprinting);
    }
    private void FixedUpdate()
    {
        // Rotate the player toward the mouse position
        RotatePlayer();
        // If the player is holding "W", move the player forward
        if (isSprinting)
        {
            moveSpeedMultiplier = sprintSpeedMultiplier;
        }
        else
        {
            moveSpeedMultiplier = walkSpeedMultiplier;
        }

        if (isMovingForward)
        {
            MoveForward();
        }
        // Debug.Log("MoveSpeedMultiplier: " + moveSpeedMultiplier);
    }

    //event driven handling for movement input 
    public void OnMovement(InputAction.CallbackContext value)
    {
        if (playerInputManager.GetCurrentControlScheme() == "KeyboardMouse")
        {
            Debug.Log("OnMovement Current control scheme : " + playerInputManager.GetCurrentControlScheme());

            if (value.performed)
            {
                isMovingForward = true;
            }
            else if (value.canceled)
            {
                isMovingForward = false; // Stop moving forward when "W" is released
            }
        }
        else if (playerInputManager.GetCurrentControlScheme() == "Gamepad")
        {
            Debug.Log("OnMovement Current control scheme : " + playerInputManager.GetCurrentControlScheme());
            if (value.performed)
            {
                isMovingForward = true;
            }
            else if (value.canceled)
            {
                isMovingForward = false; // Stop moving forward when "W" is released
            }
        }
    }
    public void OnRotate(InputAction.CallbackContext value)
    {
        if (value.performed)
        {
            if (playerInputManager.GetCurrentControlScheme() == "KeyboardMouse")
            {
                mousePosition = value.ReadValue<Vector2>();
            }
            else if (playerInputManager.GetCurrentControlScheme() == "Gamepad")
            {
                joystickInput = value.ReadValue<Vector2>();
            }
        }
        else if (value.canceled)
        {
            joystickInput = Vector2.zero;
        }
    }

    public void OnSprint(InputAction.CallbackContext value)
    {
        if (playerInputManager.GetCurrentControlScheme() == "KeyboardMouse")
        {
            if (value.performed)
            {
                isSprinting = true;
            }
            else if (value.canceled)
            {
                isSprinting = false;
            }
        }
        else if (playerInputManager.GetCurrentControlScheme() == "Gamepad")
        {
            if (value.performed && isSprinting)
            {
                isSprinting = false;
            }
            else if (value.performed && !isSprinting)
            {
                isSprinting = true;
            }
        }
    }

    //general function for player's forward movement 
    private void MoveForward()
    {
        Vector3 forwardMovement = transform.forward * moveSpeed * moveSpeedMultiplier * Time.fixedDeltaTime;
        playerRigidbody.AddForce(forwardMovement, ForceMode.Force);
    }

    //different control schemes that allow player to rotate 
    private void RotatePlayer()
    {

        //or  control schemes that require screen to world interactions 
        if (playerInputManager.GetCurrentControlScheme() == "KeyboardMouse")
        {
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, ~playerLayerMask))
            {
                Vector3 targetPosition = new Vector3(hit.point.x, transform.position.y, hit.point.z);
                Vector3 directionToLook = (targetPosition - transform.position).normalized; // Calculate direction to mouse
                Quaternion targetRotation = Quaternion.LookRotation(directionToLook);        // Determine target rotation
                playerRigidbody.MoveRotation(Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime)); // Smooth rotation with Rigidbody
            }
        }

        else if (playerInputManager.GetCurrentControlScheme() == "Keyboard")
        {
            //@TODO TBD
        }

        else if (playerInputManager.GetCurrentControlScheme() == "Gamepad")
        {
            // Check if there's any significant input from the joystick to rotate
            if (joystickInput.sqrMagnitude > 0.1f) // Ignore small inputs (dead zone)
            {
                // Convert joystick input (X and Y) to a direction in the X-Z plane
                Vector3 directionToLook = new Vector3(joystickInput.x, 0f, joystickInput.y).normalized;
                //Rotate around Y axis and move in X-Z plane 
                Quaternion rotationAxis = Quaternion.LookRotation(directionToLook);
                // Rotate the player by multiplying the current rotation with the new rotation
                playerRigidbody.MoveRotation(Quaternion.Slerp(transform.rotation, rotationAxis, rotationSpeed * Time.fixedDeltaTime));
            }
        }

    }
    public bool IsSprinting()
    {
        return isSprinting;
    }
    public bool IsMovingForward()
    {
        return isMovingForward;
    }

}
