using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 10f;
    private Vector2 move;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //switch between the values of 1,0,-1 based on the input
        move.x = Input.GetAxisRaw("Horizontal");
        move.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        //Based on the current value of the move, we multiply that into the movespeed and add that to the current position
        rb.MovePosition(rb.position + (move * moveSpeed * Time.fixedDeltaTime));
    }
}
