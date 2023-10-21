//================= OBSELETE =============


// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;


// /// <summary>
// /// OBSELETE CODE
// /// </summary>
// public class DestroyFurniture : MonoBehaviour
// {
//     // Ray ray;
//     // Camera main_camera;
//     // RaycastHit hit;
//     // GameObject[] enemies;

//     // void Start()
//     // {
//     //     main_camera = Camera.main;
//     //     ray = new Ray();
//     //     enemies = GameObject.FindGameObjectsWithTag("Enemy");
//     // }
//     // void Update()
//     // {
//     //     ray = main_camera.ScreenPointToRay(Input.mousePosition);
//     //     if(Input.GetMouseButtonDown(0))
//     //     {
//     //         if (Physics.Raycast(ray, out hit) && hit.transform.gameObject.layer == LayerMask.NameToLayer("Furniture"))
//     //         {
//     //             // AlertEnemy(hit.transform.gameObject);
//     //             Debug.Log(hit.transform.gameObject);
//     //         }

//     //     }
//     // }


    
//     // void AlertEnemy(GameObject furniture)
//     // {
//     //     // there is always going to be at least one enemy around
//     //     GameObject closestEnemy = enemies[0];
//     //     float closestDistance = Mathf.Infinity;

//     //     foreach (GameObject enemy in enemies)
//     //     {
//     //         float currDistance = Vector3.Distance(enemy.transform.position, furniture.transform.position);
//     //         // Debug.Log(enemy + "DISTANCE:" + currDistance);

//     //         // if ( currDistance <= closestDistance)
//     //         // {
//     //         //     Debug.Log("Enemy Name " + enemy.name + " curr distance " + currDistance + "closestDistance " + closestDistance );
//     //         //     closestEnemy = enemy;
//     //         //     closestDistance = currDistance;
//     //         // }
//     //     }

//     //     // closestEnemy.GetComponent<EnemyController>().InspectFurniture(furniture.transform);
//     //     // Destroy(furniture);
//     // }
// }
