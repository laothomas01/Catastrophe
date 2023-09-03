
using UnityEngine;

public class Keyboard_PlayerMovement : MonoBehaviour
{
    //we should take this stat data and place it into a scriptable object where. 

    //note: keep code scalable and modular
    public float moveSpeed, speedMultiplier;
    private Vector3 moveInput, moveDir, lookDirection, mousePoint;
    public Animator animator;
    public GameObject mouseObj;
    private Rigidbody rb;
    Ray cursor_ray;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        moveDir = new Vector3();
        mousePoint = new Vector3();
        cursor_ray = new Ray();
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {

        handleMovementInputs();
        RaycastHit hit;
        int layerMask = 1 << 7;

        layerMask = ~layerMask;
        cursor_ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(cursor_ray, out hit, Mathf.Infinity, layerMask))
        {

            //find mouse point on X-Z plane
            mousePoint = new Vector3(hit.point.x, transform.position.y, hit.point.z); 
            mouseObj.transform.position = mousePoint;
            lookDirection = mousePoint - transform.position;
            //rotate transform.forward vector in direction of mouse point
            this.transform.LookAt(mousePoint);
        }


        moveDir = transform.forward * moveInput.y;
    }

    private void OnTriggerStay(Collider other)
    {

    }

    private void OnTriggerExit(Collider other)
    {

    }

    private void FixedUpdate()
    {
        rb.AddForce(moveDir.normalized * moveSpeed * speedMultiplier * Time.fixedDeltaTime, ForceMode.Force);
    }

    public Vector3 getMousePoint()
    {
        return mousePoint;
    }
    public Vector3 getMoveDir()
    {
        return moveDir;
    }
    public Vector3 getLookDirection()
    {
        return lookDirection;
    }
    public void setLookDirection(Vector3 lookDir)
    {
        this.lookDirection = lookDir;
    }
    public void handleMovementInputs()
    {
        //switch between the values of 1,0,-1 based on the input
        //move.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        animator.SetBool("isWalking", moveInput.x != 0 || moveInput.y != 0);
        // animator.SetBool("HeavyAttacking", Input.GetMouseButtonDown(0));

        if (Input.GetKey(KeyCode.LeftShift))
        {
            animator.SetBool("isRunning", animator.GetBool("isWalking"));
            speedMultiplier = 300;
        }
        else
        {
            animator.SetBool("isRunning", false);
            speedMultiplier = 200;
        }
    }
}