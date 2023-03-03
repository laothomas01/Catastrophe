using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerAttack : MonoBehaviour

{
    private Camera cam;
    public Transform lookPoint;
    public float hitDistance;
    

    [SerializeField]
    private float forceAmount;
    [SerializeField]
    private float forceMultiplier;

    Rigidbody rb;

    GameObject[] enemies;
    
    private Color originalColor;

    int layerMask;
    GameObject hitObj;
    private bool colorChanged;
    AudioManager audioManager;


    void Start()
    {
        rb = null;
        colorChanged = false;
        originalColor = new Color();
        hitObj = new GameObject();
        cam = Camera.main;
        
        //look for all game objects with Enemy tag
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        layerMask = 1 << 7; //ignore layer

        audioManager = FindObjectOfType<AudioManager>();
    }
    void Update() {

    //isAttacking = ;
    if (Input.GetMouseButtonDown(0))
    {
        Attack();
    }
     ChangeColor();

    Debug.DrawRay(lookPoint.position,lookPoint.forward*hitDistance,Color.red);

    }

//calls enemy 
 void AlertEnemy(GameObject furniture,float destroyTime)
    {
        GameObject closestEnemy = enemies.Length > 0 ? enemies[0]: null;
        float closestDistance = Mathf.Infinity;
        Transform furn = furniture.transform;
        if(destroyTime > 0)
        {
            audioManager.Play("brokenwood");
        }
        else
        {
            Destroy(furniture);
            audioManager.Play("brokenwood");
        }

        foreach (GameObject enemy in enemies)
        {
            float currDistance = Vector3.Distance(enemy.transform.position, furniture.transform.position);
            if (currDistance <= closestDistance)
            {
                closestEnemy = enemy;
                closestDistance = currDistance;
            }
        }
        if(closestEnemy != null)
        {
            closestEnemy.GetComponent<EnemyController>().InspectFurniture(furn.transform);
        }

    }


    void ChangeColor()
    {
        RaycastHit hit;

        if (Physics.Raycast(lookPoint.position, lookPoint.forward, out hit, hitDistance, ~layerMask))
        {
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Furniture"))
            {
                hitObj = hit.transform.gameObject;
                Renderer renderer = hit.transform.GetComponent<Renderer>();

                if (!colorChanged)
                {
                    originalColor = renderer.material.color;

                    if (hitObj.tag == "Heavy")
                    {
                        renderer.material.color = Color.red;
                    }

                    else if (hitObj.tag == "Pushable")
                    {
                        renderer.material.color = Color.blue;
                    }
                    colorChanged = true;
                }
            }
        }
        else
        {
            if (hitObj != null)
            {
                if (hitObj.layer == LayerMask.NameToLayer("Furniture"))
                {
                    hitObj.GetComponent<Renderer>().material.color = originalColor;
                    colorChanged = false;

                }
            }


        }
    }

    void Attack()
    {
        RaycastHit hit;
        if (Physics.Raycast(lookPoint.position, lookPoint.forward, out hit, hitDistance, ~layerMask))
        {
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Furniture"))
            {


                hitObj = hit.transform.gameObject;
                if (hitObj.tag == "Pushable")
                {
                    rb = hitObj.GetComponent<Rigidbody>() == null ? hitObj.AddComponent<Rigidbody>() : hitObj.GetComponent<Rigidbody>();
                    rb.constraints = RigidbodyConstraints.FreezePositionY;
                    rb.AddForce(lookPoint.forward * forceAmount * forceMultiplier, ForceMode.Impulse);
                    cam.GetComponent<Follow_Player>().setCanShake(true);
                    AlertEnemy(hit.transform.gameObject, 1);
                    hitObj.AddComponent<ObjectCollision>();

                }
                else if (hitObj.tag == "Heavy")
                {
                    AlertEnemy(hit.transform.gameObject, 0);
                    cam.GetComponent<Follow_Player>().setCanShake(true);
                }
            }
        }
    }


 }