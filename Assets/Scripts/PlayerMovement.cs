using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 
Note: Do not clean anything up yet
 
 
 */
public class PlayerMovement : MonoBehaviour
{
   protected float moveSpeed = 10f;
    protected Rigidbody2D rb;
    protected Vector3 mouse_direction;
    protected float rotational_radian;
    protected Vector3 mousePos;
    int forward;
    int turn;

    float delta;

    void Start()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        rb = GetComponent<Rigidbody2D>();
        forward = 0;
        turn = 0;
        delta = Time.deltaTime;
    }

    void Update()
    {



        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse_direction = mousePos - transform.position;

        rotational_radian = Mathf.Atan2(mouse_direction.y, mouse_direction.x);

        if (rotational_radian > Mathf.PI * 2)
        {
            rotational_radian -= Mathf.PI * 2;
        }
        else if (rotational_radian < 0)
        {
            rotational_radian += Mathf.PI * 2;
        }

        handleInput();
    }
    void OnDrawGizmosSelected()
    {

        //// Draws a blue line from this transform to the target
        //Gizmos.color = Color.blue;
        //Gizmos.DrawLine(transform.position,);

    }

    protected void FixedUpdate()
    {/*
      
        [X] forward/backwards movement
        [] sideways movement
      
      */
        rb.SetRotation(rotational_radian * Mathf.Rad2Deg);
        move_player(forward, 0, delta);

        //if (turn > 0)
        //{
        //    rb.velocity = new Vector3(Mathf.Cos(rotational_radian) + Mathf.PI / 2 * forward, 1, 0);

        //}
        //else if (forward > 0 && turn < 0)
        //{
        //    rb.velocity = new Vector3(Mathf.Cos(Mathf.PI / 4) * forward, Mathf.Sin(Mathf.PI / 4) * forward, 0);

        //}
        //else if (forward > 0 && turn == 0)
        //{
        //    rb.velocity = new Vector3(Mathf.Cos(rotational_radian) * forward, Mathf.Sin(rotational_radian) * forward, 0);

        //}
        //else if (forward < 0 && turn > 0)
        //{
        //    rb.velocity = new Vector3(Mathf.Cos(Mathf.PI / 4) * forward, Mathf.Sin(Mathf.PI / 4) * forward, 0);

        //}

    }
    protected Vector3 getLookDirection()
    {
        return mouse_direction;
    }
    protected void handleInput()
    {
        bool up, left, right, down, up_release, down_release, right_release, left_release;
        up = Input.GetKey(KeyCode.W);
        down = Input.GetKey(KeyCode.S);
        left = Input.GetKey(KeyCode.A);
        right = Input.GetKey(KeyCode.D);


        up_release = Input.GetKeyUp(KeyCode.W);
        down_release = Input.GetKeyUp(KeyCode.S);
        left_release = Input.GetKeyUp(KeyCode.A);
        right_release = Input.GetKeyUp(KeyCode.D);


        if (up && right)
        {


            //Debug.Log("FORWARD RIGHT");
            forward = 1;
            turn = 1;
        }
        else if (up && left)
        {
            //Debug.Log("FORWARD LEFT");
            forward = 1;
            turn = -1;
        }
        else if (down && right)
        {
            //Debug.Log("BACKWARD RIGHT");
            forward = -1;
            turn = 1;
        }
        else if (down && left)
        {
            //Debug.Log("BACKWARD LEFT");
            forward = -1;
            turn = -1;
        }
        else if (up && !down)
        {
            forward = 1;
            //Debug.Log("UP");
        }
        else if (down && !up)
        {
            forward = -1;
            //Debug.Log("DOWN");
        }
        else if (left && !right)
        {
            turn = -1;
            //Debug.Log("LEFT");

        }
        else if (right && !left)
        {
            turn = 1;
            //Debug.Log("RIGHT");
        }
        else
        {
            forward = 0;
            turn = 0;
            //Debug.Log("NONE");
        }
        if (right_release)
        {
            turn = 0;
        }


    }
    protected void move_player(int forward, int turning, float delta)
    {

        if (forward > 0)
        {
            rb.velocity = new Vector3(Mathf.Cos(rotational_radian) * forward * moveSpeed * delta, Mathf.Sin(rotational_radian) * forward * moveSpeed * delta, 0);
        }
        else if (forward < 0)
        {
            rb.velocity = new Vector3(Mathf.Cos(rotational_radian) * forward * moveSpeed * delta, Mathf.Sin(rotational_radian) * forward * moveSpeed * delta, 0);
        }
        else
        {
            rb.velocity = new Vector3(forward, forward, 0);
        }
        //Debug.Log("FORWARD:" + forward + "TURNING:" + turning);
        //if (forward > 0)
        //{
        //    //move forward

        //}
        //else
        //{

        //}
        //if (forward > 0)
        //{
        //    rb.velocity = new Vector3(Mathf.Cos(getRotationRadians() * getMoveSpeed() * delta), Mathf.Sin(getRotationRadians() * getMoveSpeed() * delta), 0);
        //}
        //else
        //{
        //    rb.velocity = new Vector3(Mathf.Cos(getRotationRadians() * -getMoveSpeed() * delta), Mathf.Sin(getRotationRadians() * getMoveSpeed() * delta), 0);
        //}

    }
    protected int getForward()
    {
        return forward;
    }
    protected int getTurning()
    {
        return getTurning();
    }
    public float getRotationRadians()
    {
        return rotational_radian;
    }
    public float getMoveSpeed()
    {
        return moveSpeed;
    }
    public float getDeltaTime()
    {
        return delta;
    }
    public Vector3 getMouseDirection()
    {
        return mouse_direction;
    }
    public Vector3 getMousePosition()
    {
        return mousePos;
    }
}




