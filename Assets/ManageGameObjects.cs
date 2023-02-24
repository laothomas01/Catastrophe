using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageGameObjects : MonoBehaviour
{
    // Start is called before the first frame update
    List<GameObject> furnitures;
    public int layer;    
    void Start()
    {
        furnitures = new List<GameObject>();
        furnitures = FindGameObjectsInLayer(layer);
    }
    void Update()
    {
        //update list of furniture objects
         furnitures = FindGameObjectsInLayer(layer);
    }
    private List<GameObject> FindGameObjectsInLayer(int layer)
   {

        GameObject [] gameObjArray = (GameObject[]) FindObjectsOfType(typeof(GameObject));

        // ArrayList gameObjList = 
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
        return gameObjList;
        // // return gameObjList.ToArray();
   }
   public int getFurnitureCount()
   {
    return furnitures.Count;
   }
}
