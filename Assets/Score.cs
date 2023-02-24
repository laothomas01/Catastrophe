using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Score : MonoBehaviour
{
    private int maxFurnitureCount = 0;
    private int tmpMax = 0;
    private int currFurnitureCount = 0;
    
    public ManageGameObjects gameObjManager;
    private TextMeshProUGUI textMesh;

    void Start()
    {
        textMesh = this.GetComponent<TextMeshProUGUI>();
        gameObjManager = gameObjManager.GetComponent<ManageGameObjects>();
        maxFurnitureCount = gameObjManager.getFurnitureCount();
        currFurnitureCount = maxFurnitureCount;
        textMesh.text = currFurnitureCount.ToString() + "/" + maxFurnitureCount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        currFurnitureCount = gameObjManager.getFurnitureCount();
        // Debug.Log(maxFurnitureCount);
        // tmpMax = maxFurnitureCount;
        // currFurnitureCount = tmpMax;
        textMesh.text = currFurnitureCount.ToString() + "/" + maxFurnitureCount.ToString();
        
    }
    public void setCurrCount(int c)
    {
        this.currFurnitureCount = c;
    }
    
    public int getCurrCount()
    {
        return this.currFurnitureCount;
    }
    public int getMaxCount()
    {
        return this.maxFurnitureCount;
    }
    public void setMaxFurnitureCount(int i)
    {
        this.maxFurnitureCount = i;
    }
}
