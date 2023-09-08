using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class ScoreManager : MonoBehaviour
{
    private int maxFurnitureCount;
    private int currFurnitureCount = 0;

    private TextMeshProUGUI textMesh;

    // // ManageGameObjects objManage_Script;
    // // public GameObject objManage;
    // // ManageGameObjects objManage_Script;
    // public GameObject winScreen;
    // List<GameObject> objects;
    int currHeavyFurnitureCount;
    GameObject[] heavy;

    bool gameWin = false;
    // GameObject[] pushable;
    // public int layer;
    void Start()
    {
        textMesh = this.GetComponent<TextMeshProUGUI>();
        // furnitureManager = FindObjectOfType<FurnitureManager>();
        // maxFurnitureCount = furnitureManager.getDestroyableFurnitures().Count;
        //     furnitureManager = GameObject.Find("FurnitureManager").GetComponent<FurnitureManager>();
        //     // objManage_Script = objManage.GetComponent<ManageGameObjects>();
        //     //objManage_Script = GameObject.Find("GameObjectManager").GetComponent<ManageGameObjects>();
        //     //objects = GameObject.FindGameObjectsWithTag("Heavy");
        heavy = GameObject.FindGameObjectsWithTag("Heavy");
        //     //pushable = GameObject.FindGameObjectsWithTag("Pushable");
        //     try{
        maxFurnitureCount = heavy.Length;
        //     }
        //     catch
        //     {
        //         Debug.Log("Null exception");
        //     }

        // textMesh.SetText(currFurnitureCount.ToString() + "/" + maxFurnitureCount.ToString());
    }

    // // Update is called once per frame
    void Update()
    {
        heavy = GameObject.FindGameObjectsWithTag("Heavy");
        //         //pushable = GameObject.FindGameObjectsWithTag("Pushable");
        //     //if (heavy != null && pushable != null)
        //     if (heavy != null)
        //     {
        //         //currFurnitureCount = (heavy.Length + pushable.Length);
        currHeavyFurnitureCount = heavy.Length;

        textMesh.SetText(currHeavyFurnitureCount.ToString() + "/" + maxFurnitureCount);

        if(currHeavyFurnitureCount == 0)
        {
            gameWin = true;
        }

        // Debug.Log(currFurnitureCount);
        //         textMesh.SetText(currFurnitureCount.ToString() + "/" + maxFurnitureCount.ToString());
        //     }

        //     if (currFurnitureCount == 0)
        //     {
        //         winScreen.SetActive(true);
        //         winScreen.GetComponent<GameOver>().toggleGameOverScreen();
        //     }
    }

    public bool isWin()
    {
        return gameWin;
    }
    // public void setCurrCount(int c)
    // {
    //     this.currFurnitureCount = c;
    // }

    // public int getCurrCount()
    // {
    //     return this.currFurnitureCount;
    // }
    // public int getMaxCount()
    // {
    //     return this.maxFurnitureCount;
    // }
    // public void setMaxFurnitureCount(int i)
    // {
    //     this.maxFurnitureCount = i;
    // }
}
