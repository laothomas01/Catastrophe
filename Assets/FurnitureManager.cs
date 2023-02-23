using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureManager : MonoBehaviour
{
        [SerializeField]
        private int furnitureLayer;
       private GameObject[] furnitures;
    void Start()

    {
        
         furnitures = FindGameObjectsInLayer(furnitureLayer);
         Debug.Log(furnitures.Length);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

      private GameObject [] FindGameObjectsInLayer(int layer)
   {
        GameObject [] gameObjArray = (GameObject[]) FindObjectsOfType(typeof(GameObject));
        List<GameObject> gameObjList = new System.Collections.Generic.List<GameObject>();
        
        for(int i = 0; i < gameObjArray.Length; i++)
        {
            if(gameObjArray[i].layer == layer)
            {
                gameObjList.Add(gameObjArray[i]);
            }
        }
        if(gameObjList.Count == 0)
        {
            return null;
        }
        return gameObjList.ToArray();
   }
}
