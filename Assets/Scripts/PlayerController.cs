using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float speedMultiplier = 10f;
    private Vector3 move;
    private Rigidbody rb;
    private Vector3 moveDir;
    public Camera mouse;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //switch between the values of 1,0,-1 based on the input
        move.x = Input.GetAxisRaw("Horizontal");
        move.y = Input.GetAxisRaw("Vertical");
     

        RaycastHit hit;
        Ray ray = mouse.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out hit))
        {
            transform.LookAt(new Vector3(hit.point.x,transform.position.y,hit.point.z));
        }

    }

    private void FixedUpdate()
    {
        //moveDir = new Vector3(move.x, 0f, move.y);
        moveDir = (transform.forward * move.y) + (transform.right * move.x);
        rb.AddForce(moveDir.normalized  * moveSpeed * speedMultiplier, ForceMode.Force);
        //rb.MovePosition(moveDir * moveSpeed);

    }
}