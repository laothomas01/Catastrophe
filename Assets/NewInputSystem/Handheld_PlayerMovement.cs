using UnityEngine;
using UnityEngine.InputSystem;
public class Handheld_PlayerMovement : MonoBehaviour
{
    //new Unity Input System object
    PlayerInput playerInput;
    Rigidbody rigidbody;
    Animator animator;
    Vector3 movementInput;
    Vector3 movementDir;
    Vector3 lookDirection;
    public float moveSpeed;
    public int moveSpeedMultiplier;

    //temporary solution
    bool isSprinting = false;
    void Awake()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
        movementInput = new Vector3();
        movementDir = new Vector3();
        lookDirection = new Vector3();

    }
    void Start()
    {

    }

    void Update()
    {

        //joystick inputs
        movementInput = playerInput.actions["Walk"].ReadValue<Vector2>();

        //direction player should face
        lookDirection = new Vector3(movementInput.x, 0f, movementInput.y).normalized;

        //if joystick direction is downward, 
        if (movementInput.y < 0)
        {
            movementInput.y = 1; 
        }

        if (lookDirection != Vector3.zero)
        {
            transform.forward = lookDirection; // we want forward to be in a "positive" direction despite joystick up or down. we do not want to look backwards
        }


        if (playerInput.actions["ToggleSprint"].triggered)
        {
            isSprinting = !isSprinting;
            Debug.Log("isSprinting:" + isSprinting);
        }

        //perform walk animation if movement input not 0
        // animator.SetBool("isWalking", movementInput.x != 0 || movementInput.y != 0);

        if (isSprinting)
        {
            // animator.SetBool("isRunning",animator.GetBool("isWalking"));
            moveSpeedMultiplier = 300;
        }
        else
        {
            // animator.SetBool("isRunning",false);
            moveSpeedMultiplier = 200;
        }

        movementDir = transform.forward * movementInput.y;
    }

    void FixedUpdate()
    {

        rigidbody.AddForce(movementDir.normalized * moveSpeed * moveSpeedMultiplier * Time.fixedDeltaTime, ForceMode.Force);
    }
}
