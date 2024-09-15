using UnityEngine;
using TMPro;
public class Score : MonoBehaviour
{
    private TextMeshProUGUI textMesh;
    // public FurnitureManager furnitureManager;
    public GameObject  furnitureManager;
    FurnitureManager furnitureManagerScript;
    int currentHeavyFurnitureCount;
    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        furnitureManagerScript = furnitureManager.GetComponent<FurnitureManager>();
        textMesh.SetText(furnitureManagerScript.GetCurrentHeavyFurnitureCount().ToString() + "/" + furnitureManagerScript.GetMaxHeavyFurnitureCount().ToString());

    }
    void Update()
    {
        textMesh.SetText(furnitureManagerScript.GetCurrentHeavyFurnitureCount().ToString() + "/" + furnitureManagerScript.GetMaxHeavyFurnitureCount().ToString());
    }
    // public void DecrementCurrentHeavyFurnitureCount()
    // {
    //     --currentHeavyFurnitureCount;
    // }

}
