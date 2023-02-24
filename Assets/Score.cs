using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class Score : MonoBehaviour
{
    private int maxFurnitureCount;
    private int tmpMax = 0;
    private int currFurnitureCount = 0;
    
    private TextMeshProUGUI textMesh;
    // ManageGameObjects objManage_Script;
    // public GameObject objManage;
    ManageGameObjects objManage_Script;
    List<GameObject> objects;
    public int layer;
    void Start()
    {
        textMesh = this.GetComponent<TextMeshProUGUI>();
        // objManage_Script = objManage.GetComponent<ManageGameObjects>();
        objManage_Script = GameObject.Find("GameObjectManager").GetComponent<ManageGameObjects>();
        objects = objManage_Script.FindGameObjectsInLayer(layer);
        if(objects != null)
        {
            maxFurnitureCount = objects.Count;
        }
        currFurnitureCount = maxFurnitureCount;
        textMesh.text = currFurnitureCount.ToString() + "/" + maxFurnitureCount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
            objects = objManage_Script.FindGameObjectsInLayer(layer);
            if(objects != null)
            {
            currFurnitureCount = objects.Count;
           
            }
            else
            {
            currFurnitureCount = 0;
             SceneManager.LoadScene("Win_Screen");
            }
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
