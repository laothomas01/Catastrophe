// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;
using TMPro;
// using UnityEngine.SceneManagement;
public class Score : MonoBehaviour
{
    TextMeshProUGUI textMeshProUGUI;
    int currentHeavyFurnitureCount;
    int maxHeavyFurnitureCount;
    GameObject[] heavyFurnitures;
    void Start()
    {
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        heavyFurnitures = GameObject.FindGameObjectsWithTag("Heavy");
        maxHeavyFurnitureCount = heavyFurnitures.Length;
        currentHeavyFurnitureCount = maxHeavyFurnitureCount;
        
    }
    void Update()
    {
          textMeshProUGUI.SetText(currentHeavyFurnitureCount.ToString() + "/" + maxHeavyFurnitureCount.ToString());
    }

    public void decrementCurrentHeavyFurnitureCount()
    {
        currentHeavyFurnitureCount -= 1;
    }
    public int getCurrentHeavyFurnitureCount()
    {
        return currentHeavyFurnitureCount;
    }
    

}
//     private int maxFurnitureCount;
//     private int currFurnitureCount = 0;
    
//     private TextMeshProUGUI textMesh;
//     // ManageGameObjects objManage_Script;
//     // public GameObject objManage;
//     ManageGameObjects objManage_Script;
//     public GameObject winScreen;
//     List<GameObject> objects;
//     GameObject[] heavy;
//     GameObject[] pushable;
//     public int layer;
//     void Start()
//     {
//         textMesh = this.GetComponent<TextMeshProUGUI>();
//         // objManage_Script = objManage.GetComponent<ManageGameObjects>();
//         //objManage_Script = Ga==meObject.Find("GameObjectManager").GetComponent<ManageGameObjects>();
//         //objects = GameObject.FindGameObjectsWithTag("Heavy");
//         heavy = GameObject.FindGameObjectsWithTag("Heavy");
//         //pushable = GameObject.FindGameObjectsWithTag("Pushable");
//         try{
//             maxFurnitureCount = heavy.Length;
//         }
//         catch
//         {
//             Debug.Log("Null exception");
//         }
        
//         currFurnitureCount = maxFurnitureCount;
//         textMesh.SetText(currFurnitureCount.ToString() + "/" + maxFurnitureCount.ToString());
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         heavy = GameObject.FindGameObjectsWithTag("Heavy");
//             //pushable = GameObject.FindGameObjectsWithTag("Pushable");
//         //if (heavy != null && pushable != null)
//         if (heavy != null)
//         {
//             //currFurnitureCount = (heavy.Length + pushable.Length);
//             currFurnitureCount = (heavy.Length);
//             textMesh.SetText(currFurnitureCount.ToString() + "/" + maxFurnitureCount.ToString());
//         }
            
//         if (currFurnitureCount == 0)
//         {
//             winScreen.SetActive(true);
//             winScreen.GetComponent<GameOver>().toggleGameOverScreen();
//             Camera.main.GetComponent<Follow_Player>().setCanShake(false);
//         }

        
        
//     }
//     public void setCurrCount(int c)
//     {
//         this.currFurnitureCount = c;
//     }
    
//     public int getCurrCount()
//     {
//         return this.currFurnitureCount;
//     }
//     public int getMaxCount()
//     {
//         return this.maxFurnitureCount;
//     }
//     public void setMaxFurnitureCount(int i)
//     {
//         this.maxFurnitureCount = i;
//     }
// }
