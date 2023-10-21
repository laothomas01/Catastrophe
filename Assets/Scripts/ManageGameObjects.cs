//================= OBSELETE =============


// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class ManageGameObjects : MonoBehaviour
// {
//     List<GameObject> furnitures;
//       void Start()
//     {
//         furnitures = FindGameObjectsInLayer(9);
            
//         for(int i = 0; i < furnitures.Count; i++)
//         {
//             furnitures[i].AddComponent<ObjectCollision>();
//         }
//         Debug.Log(furnitures.Count);
   
        
//     }
   
//     void Update()
//     {
    
//     }

   
//     public List<GameObject> FindGameObjectsInLayer(int layer)
//    {

//         GameObject [] gameObjArray = (GameObject[]) FindObjectsOfType(typeof(GameObject));

//         // ArrayList gameObjList = 
//         List<GameObject> gameObjList = new System.Collections.Generic.List<GameObject>();
        
//         for(int i = 0; i < gameObjArray.Length; i++)
//         {
//             if(gameObjArray[i].layer == layer)
//             {
//                 gameObjList.Add(gameObjArray[i]);
//             }
//         }
//         if(gameObjList.Count == 0)
//         {
//             return null;
//         }
//         return gameObjList;
  
//    }

// }


//================= OBSELETE =============
