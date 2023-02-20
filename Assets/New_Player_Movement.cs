using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class New_Player_Movement : MonoBehaviour
{

    [SerializeField]
    protected float moveSpeed = 10f;
    protected Vector3 mouse_direction;
    protected Rigidbody2D rb;
    protected float rotational_radian;
    //protected Vector3 mousePos;

    //send ray in z direction from underneath cursor position
    Ray mousePos;
    //checking if raycast hit
    RaycastHit hit;
    void Start()
    {
        mousePos = new Ray();
        hit = new RaycastHit();
    }

    void Update()
    {

        
        mousePos = Camera.main.ScreenPointToRay(Input.mousePosition);

        //confirm ray cast hit
        if (Physics.Raycast(mousePos, 
            
            //cursor = point hitting the screen????
            out hit))
        {
            transform.LookAt(new Vector3(hit.point.x, transform.position.y, hit.point.z));
        }

    }

    private void FixedUpdate()
    {
        //mouse_direction = mousePos - transform.position;
        //mouse_direction = Vector3.ClampMagnitude(mo)
    }
}
