using UnityEngine;


/// <summary>
/// Base class for Furniture objects
/// - can be extended for more specific functionalities
/// - can be an abstract class 
/// </summary>
public class Furniture : MonoBehaviour

{
    EnemyManager enemyManager;
    Score score;

    MainCamera camera;
    void Start()
    {
        score = FindFirstObjectByType<Score>();
        enemyManager = FindAnyObjectByType<EnemyManager>();
        camera = FindAnyObjectByType<MainCamera>();
    }
    bool isDestroyed = false;
    void OnDestroy()
    {

        if (isDestroyed)
        {
            score.DecrementCurrentHeavyFurnitureCount();
            camera.CanShake(true);
            enemyManager.GetComponent<EnemyManager>().HandleAlertEnemyEvent(gameObject.transform.position);
        }
    }
    public void IsDestroyed(bool destroyed)
    {
        isDestroyed = destroyed;
    }
}
