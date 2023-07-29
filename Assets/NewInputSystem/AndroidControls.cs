using UnityEngine;
using UnityEngine.InputSystem;

public class AndroidControls : MonoBehaviour
{
    PlayerInput playerInput;
    Rigidbody rigidbody;
    Animator animator;
    Vector3 movementInput;
    Vector3 movementDir;
    Vector3 lookDirection;
    public float moveSpeed;
    public float moveSpeedMultiplier;

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
        movementInput = playerInput.actions["Movement"].ReadValue<Vector2>();
        //@TODO: toggle sprint on/off with button 
        Debug.Log(movementInput);
        lookDirection = new Vector3(movementInput.x, 0f, movementInput.y).normalized;
        if (lookDirection != Vector3.zero)
        {
            transform.forward = lookDirection;
        }
        if (movementInput.y < 0)
        {
            //i dont have proper terminology for this right now
            //basically: we want to move in direction of joystick angle. 
            // this is different from moving in a "negative"/backwards direction
            //direction is always "forward" aka positive but the angle differs
            movementInput.y *= -1;
        }
    }

    void FixedUpdate()
    {

        //Calculate movement direction from joystick input
        movementDir = transform.forward * movementInput.y;
        rigidbody.AddForce(movementDir.normalized * moveSpeed * moveSpeedMultiplier * Time.fixedDeltaTime, ForceMode.Force);

    }
}
