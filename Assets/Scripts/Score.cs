using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class Score : MonoBehaviour
{
    private int maxFurnitureCount;
    private int currFurnitureCount = 0;
    
    private TextMeshProUGUI textMesh;
    // ManageGameObjects objManage_Script;
    // public GameObject objManage;
    ManageGameObjects objManage_Script;
    public GameObject winScreen;
    List<GameObject> objects;
    GameObject[] heavy;
    GameObject[] pushable;
    public int layer;
    void Start()
    {
        textMesh = this.GetComponent<TextMeshProUGUI>();
        // objManage_Script = objManage.GetComponent<ManageGameObjects>();
        //objManage_Script = GameObject.Find("GameObjectManager").GetComponent<ManageGameObjects>();
        //objects = GameObject.FindGameObjectsWithTag("Heavy");
        heavy = GameObject.FindGameObjectsWithTag("Heavy");
        pushable = GameObject.FindGameObjectsWithTag("Pushable");
        try{
            maxFurnitureCount = heavy.Length + pushable.Length;
        }
        catch
        {
            Debug.Log("Null exception");
        }
        
        currFurnitureCount = maxFurnitureCount;
        textMesh.SetText(currFurnitureCount.ToString() + "/" + maxFurnitureCount.ToString());
    }

    // Update is called once per frame
    void Update()
    {
            heavy = GameObject.FindGameObjectsWithTag("Heavy");
            pushable = GameObject.FindGameObjectsWithTag("Pushable");
        if (heavy != null && pushable != null)
        {
            currFurnitureCount = (heavy.Length + pushable.Length);
            textMesh.SetText(currFurnitureCount.ToString() + "/" + maxFurnitureCount.ToString());
        }
            
        if (currFurnitureCount == 0)
        {
            winScreen.SetActive(true);
        }

        
        
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
