using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    static GameObject[] enemies;
    
    void Awake()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }
    public static void handleAlertEnemyEvent(GameObject furniture)
    {
        Debug.Log("Enemy Alerted!");
        //there will always be at least one enemy
        GameObject closestEnemy = enemies.Length > 0 ? enemies[0] : null;
        float closestDistance = Mathf.Infinity;

        foreach(GameObject enemy in enemies)
        {
            if(enemy != null)
            {
                float currDistance = Vector3.Distance(enemy.transform.position,furniture.transform.position);
                if(currDistance <= closestDistance)
                {
                    closestEnemy = enemy;
                    closestDistance = currDistance;
                }
                if(closestEnemy != null)
                {
                    closestEnemy.GetComponent<EnemyController>().InspectFurniture(furniture.transform);
                    
                }
            }
        }
    }
}
