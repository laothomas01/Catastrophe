using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 10f;
    private Vector2 move;
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

        //if (forward == 1)
        //{
        //    rb.velocity = new Vector3(Mathf.Cos(rotational_radian), Mathf.Sin(rotational_radian), 0);
        //}
        //else
        //{
        //    rb.velocity = Vector3.zero;
        //}
        //move_player(forward, turn, delta);
        //rb.velocity = new Vector3(Mathf.Cos(rot_radian), Mathf.Sin(rot_radian), 0);


        ////Based on the current value of the move, we multiply that into the movespeed and add that to the current position
        //rb.MovePosition(rb.position + (move * moveSpeed * Time.fixedDeltaTime));
        //if(forward == 1)
        //{
        //rb.MovePosition(rb.position + (move * rb.velocity * moveSpeed) * Time.fixedDeltaTime);
        //}
    }
    private Vector3 getLookDirection()
    {
        return mouse_direction;
    }
    private void handleInput()
    {
        bool up, left, right, down;
        up = Input.GetKey(KeyCode.W);
        down = Input.GetKey(KeyCode.S);
        left = Input.GetKey(KeyCode.A);
        right = Input.GetKey(KeyCode.D);




        //if (up)
        //{
        //    Debug.Log("UP");
        //    forward = 1;
        //}
        //else


        if (up && right)
        {
            Debug.Log("FORWARD RIGHT");
            forward = 1;
            turn = 1;
        }
        else if (up && left)
        {
            Debug.Log("FORWARD LEFT");
            forward = 1;
            turn = 1;
        }
        else if (down && right)
        {
            Debug.Log("BACKWARD RIGHT");
            forward = -1;
            turn = 1;
        }
        else if (down && left)
        {
            Debug.Log("BACKWARD LEFT");
            forward = -1;
            turn = 1;
        }
        else if (up && !down)
        {
            Debug.Log("UP");
        }
        else if (down && !up)
        {
            Debug.Log("DOWN");
        }
        else if (left && !right)
        {
            Debug.Log("LEFT");

        }
        else if (right && !left)
        {
            Debug.Log("RIGHT");
        }
        else
        {
            Debug.Log("NONE");
        }
        //else if (down)
        //{
        //    Debug.Log("DOWN");
        //    forward = -1;
        //}


        //else if (up && !down && right && !left)
        //{
        //    Debug.Log("FOWARD RIGHT");
        //    forward = 1;
        //    turn = 1;
        //}
        //else if (down && !up && left && !right)
        //{
        //    Debug.Log("BACK LEFT");
        //    forward = -1;
        //    turn = -1;
        //}
        //else if (down && !up && right && !left)
        //{

        //    Debug.Log("BACK RIGHT");
        //    forward = -1;
        //    turn = 1;
        //}
        //else
        //{
        //    Debug.Log("NONE");
        //    forward = 0;
        //    turn = 0;
        //}


    }
    private void move_player(int forward, int turning, float delta)
    {
        Debug.Log("FORWARD:" + forward + "TURNING:" + turning);
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




