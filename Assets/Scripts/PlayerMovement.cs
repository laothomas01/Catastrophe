using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    PlayerInput leftJoyStick;
    public float moveSpeed = 1f;  // Set a default value if not set in the Inspector
    public float runSpeedMultiplier = 1f;
    public float walkSpeedMultiplier = 1f;

    private float moveSpeedMultiplier;
    private Vector3 movementInput;
    private Vector3 movementDirection;
    private Rigidbody rb;
    private Animator animator;
    private DeviceManager.PlatformType currentPlatform;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        leftJoyStick = GetComponent<PlayerInput>();
        moveSpeedMultiplier = walkSpeedMultiplier;
    }

    void Update()
    {
        HandleCurrentPlatformControls();
        // HandleMoveAnimation();
    }

    void FixedUpdate()
    {
        Move();
    }

    // void HandleMovementInputs()
    // {
    //     movementInput.y = Input.GetAxisRaw("Vertical");
    //     movementDirection = transform.forward * movementInput.y;
    //     ToggleSprint();
    // }

    // void HandleMoveAnimation()
    // {
    //     bool isMoving = movementInput.x != 0 || movementInput.y != 0;
    //     bool isRunning = moveSpeedMultiplier == runSpeedMultiplier;

    //     animator.SetBool("isRunning", isRunning && isMoving);
    //     animator.SetBool("isWalking", !isRunning && isMoving);
    // }

    private void Move()
    {
        rb.AddForce(movementDirection * moveSpeed * moveSpeedMultiplier, ForceMode.Force);
    }

    // can be rewritten to fit mobile controls 
    private void ToggleSprint()
    {
        moveSpeedMultiplier = Input.GetKey(KeyCode.LeftShift) ? runSpeedMultiplier : walkSpeedMultiplier;
    }

    public void SetPlatformControls(DeviceManager.PlatformType platform)
    {
        currentPlatform = platform;
    }

    void HandlePcControls()
    {
        movementInput.y = Input.GetAxisRaw("Vertical");
        movementDirection = transform.forward * movementInput.y;
        ToggleSprint();
    }
    
    // there is no backwards movement when playing on mobile compared to playing keyboard/mouse 
    // both come with unique challenges 
    void HandleMobileControls()
    {
        movementInput = leftJoyStick.actions["Move"].ReadValue<Vector2>();
        if(movementInput.magnitude > 0.1f)
        {
            //Calculate movement bsaed on pplayer's current forward direction
            movementDirection = new Vector3(movementInput.x,0,movementInput.y);

            //Ensure movement direction happens in the forward direction 
            movementDirection = transform.forward * movementDirection.magnitude; 
        }
        else
        {
            movementDirection = Vector3.zero;
        }
    }
    void HandleCurrentPlatformControls()
    {
        switch (currentPlatform)
        {
            case DeviceManager.PlatformType.PC:
                // HandlePcControls();
                HandleMobileControls(); // Placed here for testing purposes 
                break;
            case DeviceManager.PlatformType.Mobile:
                HandleMobileControls();
                break;
            default:
                throw new System.Exception("Current device type not found");

        }
    }

}
