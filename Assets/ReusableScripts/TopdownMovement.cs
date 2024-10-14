
using UnityEngine;
using UnityEngine.InputSystem;


//handling unity new input manager 
[RequireComponent(typeof(PlayerInputManager))]
//player physics 
[RequireComponent(typeof(Rigidbody))]

public class TopdownMovement : MonoBehaviour
{
    //@TODO Add a toggling on/off/implementation for mouse cursor 
    // public GameObject mouseCursor;
    // Vector3 cursorPosition;

    // [SerializeField] private bool toggleMouseCursorOn = false;
    // public 


    private LayerMask playerLayerMask;
    public float walkSpeed = 300f;
    public float sprintMultiplier = 2f; 
    private float currentSpeed; 
    // private float moveSpeedMultiplier = 1f;
    // [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float rotationSpeed = 1f; 

    // [SerializeField] private float moveSpeed = 1f;
    // [SerializeField] private float rotationSpeed = 1f;
    // [SerializeField] private int walkSpeedMultiplier = 300;
    // [SerializeField] private int sprintSpeedMultiplier = 500;
    
    private Vector2 mousePosition = Vector2.zero;
    private bool isMoving = false;
    private bool isSprinting = false;
    private Rigidbody rb;
    private Vector2 joystickInput = Vector2.zero;
    private PlayerInputManager playerInputManager;
    private Vector3 currentLookDirection = Vector3.zero;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerInputManager = GetComponent<PlayerInputManager>();
        playerLayerMask = 1 << 7;
        currentSpeed = walkSpeed;
    }

    private void Update()
    {

        if (isSprinting)
        {
            currentSpeed = walkSpeed * sprintMultiplier;
        }
        else
        {
            currentSpeed = walkSpeed; 
        }

    }
    private void FixedUpdate()
    {
        RotatePlayer();

        if (isMoving)
        {
            MoveForward();
        }
        if (isMoving)
        {
            MoveForward();
        }
    }

    // ====================== these callback functions are mapped to an action in the input action asset ========================

    // event driven rotation input 
    // use keyboard mouse control scheme action to invoke an input event 
    // use mobile control scheme action to invoke an input event 
    // check input action asset's control scheme's actions
    // more inputs can be added to the movement control scheme 

    public void OnMovement(InputAction.CallbackContext value)
    {
        if (playerInputManager.GetCurrentControlScheme() == "Gamepad" || playerInputManager.GetCurrentControlScheme() == "KeyboardMouse")
        {
            if (value.performed)
            {
                isMoving = true;
            }
            else if (value.canceled)
            {
                isMoving = false;
            }
        }
    }

    // event driven rotation input 
    // use keyboard mouse control scheme action to invoke an input event 
    // use mobile control scheme action to invoke an input event 
    // check input action asset's control scheme's actions

    public void OnRotate(InputAction.CallbackContext value)
    {
        if (value.performed)
        {
            if (playerInputManager.GetCurrentControlScheme() == "KeyboardMouse")
            {
                mousePosition = value.ReadValue<Vector2>();
            }
            if (playerInputManager.GetCurrentControlScheme() == "Gamepad")
            {
                joystickInput = value.ReadValue<Vector2>();
            }
        }
        else if (value.canceled)
        {
            joystickInput = Vector2.zero;
        }
    }

    // event driven rotation input 
    // use keyboard mouse control scheme action to invoke an input event 
    // use mobile control scheme action to invoke an input event 
    // check input action asset's control scheme's actions

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

    // =====================================================================================================================

    //specifically coded only move forward and in direction player is facing 
    // specifically made for a top down perspecitive 
    // will have to be updated if want to move in x or y direction. 
    // written to only move in Z direction 
    private void MoveForward()
    {
        // // rigidbody.velocity = vector3.lerp(rigidbody.velocity, desiredVelocity, 0.3f)
        // Vector3 forwardMovement = transform.forward * moveSpeed * moveSpeedMultiplier * Time.deltaTime;
        // // rb.MovePosition(transform.position + forwardMovement);
        // // transform.Translate(forwardMovement.x, forwardMovement.z, forwardMovement.);
        // // rb.velocity = Vector3.Lerp(rb.velocity, forwardMovement, 0.3f);
        // // rb.velocity += forwardMovement; 
        // // rb.AddForce(forwardMovement, ForceMode.Force);

        // Apply force to move forward
        Vector3 forwardMovement = transform.forward * currentSpeed * Time.fixedDeltaTime;
        rb.velocity = forwardMovement;
        // rb.AddForce(forwardMovement, ForceMode.VelocityChange);

        // // Clamp the maximum speed to control snappiness
        // if (rb.velocity.magnitude > moveSpeed * moveSpeedMultiplier)
        // {
        //     rb.velocity = rb.velocity.normalized * moveSpeed * moveSpeedMultiplier;
        // }
    }

    private void RotatePlayer()
    {
        if (playerInputManager.GetCurrentControlScheme() == "KeyboardMouse")
        {
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);

            //do this to prevent loss of input reading from raycast when raycast hits player 
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, ~playerLayerMask))
            {
                Vector3 mousePosition = new Vector3(hit.point.x, transform.position.y, hit.point.z);
                Vector3 directionToLook = (mousePosition - transform.position).normalized; // Calculate direction to mouse
                currentLookDirection = directionToLook;
                Quaternion targetRotation = Quaternion.LookRotation(directionToLook);        // Determine target rotation
                rb.MoveRotation(Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime)); // Smooth rotation with Rigidbody
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
                rb.MoveRotation(Quaternion.Slerp(transform.rotation, rotationAxis, rotationSpeed * Time.fixedDeltaTime));
            }
        }

    }

    public bool IsSprinting()
    {
        return isSprinting;
    }
    public bool IsMoving()
    {
        return isMoving;
    }

    //the direction you are currently facing 
    public Vector3 GetCurrentLookDirection()
    {
        return currentLookDirection;
    }

    public void SetCurrentLookDirection(Vector3 lookDirection)
    {
        currentLookDirection = lookDirection;
    }

}
