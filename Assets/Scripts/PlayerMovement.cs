
using UnityEngine;
public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed;
    private float moveSpeedMultiplier;
    private Vector3 movementInput;
    private Vector3 movementDirection;
    private Rigidbody rigidbody;
    public int runSpeedMultiplier;
    public int walkSpeedMultiplier;
    Animator animator;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
    void Update()
    {
        animator = GetComponent<Animator>();
        HandleMovementInputs();
        HandleMoveAnimation();
    }

    void FixedUpdate()
    {
        Move();
    }
    void HandleMovementInputs()
    {
        movementInput.y = Input.GetAxisRaw("Vertical");
        movementDirection = transform.forward * movementInput.y;
        toggleSprint();
    }
    void HandleMoveAnimation()
    {
        if (movementInput.x != 0 || movementInput.y != 0)
        {
            if (moveSpeedMultiplier == walkSpeedMultiplier)
            {
                animator.SetBool("isRunning", false);
                animator.SetBool("isWalking", true);
            }
            else
            {
                animator.SetBool("isRunning", true);
                animator.SetBool("isWalking", false);
            }

        }
        else
        {
            animator.SetBool("isRunning", false);
            animator.SetBool("isWalking", false);
        }

    }
    void Move()
    {
        rigidbody.AddForce(movementDirection.normalized * moveSpeed * moveSpeedMultiplier * Time.fixedDeltaTime, ForceMode.Force);
    }
    void toggleSprint()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeedMultiplier = runSpeedMultiplier;
        }
        else
        {
            moveSpeedMultiplier = walkSpeedMultiplier;
        }
    }
}