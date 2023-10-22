using System.Collections;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    GameObject[] enemies;

    void Awake()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }
    public void HandleAlertEnemyEvent(Vector3 furniturePosition)
    {
        // Debug.Log("Enemy Alerted!");
        // //there will always be at least one enemy
        GameObject closestEnemy = enemies.Length > 0 ? enemies[0] : null;
        Vector3 position = furniturePosition;
        float closestDistance = Mathf.Infinity;

        StartCoroutine(closestEnemy.GetComponent<EnemyController>().InspectFurniture(position));
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
}
