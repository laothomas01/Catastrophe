using UnityEngine;

/// <summary>
/// Contains functionality for handling an array of enemy game objects
/// </summary>
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
        GameObject closestEnemy = FindClosestEnemy();
        if(closestEnemy != null)
        {
             Vector3 position = furniturePosition;
             Debug.Log("Enemy Alerted!");
             closestEnemy.GetComponent<EnemyController>().InspectFurniture(position);
        }
    }

    private GameObject FindClosestEnemy()
    {
        float closestDistance = Mathf.Infinity;
        GameObject closestEnemy = null;
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


}
