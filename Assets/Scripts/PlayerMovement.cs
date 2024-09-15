
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
    PlayerInput playerInput;
    private string currentControlScheme;
    private Vector2 joystickInput = Vector2.zero;
    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
        playerInput.SwitchCurrentControlScheme("Gamepad");
        currentControlScheme = playerInput.currentControlScheme;
    }

    private void FixedUpdate()
    {
        // Rotate the player toward the mouse position
        RotatePlayer();
        // If the player is holding "W", move the player forward
        if (isSprinting)
        {
            moveSpeedMultiplier = 500f;
        }
        else
        {
            moveSpeedMultiplier = 100f;
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
        if (value.performed)
        {
            if (currentControlScheme == "KeyboardMouse")
            {
                isMovingForward = true;
            }
            else if (currentControlScheme == "Gamepad")
            {
                isMovingForward = true;
            }
        }
        else if (value.canceled)
        {
            isMovingForward = false; // Stop moving forward when "W" is released
        }
    }
    public void OnRotate(InputAction.CallbackContext value)
    {
        if (value.performed)
        {
            if (currentControlScheme == "KeyboardMouse")
            {
                mousePosition = value.ReadValue<Vector2>();
            }
            else if (currentControlScheme == "Gamepad")
            {
                joystickInput = value.ReadValue<Vector2>();
            }
        }
        else if (value.canceled)
        {
            joystickInput = Vector2.zero;
        }
    }

    // public void OnRotate(InputAction.CallbackContext value)
    // {

    //     if (value.performed)
    //     {
    //         if (currentControlScheme == "KeyboardMouse")
    //         {
    //         }
    //         else if (currentControlScheme == "Gamepad")
    //         {
    //             Debug.Log(currentControlScheme);
    //             // joystickInput = value.ReadValue<Vector2>();
    //         }
    //     }

    // }

    public void OnSprint(InputAction.CallbackContext value)
    {
        if (value.performed)
        {
            isSprinting = true;
        }
        if (value.canceled)
        {
            isSprinting = false;
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
        if (currentControlScheme == "KeyboardMouse")
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

        if (currentControlScheme == "Keyboard")
        {
            //@TODO TBD
        }

        if (currentControlScheme == "Gamepad")
        {
            // Check if there's any significant input from the joystick to rotate
            if (joystickInput.sqrMagnitude > 0.1f) // Ignore small inputs (dead zone)
            {
                // Convert joystick input (X and Y) to a direction in the X-Z plane
                //Rotate around Y axis and move in X-Z plane 
                Vector3 directionToLook = new Vector3(joystickInput.x, 0f, joystickInput.y).normalized;
                Quaternion rotationAxis = Quaternion.LookRotation(directionToLook);
                // Rotate the player by multiplying the current rotation with the new rotation
                playerRigidbody.MoveRotation(Quaternion.Slerp(transform.rotation, rotationAxis, rotationSpeed * Time.fixedDeltaTime));
            }
        }

    }
}
