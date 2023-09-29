
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    public int moveSpeed_;
    public int moveSpeedMultiplier_;

    Vector3 movementInput_ = Vector3.zero;

    Vector3 moveDirection_ = Vector3.zero;

    Vector3 lookDirection_;

    Vector3 mousePoint;

    Rigidbody rigidbody_;

    bool isRunning = false;
    bool isWalking = false;
    void Start()
    {
        rigidbody_ = GetComponent<Rigidbody>();
    }


    void FixedUpdate()
    {

        //after calculating movement direction, apply physics force to player for movement
        //used also for interacting with collisions
        rigidbody_.AddForce(moveDirection_.normalized * moveSpeed_ * moveSpeedMultiplier_ * Time.fixedDeltaTime, ForceMode.Force);

    }

    void Update()
    {
        //calculate player input
        // calculate mouse direction
        Ray cursorRay_;
        RaycastHit mouseToScreenHit;
        //we want to raycast around the player model
        //this creates a boundary of where the player can point their cursor for movement
        int targetLayerMask_ = 1 << 7;
        cursorRay_ = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(cursorRay_, out mouseToScreenHit, Mathf.Infinity, ~targetLayerMask_))
        {
            // Debug.Log("Hit Something!");
            mousePoint = new Vector3(mouseToScreenHit.point.x, transform.position.y, mouseToScreenHit.point.z);
            // Debug.Log(mousePoint);
            lookDirection_ = mousePoint - transform.position;
            transform.LookAt(mousePoint);
        }



        //using mouse direction and player input, calculate movement direction
        moveDirection_ = transform.forward * movementInput_.y;
    }

    public Vector3 getLookDirection()
    {
        return lookDirection_;
    }

    public void OnMovement(InputAction.CallbackContext contxt)
    {

        movementInput_ = contxt.ReadValue<Vector2>();
        switch (contxt.phase)
        {
            case InputActionPhase.Performed:
                isWalking = true;
                GetComponent<Animator>().SetBool("isWalking",isWalking);
                break;
            case InputActionPhase.Canceled:
                isWalking = false;
                GetComponent<Animator>().SetBool("isWalking",isWalking);
                break;
        }
    }

    public void OnToggleSprint(InputAction.CallbackContext contxt)
    {

        int currentSpeedMultiplier_ = moveSpeedMultiplier_;

        switch (contxt.phase)
        {
            case InputActionPhase.Performed:
                isRunning = true;
                Debug.Log("Sprinting!");
                moveSpeedMultiplier_ = currentSpeedMultiplier_ * 2;
                GetComponent<Animator>().SetBool("isRunning", isRunning);
                break;
            case InputActionPhase.Canceled:
                isRunning = false;
                Debug.Log("Not Sprinting!");
                moveSpeedMultiplier_ = currentSpeedMultiplier_ / 2;
                GetComponent<Animator>().SetBool("isRunning",isRunning);

                break;
        }
    }
}