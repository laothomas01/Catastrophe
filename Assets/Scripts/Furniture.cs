using UnityEngine;


/// <summary>
/// Base class for Furniture objects
/// - can be extended for more specific functionalities
/// - can be an abstract class 
/// </summary>
public class Furniture : MonoBehaviour

{
    FurnitureManager furnitures;
    

    
    void Start()
    {
        furnitures = FindFirstObjectByType<FurnitureManager>();
        
    }
    bool isDestroyed = false;
    void OnDestroy()
    {
        if (isDestroyed)
        {
            if(this.gameObject.tag == "Heavy")
            {
                MainCamera camera = FindAnyObjectByType<MainCamera>();
                camera.CanShake(true);
                furnitures.DecrementCurrentHeavyFurnitureCount();
            }
        }

    }
    public void IsDestroyed(bool destroyed)
    {
        isDestroyed = destroyed;
    }
}
