// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// /*
 
// Note: Do not clean anything up yet
 
 
//  */
// public class PlayerMovement : MonoBehaviour
// {
//     [SerializeField]
//     protected float moveSpeed = 10f;
//     protected Rigidbody2D rb;
//     protected Vector3 mouse_direction;
//     protected float rotational_radian;
//     protected Vector3 mousePos;
//     int forward;
//     int turn;

//     float delta;

//     void Start()
//     {
//         mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//         mouse_direction = new Vector3();
//         rb = GetComponent<Rigidbody2D>();
//         forward = 0;
//         turn = 0;
//         delta = Time.deltaTime;
//     }

//     void Update()
//     {



//         mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//         mouse_direction = mousePos - transform.position;
//         mouse_direction = Vector3.ClampMagnitude(mouse_direction, 5);
//         rotational_radian = Mathf.Atan2(mouse_direction.y, mouse_direction.x);

//         if (rotational_radian > Mathf.PI * 2)
//         {
//             rotational_radian -= Mathf.PI * 2;
//         }
//         else if (rotational_radian < 0)
//         {
//             rotational_radian += Mathf.PI * 2;
//         }

//         handleInput();
//     }
//     void OnDrawGizmosSelected()
//     {

//         //// Draws a blue line from this transform to the target
//         //Gizmos.color = Color.blue;
//         //Gizmos.DrawLine(transform.position,);

//     }

//     protected void FixedUpdate()
//     {/*
      
//         [X] forward/backwards movement
//         [] sideways movement
      
//       */
//         rb.SetRotation(rotational_radian * Mathf.Rad2Deg);
//         move_player(forward, 0, delta);

//         //if (turn > 0)
//         //{
//         //    rb.velocity = new Vector3(Mathf.Cos(rotational_radian) + Mathf.PI / 2 * forward, 1, 0);

//         //}
//         //else if (forward > 0 && turn < 0)
//         //{
//         //    rb.velocity = new Vector3(Mathf.Cos(Mathf.PI / 4) * forward, Mathf.Sin(Mathf.PI / 4) * forward, 0);

//         //}
//         //else if (forward > 0 && turn == 0)
//         //{
//         //    rb.velocity = new Vector3(Mathf.Cos(rotational_radian) * forward, Mathf.Sin(rotational_radian) * forward, 0);

//         //}
//         //else if (forward < 0 && turn > 0)
//         //{
//         //    rb.velocity = new Vector3(Mathf.Cos(Mathf.PI / 4) * forward, Mathf.Sin(Mathf.PI / 4) * forward, 0);

//         //}

//     }
//     protected Vector3 getLookDirection()
//     {
//         return mouse_direction;
//     }
//     protected void handleInput()
//     {
//         bool up, left, right, down, up_release, down_release, right_release, left_release;
//         up = Input.GetKey(KeyCode.W);
//         down = Input.GetKey(KeyCode.S);
//         left = Input.GetKey(KeyCode.A);
//         right = Input.GetKey(KeyCode.D);


//         up_release = Input.GetKeyUp(KeyCode.W);
//         down_release = Input.GetKeyUp(KeyCode.S);
//         left_release = Input.GetKeyUp(KeyCode.A);
//         right_release = Input.GetKeyUp(KeyCode.D);


//         if (up && right)
//         {


//             //Debug.Log("FORWARD RIGHT");
//             forward = 1;
//             turn = 1;
//         }
//         else if (up && left)
//         {
//             //Debug.Log("FORWARD LEFT");
//             forward = 1;
//             turn = -1;
//         }
//         else if (down && right)
//         {
//             //Debug.Log("BACKWARD RIGHT");
//             forward = -1;
//             turn = 1;
//         }
//         else if (down && left)
//         {
//             //Debug.Log("BACKWARD LEFT");
//             forward = -1;
//             turn = -1;
//         }
//         else if (up && !down)
//         {
//             forward = 1;
//             //Debug.Log("UP");
//         }
//         else if (down && !up)
//         {
//             forward = -1;
//             //Debug.Log("DOWN");
//         }
//         else if (left && !right)
//         {
//             turn = -1;
//             //Debug.Log("LEFT");

//         }
//         else if (right && !left)
//         {
//             turn = 1;
//             //Debug.Log("RIGHT");
//         }
//         else
//         {
//             forward = 0;
//             turn = 0;
//             //Debug.Log("NONE");
//         }
//         if (right_release)
//         {
//             turn = 0;
//         }


//     }
//     protected void move_player(int forward, int turning, float delta)
//     {

//         if (forward > 0)
//         {
//             rb.velocity = new Vector3(Mathf.Cos(rotational_radian) * forward * moveSpeed * delta, Mathf.Sin(rotational_radian) * forward * moveSpeed * delta, 0);
//         }
//         else if (forward < 0)
//         {
//             rb.velocity = new Vector3(Mathf.Cos(rotational_radian) * forward * moveSpeed * delta, Mathf.Sin(rotational_radian) * forward * moveSpeed * delta, 0);
//         }
//         else
//         {
//             rb.velocity = new Vector3(forward, forward, 0);
//         }
//         //Debug.Log("FORWARD:" + forward + "TURNING:" + turning);
//         //if (forward > 0)
//         //{
//         //    //move forward

//         //}
//         //else
//         //{

//         //}
//         //if (forward > 0)
//         //{
//         //    rb.velocity = new Vector3(Mathf.Cos(getRotationRadians() * getMoveSpeed() * delta), Mathf.Sin(getRotationRadians() * getMoveSpeed() * delta), 0);
//         //}
//         //else
//         //{
//         //    rb.velocity = new Vector3(Mathf.Cos(getRotationRadians() * -getMoveSpeed() * delta), Mathf.Sin(getRotationRadians() * getMoveSpeed() * delta), 0);
//         //}

//     }

//     private void OnDrawGizmos()
//     {
//         //Gizmos.color = Color.blue;
//         //Gizmos.DrawLine(mousePos, transform.position);

//     }
//     protected int getForward()
//     {
//         return forward;
//     }
//     protected int getTurning()
//     {
//         return getTurning();
//     }
//     public float getRotationRadians()
//     {
//         return rotational_radian;
//     }
//     public float getMoveSpeed()
//     {
//         return moveSpeed;
//     }
//     public float getDeltaTime()
//     {
//         return delta;
//     }
//     public Vector3 getMouseDirection()
//     {
//         return mouse_direction;
//     }
//     public Vector3 getMousePosition()
//     {
//         return mousePos;
//     }
// }







using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float speedMultiplier = 10f;
    private Vector3 move;
    private Rigidbody rb;
    private Vector3 moveDir;

    //where camera sees mouse
    private Camera mouse;
    RaycastHit hit;
    Ray cursor_ray;
    RaycastHit look_hit;

    Vector3 test;




    private float player_y_position;
    // Start is called before the first frame update
    void Start()
    {
        test = new Vector3();
        moveDir = new Vector3();
        hit = new RaycastHit();
        look_hit = new RaycastHit();
        cursor_ray = new Ray();
        rb = GetComponent<Rigidbody>();
        mouse = Camera.main;
        player_y_position = transform.position.y;

    }

    // Update is called once per frame
    void Update()
    {
        //switch between the values of 1,0,-1 based on the input
        move.x = Input.GetAxisRaw("Horizontal");
        move.y = Input.GetAxisRaw("Vertical");

        //mouse position
        cursor_ray = mouse.ScreenPointToRay(Input.mousePosition);


        if (Physics.Raycast(cursor_ray, out hit))
        {

            //make sure raycast hit position does not find objectg with layer "Player"
            if (hit.transform.gameObject.layer != LayerMask.NameToLayer("Player"))
            {
                //transforms player to look at cursor point

                /*
                
                 - Working with X_Z Plane instead of X_Y Plane
                 - just set y position to player's y position. no changes to y position
                 */

                // test = new Vector3(hit.point.x, player_y_position, hit.point.z);
                //player's forward vector points at raycast's hit position
                this.transform.LookAt(new Vector3(hit.point.x, player_y_position, hit.point.z));

            }

        }
        // look_ray.direction = moveDir;
        
    }

private void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position,this.transform.forward);
}
    private void FixedUpdate()
    {

  

        moveDir = (transform.forward * move.y) + (transform.right * move.x);
        rb.AddForce(moveDir.normalized * moveSpeed * speedMultiplier, ForceMode.Force);
    }
    
    private Vector3 getMoveDir()
    {
        return moveDir;
    }
}