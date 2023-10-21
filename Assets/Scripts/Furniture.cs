using UnityEngine;

public class Furniture : MonoBehaviour

{
    EnemyManager enemyManager;
    Score score;
    void Start()
    {
        score = FindFirstObjectByType<Score>();
        enemyManager = FindAnyObjectByType<EnemyManager>();
    }
    bool isDestroyed = false;
    void OnDestroy()
    {

        if (isDestroyed)
        {
            score.DecrementCurrentHeavyFurnitureCount();
            enemyManager.GetComponent<EnemyManager>().handleAlertEnemyEvent(gameObject);
            
        }
    }
    public void IsDestroyed(bool destroyed)
    {
        isDestroyed = destroyed;
    }
}
