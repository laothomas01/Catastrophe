using UnityEngine;
using TMPro;
public class Score : MonoBehaviour
{
    private TextMeshProUGUI textMesh;
    // public FurnitureManager furnitureManager;
    // public GameObject  furnitureManager;
    FurnitureManager furnitureManager;

    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        furnitureManager = FindObjectOfType<FurnitureManager>();
    }
    void Update()
    {
        textMesh.SetText(furnitureManager.GetCurrentHeavyFurnitureCount().ToString() + "/" + furnitureManager.GetMaxHeavyFurnitureCount().ToString());
    }

}
