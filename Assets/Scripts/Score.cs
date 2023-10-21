using UnityEngine;
using TMPro;
public class Score : MonoBehaviour
{
    private TextMeshProUGUI textMesh;
    FurnitureManager furnitureManager;
    int currentHeavyFurnitureCount;
    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        furnitureManager = FindFirstObjectByType<FurnitureManager>();
        currentHeavyFurnitureCount = furnitureManager.GetMaxHeavyFurnitureCount();
        textMesh.SetText(currentHeavyFurnitureCount.ToString() + "/" + furnitureManager.GetMaxHeavyFurnitureCount().ToString());
    }
    void Update()
    {
        textMesh.SetText(currentHeavyFurnitureCount.ToString() + "/" + furnitureManager.GetMaxHeavyFurnitureCount().ToString());
    }
    public void DecrementCurrentHeavyFurnitureCount()
    {
        --currentHeavyFurnitureCount;
    }

}
