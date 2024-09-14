// // using UnityEngine;
// // using UnityEngine.InputSystem;

// // public class PlayerMovement : MonoBehaviour
// // {
// //     PlayerInput leftJoyStick;
// //     PlayerInput rightStickPress;
// //     public float moveSpeed = 1f;  // Set a default value if not set in the Inspector
// //     public float runSpeedMultiplier = 1f;
// //     public float walkSpeedMultiplier = 1f;

// //     private float moveSpeedMultiplier;
// //     private Vector3 movementInput;
// //     private Vector3 movementDirection;
// //     private Rigidbody rb;
// //     private DeviceManager.PlatformType currentPlatform;
// //     private bool isSprinting;

// //     void Start()
// //     {
// //         isSprinting = false;
// //         rb = GetComponent<Rigidbody>();
// //         leftJoyStick = GetComponent<PlayerInput>();
// //         rightStickPress = GetComponent<PlayerInput>();
// //         moveSpeedMultiplier = walkSpeedMultiplier;

// //     }

// //     void Update()
// //     {
// //         HandleCurrentPlatformControls();
// //         // HandleMoveAnimation();
// //     }

// //     void FixedUpdate()
// //     {
// //         Move();
// //     }

// //     // void HandleMovementInputs()
// //     // {
// //     //     movementInput.y = Input.GetAxisRaw("Vertical");
// //     //     movementDirection = transform.forward * movementInput.y;
// //     //     ToggleSprint();
// //     // }

// //     // void HandleMoveAnimation()
// //     // {
// //     //     bool isMoving = movementInput.x != 0 || movementInput.y != 0;
// //     //     bool isRunning = moveSpeedMultiplier == runSpeedMultiplier;

// //     //     animator.SetBool("isRunning", isRunning && isMoving);
// //     //     animator.SetBool("isWalking", !isRunning && isMoving);
// //     // }

// //     private void Move()
// //     {
// //         rb.AddForce(movementDirection * moveSpeed * moveSpeedMultiplier, ForceMode.Force);
// //         moveSpeedMultiplier = isSprinting ? runSpeedMultiplier : walkSpeedMultiplier;
// //     }

// //     // can be rewritten to fit mobile controls 
// //     private void ToggleSprint()
// //     {
// //         moveSpeedMultiplier = Input.GetKey(KeyCode.LeftShift) ? runSpeedMultiplier : walkSpeedMultiplier;
// //     }

// //     public void SetPlatformControls(DeviceManager.PlatformType platform)
// //     {
// //         currentPlatform = platform;
// //     }

// //     void HandlePcControls()
// //     {
// //         movementInput.y = Input.GetAxisRaw("Vertical");
// //         movementDirection = transform.forward * movementInput.y;
// //         ToggleSprint();
// //     }

// //     // there is no backwards movement when playing on mobile compared to playing keyboard/mouse 
// //     // both come with unique challenges 
// //     void HandleMobileControls()
// //     {
// //         movementInput = leftJoyStick.actions["Move"].ReadValue<Vector2>();
// //         if(movementInput.magnitude > 0.1f)
// //         {
// //             //Calculate movement bsaed on pplayer's current forward direction
// //             movementDirection = new Vector3(movementInput.x,0,movementInput.y);

// //             //Ensure movement direction happens in the forward direction 
// //             movementDirection = transform.forward * movementDirection.magnitude; 
// //         }
// //         else
// //         {
// //             movementDirection = Vector3.zero;
// //         }
// //         if(rightStickPress.actions["Sprint"].IsPressed())
// //         {
// //             if(isSprinting)
// //             {
// //                 isSprinting = false;
// //             }
// //             else
// //             {
// //                 isSprinting = true;
// //             }
// //         }
// //         Debug.Log("is sprinting: " + isSprinting);
// //     }
// //     void HandleCurrentPlatformControls()
// //     {
// //         switch (currentPlatform)
// //         {
// //             case DeviceManager.PlatformType.PC:
// //                 // HandlePcControls();
// //                 HandleMobileControls(); // Placed here for testing purposes 
// //                 break;
// //             case DeviceManager.PlatformType.Mobile:
// //                 HandleMobileControls();
// //                 break;
// //             default:
// //                 throw new System.Exception("Current device type not found");

// //         }
// //     }
// //     [HideInInspector] public bool IsSprinting()
// //     {
// //         return isSprinting;
// //     }
// //     [HideInInspector]
// //     public Vector3 GetMovementInput()
// //     {
// //         return movementInput;
// //     }

// // }

// using UnityEngine;
// using UnityEngine.InputSystem;

// public class PlayerMovement : MonoBehaviour
// {
//     PlayerInput leftJoyStick;
//     PlayerInput rightStickPress;
//     public float moveSpeed = 1f;  // Set a default value if not set in the Inspector
//     public float runSpeedMultiplier = 1f;
//     public float walkSpeedMultiplier = 1f;

//     private float moveSpeedMultiplier;
//     private Vector3 movementInput;
//     private Vector3 movementDirection;
//     private Rigidbody rb;
//     private DeviceManager.PlatformType currentPlatform;
//     private bool isSprinting;

//     void Start()
//     {
//         isSprinting = false;
//         rb = GetComponent<Rigidbody>();
//         leftJoyStick = GetComponent<PlayerInput>();
//         rightStickPress = GetComponent<PlayerInput>();
//         moveSpeedMultiplier = walkSpeedMultiplier;
//     }

//     void Update()
//     {
//         HandleCurrentPlatformControls();
//     }

//     void FixedUpdate()
//     {
//         Move();
//     }

//     // Handle sprint toggle input through InputAction callbacks
//     public void OnSprint(InputAction.CallbackContext context)
//     {
//         if (context.performed)
//         {
//             isSprinting = !isSprinting;
//             moveSpeedMultiplier = isSprinting ? runSpeedMultiplier : walkSpeedMultiplier;
//         }
//     }

//     public void OnMove(InputAction.CallbackContext context)
//     {
//         if (context.performed || context.started)
//         {
//             // Read movement input
//             movementInput = context.ReadValue<Vector2>();
//             movementDirection = new Vector3(movementInput.x, 0, movementInput.y);
//         }

//         if (context.canceled)
//         {
//             // Stop movement when the input is released
//             movementDirection = Vector3.zero;
//         }
//     }

//     private void Move()
//     {
//         rb.AddForce(movementDirection * moveSpeed * moveSpeedMultiplier, ForceMode.Force);
//     }

//     public void SetPlatformControls(DeviceManager.PlatformType platform)
//     {
//         currentPlatform = platform;
//     }

//     void HandleCurrentPlatformControls()
//     {
//         switch (currentPlatform)
//         {
//             case DeviceManager.PlatformType.PC:
//                 HandlePcControls();
//                 break;
//             case DeviceManager.PlatformType.Mobile:
//                 HandleMobileControls();
//                 break;
//             default:
//                 throw new System.Exception("Current device type not found");
//         }
//     }

//     void HandlePcControls()
//     {
//         // For PC controls, read the input from the new Input System using the assigned actions.
//         // This is done in the OnMove and OnSprint methods
//     }

//     void HandleMobileControls()
//     {
//         // Mobile control is now handled through OnMove and OnSprint methods as well
//     }

//     [HideInInspector] public bool IsSprinting()
//     {
//         return isSprinting;
//     }

//     [HideInInspector]
//     public Vector3 GetMovementInput()
//     {
//         return movementInput;
//     }
// }


/*
    component for handling player movement based on the current control schemes:
        - Keyboard/Mouse
        - Keyboard
        - Mobile 
*/
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{
    // public float moveSpeed = 1f;  // Set a default value if not set in the Inspector
    // public float runSpeedMultiplier = 1f;
    // public float walkSpeedMultiplier = 1f;

    // private float moveSpeedMultiplier;
    // private Vector2 movementInput;
    // private Vector3 movementDirection;
    // private Rigidbody rb;
    // private DeviceManager.PlatformType currentPlatform;
    // private bool isSprinting;

    // void Start()
    // {
    //     isSprinting = false;
    //     rb = GetComponent<Rigidbody>();
    //     moveSpeedMultiplier = walkSpeedMultiplier;
    // }

    // void Update()
    // {
    //     // HandleCurrentPlatformControls();
    // }

    // void FixedUpdate()
    // {
    //     Move();
    // }

    // //player toggle sprint mobiel controls 
    // public void On_Sprint_Mobile_Controls(InputAction.CallbackContext context)
    // {
    //     if (context.performed)
    //     {
    //         isSprinting = !isSprinting;
    //         moveSpeedMultiplier = isSprinting ? runSpeedMultiplier : walkSpeedMultiplier;
    //     }
    // }

    // //player movement mobile controls 
    // public void On_Move_Mobile_Controls(InputAction.CallbackContext context)
    // {
    //     if (context.performed || context.started)
    //     {
    //         // Read movement input
    //         movementInput = context.ReadValue<Vector2>();
    //         movementDirection = new Vector3(movementInput.x, 0, movementInput.y);
    //     }

    //     if (context.canceled)
    //     {
    //         // Stop movement when the input is released
    //         movementDirection = Vector3.zero;
    //     }
    // }

    // //universal move function 
    // private void Move()
    // {
    //     rb.AddForce(movementDirection * moveSpeed * moveSpeedMultiplier, ForceMode.Force);
    // }

    // public void SetCurrentDeviceControls(DeviceManager.PlatformType platform)
    // {
    //     currentPlatform = platform;
    // }

    // void HandleCurrentPlatformControls()
    // {
    //     switch (currentPlatform)
    //     {
    //         case DeviceManager.PlatformType.PC:
    //             HandlePcControls();
    //             break;
    //         case DeviceManager.PlatformType.Mobile:
    //             HandleMobileControls();
    //             break;
    //         default:
    //             throw new System.Exception("Current device type not found");
    //     }
    // }

    // void HandlePcControls()
    // {
    //     // For PC controls, read the input from the new Input System using the assigned actions.
    //     // This is done in the OnMove and OnSprint methods
    // }

    // void HandleMobileControls()
    // {
    //     // Mobile control is now handled through OnMove and OnSprint methods as well
    // }

    // [HideInInspector] public bool IsSprinting()
    // {
    //     return isSprinting;
    // }

    // [HideInInspector]
    // public Vector3 GetMovementInput()
    // {
    //     return movementInput;
    // }

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
