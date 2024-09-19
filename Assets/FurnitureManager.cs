using UnityEngine;

public class FurnitureManager : MonoBehaviour
{
    private GameObject[] heavyFurnitures;
    private int maxHeavyFurnitureCount;
    private int currentHeavyFurnitureCount;
    // Start is called before the first frame update
    void Start()
    {
        heavyFurnitures = GameObject.FindGameObjectsWithTag("Heavy");
        
        // Get the number of heavy furnitures found
        maxHeavyFurnitureCount = heavyFurnitures.Length;
        currentHeavyFurnitureCount = maxHeavyFurnitureCount;
        // currentHeavyFurnitureCount = maxHeavyFurnitureCount;
    }
    void Update()
    {
        
    }
    public void DecrementCurrentHeavyFurnitureCount()
    {
        currentHeavyFurnitureCount--;
    }
    public int GetCurrentHeavyFurnitureCount()
    {
        return currentHeavyFurnitureCount;
    }
    public int GetMaxHeavyFurnitureCount()
    {
        return maxHeavyFurnitureCount;
    }

}
