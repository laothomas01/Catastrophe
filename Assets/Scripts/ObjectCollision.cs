using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCollision : MonoBehaviour
{
    
  void OnCollisionEnter(Collision col) {
    // if(col.gameObject.layer == LayerMask.NameToLayer("Player"))
    // {
    //     return;
    // }
  }
    // void OnTriggerEnter(Collider col) {
    //     if(col.gameObject.layer == LayerMask.NameToLayer("Player"))
    //     {
    //             this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
    //     }       
    // }
    //  void OnTriggerExit(Collider col) {
    //      if(col.gameObject.layer == LayerMask.NameToLayer("Player"))
    //     {
    //             this.gameObject.GetComponent<Rigidbody>().isKinematic =false;
    //             this.gameObject.GetComponent<Rigidbody>().WakeUp();
    //     }    
    // }

    void OnDestroy() {
            // FindObjectOfType<AudioManager>().Play("brokenwood");
    }
     
   
}