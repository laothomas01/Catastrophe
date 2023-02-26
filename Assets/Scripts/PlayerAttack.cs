using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerAttack : MonoBehaviour

{
    private PlayerMovement moveScript;
    private Animator animator;

    // private Camera camera;
    private Vector3 maxAttackDirection;
    public Transform lookPoint;

    [SerializeField]
    private float attackRange;  
    [SerializeField]  
    private bool isAttacking;

    [SerializeField]
    private float forceAmount;
    [SerializeField]
    private float forceMultiplier;

[SerializeField]
    private float attackCoolDownTimer;
    
    [SerializeField]
    private float maxAttackCoolDown;

    [SerializeField]
    bool DEBUG_CAMERA,DEBUG_DESTROY;
    [SerializeField]
    private float destroyTime;
 
    GameObject[] enemies;
    public bool DEBUG_ATTACK_DIR;
    [SerializeField]
    private Color originalColor;
    int furnitureLayer;
    
    int layerMask;
       GameObject hitObj;

   
       private bool colorChanged;

    HashSet<GameObject> seenObjs;

[SerializeField]
    bool isHolding;
    HashSet<GameObject> destroyedObjs;
    void Start()
    {
        destroyedObjs = new HashSet<GameObject>();
        isHolding = false;
        seenObjs = new HashSet<GameObject>();
        colorChanged = false;
        originalColor = new Color();
        hitObj = new GameObject();
        furnitureLayer = 9;
        destroyTime = 3;
        //look for all game objects with Enemy tag
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        maxAttackCoolDown = 0;
        attackCoolDownTimer = maxAttackCoolDown;
        moveScript = GetComponent<PlayerMovement>();
        animator = gameObject.GetComponent<Animator>();
        maxAttackDirection = new Vector3();
        isAttacking = false;
        layerMask = 1 << 9;
        DEBUG_ATTACK_DIR = true;
    }
        void Update() {
          
            if(attackCoolDownTimer < maxAttackCoolDown)
            {
                attackCoolDownTimer += Time.deltaTime;
            }


                //animator.SetBool("HeavyAttacking", isAttacking);
                maxAttackDirection = moveScript.getLookDirection();
                maxAttackDirection = Vector3.ClampMagnitude(maxAttackDirection,attackRange);

             if(isAttacking)
            {
  
            
                attackCoolDownTimer = 0;
            }

            Debug.DrawRay(lookPoint.position,maxAttackDirection,Color.red);


            attackInputs();
        }
    void FixedUpdate()
    {
        RaycastHit hit;
         // Bit shift the index of the layer (9) to get a bit mask

         //e.g: 1 << 9 checks layer 9 ("Furniture layer") 
    
//temp variable to compare current and seen hit object
        if(Physics.Raycast(lookPoint.position,lookPoint.forward,out hit,(maxAttackDirection).magnitude,layerMask))
        {

            //store data of hit object into global variable

                hitObj = hit.transform.gameObject;
                
                
                if(!colorChanged)
                {
                    
                    originalColor = hit.transform.GetComponent<Renderer>().material.color;
                    if(hit.transform.gameObject.tag == "Pushable")
                    {
                    if (Input.GetMouseButtonDown(0))
                    {
                        AlertEnemy(hit.transform.gameObject,0);
                        //Destroy(hit.transform.gameObject);
                    }
                        hit.transform.GetComponent<Renderer>().material.color = Color.blue;
                    }
                    else if(hit.transform.gameObject.tag == "Throwable")
                    {
                    if (Input.GetMouseButtonDown(0))
                    {
                        AlertEnemy(hit.transform.gameObject, 0);
                        //Destroy(hit.transform.gameObject);
                    }
                    hit.transform.GetComponent<Renderer>().material.color = Color.yellow;
                    }
                    else if (hit.transform.gameObject.tag == "Heavy")
                    {
                    if (Input.GetMouseButtonDown(0))
                    {
                        AlertEnemy(hit.transform.gameObject, 0);
                        //Destroy(hit.transform.gameObject);
                    }
                    hit.transform.GetComponent<Renderer>().material.color = Color.red;
                    }
                colorChanged = true;
                }
                



//performing attack                
               if(isAttacking)
               {
                Debug.Log("Attacking" + hit.transform.gameObject.name);
                Debug.Log(hit.transform.gameObject.tag);
                   //pushing
                    if(hit.transform.gameObject.tag == "Pushable")
                    {
                        Debug.Log("Hit" + hit.transform.gameObject);
                    hit.transform.gameObject.GetComponent<Rigidbody>().AddForce(maxAttackDirection * forceAmount * forceMultiplier * Time.fixedDeltaTime ,ForceMode.Impulse); 
                    // FindObjectOfType<AudioManager>().Play("punch");
                     Camera.main.GetComponent<Follow_Player>().setCanShake(true);

                    //  FindObjectOfType<AudioManager>().Play("brokenwood");

                    //Destroy(hit.transform.gameObject);
                    AlertEnemy(hit.transform.gameObject, 2);
                   
                    }
                    //throwing
                    else if(hit.transform.gameObject.tag == "Throwable")
                    {
                        if(isHolding)
                        {
                                hit.transform.gameObject.GetComponent<Rigidbody>().AddForce(maxAttackDirection * forceAmount * forceMultiplier * Time.fixedDeltaTime ,ForceMode.Impulse);  
                                 FindObjectOfType<AudioManager>().Play("throw");
                                
                                


                        // FindObjectOfType<AudioManager>().Play("brokenwood");

                        //Destroy(hit.transform.gameObject);
                        AlertEnemy(hit.transform.gameObject, 2);
                        setHolding(false);
                        }
                    }
                    else if(hit.transform.gameObject.tag == "Heavy")
                {
                    AlertEnemy(hit.transform.gameObject, 0);
                    Camera.main.GetComponent<Follow_Player>().setCanShake(true);
                }

               }

            if(isHolding)
            {
                if(hit.transform.gameObject.tag == "Throwable")
                 {
                        hit.transform.position = this.transform.position + maxAttackDirection;
                        //  FindObjectOfType<AudioManager>().Play("pickup");
                }
                else
                {
                    setHolding(false);
                    // FindObjectOfType<AudioManager>().Play("drop");

                }

            }
         
              
        }
        else
        {
          
          if(hitObj != null)

          {
            if(hitObj.layer == LayerMask.NameToLayer("Furniture"))
          {
            hitObj.GetComponent<Renderer>().material.color = originalColor;
            colorChanged = false;
            
          }
          }

            
        }
    
    }
   
    void attackInputs()
    {

        //set attack flag
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
                   
                if(attackCoolDownTimer >= maxAttackCoolDown)
                {
                    setAttack(true);
                }
        }
        else
        {
               setAttack(false);

        }
        if(Input.GetKeyDown(KeyCode.Mouse1))
        {
           isHolding = !isHolding;
        }
        

    }


//calls enemy 
 void AlertEnemy(GameObject furniture,float destroyTime)
    {
        //there is always going to be atleast one enemy around
        GameObject closestEnemy = enemies[0];
        float closestDistance = Mathf.Infinity;
        foreach (GameObject enemy in enemies)
        {
            float currDistance = Vector3.Distance(enemy.transform.position, furniture.transform.position);
            if ( currDistance <= closestDistance)
            {
                closestEnemy = enemy;
                closestDistance = currDistance;
            }
        }
        closestEnemy.GetComponent<EnemyController>().InspectFurniture(furniture.transform);
        Destroy(furniture);
    }
    public void setAttack(bool attack)
    {
        this.isAttacking = attack;
    }    public void setHolding(bool hold)
    {
        this.isHolding = hold;
    }


 


 }