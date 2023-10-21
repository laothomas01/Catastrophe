//================= OBSELETE =============


// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class PlayerController : MonoBehaviour
// {
//     public float moveSpeed = 10f;
//     public float speedMultiplier = 10f;
//     private Vector3 move;
//     private Rigidbody rb;
//     private Vector3 moveDir;

//     //where camera sees mouse
//     private Camera mouse;
//     RaycastHit hit;
//     Ray cursor_ray;

//     private float player_y_position;
//     // Start is called before the first frame update
//     void Start()
//     {
//         moveDir = new Vector3();
//         hit = new RaycastHit();
//         cursor_ray = new Ray();
//         rb = GetComponent<Rigidbody>();
//         mouse = Camera.main;
//         player_y_position = transform.position.y;

//     }

//     // Update is called once per frame
//     void Update()
//     {
//         //switch between the values of 1,0,-1 based on the input
//         move.x = Input.GetAxisRaw("Horizontal");
//         move.y = Input.GetAxisRaw("Vertical");

//         //mouse position
//         cursor_ray = mouse.ScreenPointToRay(Input.mousePosition);


//         if (Physics.Raycast(cursor_ray, out hit))
//         {

//             //make sure cursor doesnt touch player
//             if (hit.transform.gameObject.layer != LayerMask.NameToLayer("Player"))
//             {
//                 //transforms player to look at cursor point

//                 /*
                
//                  - Working with X_Z Plane instead of X_Y Plane
//                  - just set y position to player's y position. no changes to y position
//                  */


//                 //set facing direction to mouse cursor
//                 this.transform.LookAt(new Vector3(hit.point.x, player_y_position, hit.point.z));

//             }

//         }
//     }

//     private void FixedUpdate()
//     {

  

//         moveDir = (transform.forward * move.y) + (transform.right * move.x);
//         rb.AddForce(moveDir.normalized * moveSpeed * speedMultiplier, ForceMode.Force);
//     }
// }