using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 10f;
    private Rigidbody2D rb;
    private Vector3 mouse_direction;
    private float rotational_radian;
    private Vector3 mousePos;
    int forward;
    int turn;

    float delta;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        forward = 0;
        turn = 0;
        delta = Time.deltaTime;
    }

    // Update is called once per frame
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

    private void FixedUpdate()
    {
        rb.SetRotation(rotational_radian * Mathf.Rad2Deg);
        move_player(forward, turn, delta);
        if (forward > 0 && turn > 0)
        {
            rb.velocity = new Vector3(Mathf.Cos(Mathf.PI / 4) * forward, Mathf.Sin(Mathf.PI / 4) * forward, 0);

        }
        else if (forward > 0 && turn < 0)
        {
            rb.velocity = new Vector3(Mathf.Cos(Mathf.PI / 4) * forward, Mathf.Sin(Mathf.PI / 4) * forward, 0);

        }
        else if (forward > 0 && turn == 0)
        {
            rb.velocity = new Vector3(Mathf.Cos(rotational_radian) * forward, Mathf.Sin(rotational_radian) * forward, 0);

        }
        else if (forward < 0 && turn > 0)
        {
            rb.velocity = new Vector3(Mathf.Cos(Mathf.PI / 4) * forward, Mathf.Sin(Mathf.PI / 4) * forward, 0);

        }
        else
        {
            rb.velocity = new Vector3(forward, forward, 0);
        }
    }
    private Vector3 getLookDirection()
    {
        return mouse_direction;
    }
    private void handleInput()
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
    private void move_player(int forward, int turning, float delta)
    {
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
    private int getForward()
    {
        return forward;
    }
    private int getTurning()
    {
        return getTurning();
    }
    private float getRotationRadians()
    {
        return rotational_radian;
    }
    private float getMoveSpeed()
    {
        return moveSpeed;
    }
    private float getDeltaTime()
    {
        return delta;
    }
}




