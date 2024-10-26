using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class FragmentObject : MonoBehaviour
{
    
    public GameObject fracture; 
    public float explosionForce;
    public float explosionRadius; 
    void OnCollisionEnter(Collision collision)
    {
        Break();
    }
    void Break()
    {

        GameObject frac = Instantiate(fracture,transform.position,transform.rotation);
        
        foreach(Transform obj in frac.GetComponentInChildren<Transform>())
        {
            obj.localScale = new Vector3(5,5,5);
        }
        foreach(Rigidbody rb in frac.GetComponentsInChildren<Rigidbody>() )
        {   
            // rb.AddExplosionForce(explosionForce,this.transform.position,explosionRadius);
            Vector3 force = (rb.transform.position - transform.position).normalized * explosionForce;
            rb.AddForce(force);
            
        }
        Destroy(gameObject);

    }
}
