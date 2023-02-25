using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCollision : MonoBehaviour
{
    
  void OnCollisionEnter(Collision col) {
    if(col.gameObject.layer == LayerMask.NameToLayer("Furniture") || col.gameObject.layer == LayerMask.NameToLayer("Walls"))
    {
        FindObjectOfType<AudioManager>().Play("brokenwood");
            Destroy(this.gameObject);
    }
 
  }


     
   
}