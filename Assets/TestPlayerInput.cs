//==============================================================
using UnityEngine;
using UnityEngine.InputSystem;

/*
    file for handling player movement 
*/
[RequireComponent(typeof(Rigidbody))]
public class TestPlayerInput : MonoBehaviour
{
    [SerializeField] private LayerMask playerLayerMask;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 10f;
    private Vector2 mousePosition = Vector2.zero;
    private bool isMovingForward = false;          // To track if the player is holding "W" to move
    private Rigidbody playerRigidbody;             // Reference to the player's Rigidbody
    PlayerInput playerInput;
    private string currentControlSceheme;
    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
        currentControlSceheme = playerInput.currentControlScheme;
    }

    private void FixedUpdate()
    {
        // Rotate the player toward the mouse position
        RotatePlayer();
        // If the player is holding "W", move the player forward
        if (isMovingForward)
        {
            MoveForward();
        }
    }

    //different ways to handle player moving 
    // Handle movement input (W key)
    // Handle movement input (Gamepad Joystick)
    public void OnMovement(InputAction.CallbackContext value)
    {
        if (value.performed)
        {
            isMovingForward = true; // Start moving forward when "W" is pressed
        }
        else if (value.canceled)
        {
            isMovingForward = false; // Stop moving forward when "W" is released
        }
    }

    // Handle mouse rotation input
    public void OnRotate(InputAction.CallbackContext value)
    {
        if (value.performed)
        {
            if (currentControlSceheme == "KeyboardMouse")
            {
                mousePosition = value.ReadValue<Vector2>(); // Update mouse position
            }
            else if (currentControlSceheme == "Keyboard")
            {
                //@TODO TBD
            }
            else if (currentControlSceheme == "Mobile")
            {
                
            }
        }
    }

    //general function for player's forward movement 
    private void MoveForward()
    {
        Vector3 forwardMovement = transform.forward * moveSpeed * Time.fixedDeltaTime;
        playerRigidbody.AddForce(forwardMovement, ForceMode.Force);
    }

    //different control schemes that allow player to rotate 
    private void RotatePlayer()
    {
        //or  control schemes that require screen to world interactions 
        Ray ray;
        if (currentControlSceheme == "KeyboardMouse")
        {
            ray = Camera.main.ScreenPointToRay(mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, ~playerLayerMask))
            {
                Vector3 targetPosition = new Vector3(hit.point.x, transform.position.y, hit.point.z);
                Vector3 directionToLook = (targetPosition - transform.position).normalized; // Calculate direction to mouse
                Quaternion targetRotation = Quaternion.LookRotation(directionToLook);        // Determine target rotation
                playerRigidbody.MoveRotation(Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime)); // Smooth rotation with Rigidbody
            }
        }
        else if (currentControlSceheme == "Keyboard")
        {
            //@TODO TBD
        }
        else if (currentControlSceheme == "Mobile")
        {

        }

    }
}


