using UnityEngine;


/// <summary>
/// Base class for Furniture objects
/// - can be extended for more specific functionalities
/// - can be an abstract class 
/// </summary>
public class Furniture : MonoBehaviour

{
    EnemyManager enemyManager;
    FurnitureManager furnitures;
    MainCamera camera;
    void Start()
    {
        furnitures = FindFirstObjectByType<FurnitureManager>();
        enemyManager = FindAnyObjectByType<EnemyManager>();
        camera = FindAnyObjectByType<MainCamera>();
    }
    bool isDestroyed = false;
    void OnDestroy()
    {
        if (isDestroyed)
        {
            furnitures.DecrementCurrentHeavyFurnitureCount();
        }

    }
    public void IsDestroyed(bool destroyed)
    {
        isDestroyed = destroyed;
    }
}
