using System.Collections;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    GameObject[] enemies;
    GameObject player;
    void Awake()
    {   
        player = GameObject.Find("Cat");
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }
    public void HandleAlertEnemyEvent(Vector3 furniturePosition)
    {
        // Debug.Log("Enemy Alerted!");
        // //there will always be at least one enemy
        // GameObject closestEnemy = null;
        // float closestDistance = Mathf.Infinity;
        // foreach(GameObject enemy in enemies)
        // {
        //     float currDistance = Vector3.Distance(enemy.transform.position,transform.position);

        //     // if(currDistance < closestDistance)
        //     // {
        //     //     closestEnemy = enemy;
        //     //     closestDistance = currDistance;
        //     // }
        //     Debug.Log(enemy.name + "\n" + currDistance);

        // }

        GameObject closestEnemy = FindClosestEnemy();
        // Debug.Log(closestEnemy.name);
        // Debug.Log("ENEMY:" + closestEnemy.name + "CLOSEST DISTANCE:" + closestDistance);
        Vector3 position = furniturePosition;
        StartCoroutine(closestEnemy.GetComponent<EnemyController>().InspectFurniture(position));

        /*
        
        here is demo of my logic:

        [     Enemy 1, Enemy 2, Enemy 3  ]
                ^                   ^
                i                   j 
                - leet code problem 101
                    - find enemy with the closest distance
                        - store that max distance
                        - keep iterating until i and j pass each other
                        - current enemy with the closest distance will be the enemy to be towards the player 
        
            OR.....

        we can sort of "binary search" this from the center?

        [ ENemy 1, Enemy 2 , ENemy 3].... [Enemy1, Enemy2,ENemy3,ENemy4,ENemy5]
                                                            ^


        */



        // for(int i = 0,j = enemies.Length - 1; i < enemies.Length; i++,j--)
        // {
        //         if(j == 0)
        //         {
        //             j = enemies.Length - 1;
        //         }
        //         Debug.Log()
        // }

        // foreach(GameObject enemy in enemies)
        // {

        // }
        // foreach(GameObject enemy in enemies)
        // {

        // }
        // foreach(GameObject enemy in enemies)
        // {
        //     enemy.GetComponent<EnemyController>().InspectFurniture(position);
        //     break;
        // }
        // foreach (GameObject enemy in enemies)
        // {
        //     if (enemy != null)
        //     {
        //         float currDistance = Vector3.Distance(enemy.transform.position, furniture.transform.position);
        //         if (currDistance <= closestDistance)
        //         {
        //             closestEnemy = enemy;
        //             closestDistance = currDistance;
        //             break;
        //         }

        //     }
        // }
        // if (closestEnemy != null)
        // {
        //     closestEnemy.GetComponent<EnemyController>().InspectFurniture(furniture.transform);
        // }

    }

    private GameObject FindClosestEnemy()
    {
        float closestDistance = Mathf.Infinity;
        GameObject closestEnemy = null;
        // foreach(GameObject enemy in enemies)
        // {
        //     float distance = Vector3.Distance(enemy.transform.position,transform.position);

        //     if(distance < closestDistance)
        //     {
        //         closestDistance = distance;
        //         closestEnemy = enemy;
        //     }
        // }
        for (int i = 0; i < enemies.Length; i++)
        {
            //player's position. not "enemy manager positon you retard!"
            float distance = Vector3.Distance(enemies[i].transform.position, player.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemies[i];
            }
        }
        // Debug.Log("Closest Distance: " + closestDistance + " " + closestEnemy.name);
        return closestEnemy;
    }

    /*
    
    implementing strategy pattern:
    - based on the given size of the array, we can implement and use different algorithms to solve our problem: which enemy is the closest

    
    */


}
