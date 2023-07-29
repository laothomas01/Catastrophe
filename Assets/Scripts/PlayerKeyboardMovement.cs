
using UnityEngine;

public class PlayerKeyboardMovement : MonoBehaviour
{
    public float moveSpeed, speedMultiplier;
    private Vector3 move, moveDir, lookDirection, mousePoint;
    public Animator animator;
    public GameObject mouseObj;
    private Rigidbody rb;
    Ray cursor_ray;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        // lookDirection = new Vector3();
        moveDir = new Vector3();
        mousePoint = new Vector3();
        // hit = new RaycastHit();
        cursor_ray = new Ray();
        rb = GetComponent<Rigidbody>();
        //DEBUG_LOOK_DIR = false;

    }

    // Update is called once per frame
    void Update()
    {

        movementInputs();
        //testing
        //   if(DEBUG_LOOK_DIR)
        //   {
        //       Debug.DrawRay(transform.position,lookDirection,Color.blue);
        //   }



    }

    private void OnTriggerStay(Collider other)
    {

    }

    private void OnTriggerExit(Collider other)
    {

    }

    private void FixedUpdate()
    {

        // RaycastHit hit;
        // // Bit shift the index of the layer (7) to get a bit mask
        // int layerMask = 1 << 7;

        // // This would cast rays only against colliders in layer 7.
        // // But instead we want to collide against everything except layer 7. The ~ operator does this, it inverts a bitmask.
        // layerMask = ~layerMask;
        // cursor_ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // //only 
        // if (Physics.Raycast(cursor_ray, out hit, Mathf.Infinity, layerMask))
        // {

        //     //find mouse point on X-Z plane
        //     mousePoint = new Vector3(hit.point.x, transform.position.y, hit.point.z);
        //     mouseObj.transform.position = mousePoint;
        //     lookDirection = mousePoint - transform.position;
        //     //rotate transform.forward vector in direction of mouse point
        //     this.transform.LookAt(mousePoint);
        // }


        moveDir =
            //moving in look direction via Verticle button
            transform.forward * move.y;
        //moving sideways via Horizontal button
        //+(transform.right * move.x);
        //move in look direction
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
    public void movementInputs()
    {
        //switch between the values of 1,0,-1 based on the input
        //move.x = Input.GetAxisRaw("Horizontal");
        move.y = Input.GetAxisRaw("Vertical");

        animator.SetBool("isWalking", move.x != 0 || move.y != 0);
        animator.SetBool("HeavyAttacking", Input.GetMouseButtonDown(0));

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