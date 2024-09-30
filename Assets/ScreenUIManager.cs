using UnityEngine;

public class ScreenUIManager : MonoBehaviour
{
    TimeManager timeManager;
    FurnitureManager furnitureManager;
    void Start()
    {
        timeManager = GetComponentInChildren<TimeManager>();
        furnitureManager = FindAnyObjectByType<FurnitureManager>(); // i dont like this way of coding but it will do for now 
    }

    // Update is called once per frame
    void Update()
    {
        if(furnitureManager.GetCurrentHeavyFurnitureCount() <= 0)
        {
            if(timeManager.RunningTimer())
            {
                timeManager.StopTimer();
            }
        }
    }
}
