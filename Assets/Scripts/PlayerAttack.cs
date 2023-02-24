using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerAttack : MonoBehaviour

{
    private PlayerMovement moveScript;
    // private Camera camera;
    private Vector3 maxAttackDirection;
    private Vector3 minAttackDirection;

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
       
    void Start()
    {
        seenObjs = new HashSet<GameObject>();
        Cursor.visible = false;
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
        maxAttackDirection = new Vector3();
        isAttacking = false;
        layerMask = 1 << 9;
        DEBUG_ATTACK_DIR = true;
        minAttackDirection = new Vector3();
    }
        void Update() {
          
            if(attackCoolDownTimer < maxAttackCoolDown)
            {
                attackCoolDownTimer += Time.deltaTime;
            }


                maxAttackDirection = moveScript.getLookDirection();
                maxAttackDirection = Vector3.ClampMagnitude(maxAttackDirection,attackRange);
               
                Debug.DrawRay(transform.position,maxAttackDirection,Color.red);


             if(isAttacking)
            {
  
            
                attackCoolDownTimer = 0;
            }

            attackInputs();
        }
    void FixedUpdate()
    {
        RaycastHit hit;
         // Bit shift the index of the layer (9) to get a bit mask

         //e.g: 1 << 9 checks layer 9 ("Furniture layer") 
     


        // maxAttackDirection = moveScript.getLookDirection();

//temp variable to compare current and seen hit object
        if(Physics.Raycast(transform.position,maxAttackDirection,out hit,(maxAttackDirection).magnitude,layerMask))
        {

            //store data of hit object into global variable

                hitObj = hit.transform.gameObject;
                
                
                if(!colorChanged)
                {
                    
                    originalColor = hit.transform.GetComponent<Renderer>().material.color;
                    hit.transform.GetComponent<Renderer>().material.color = Color.blue;
                    colorChanged = true;
                }
                
                
               if(isAttacking)
               {
        
                    Debug.Log("HIT!");
                    
                    hit.transform.gameObject.GetComponent<Rigidbody>().AddForce(maxAttackDirection * forceAmount * forceMultiplier * Time.fixedDeltaTime ,ForceMode.Impulse);  

                     Camera.main.GetComponent<Follow_Player>().setCanShake(true);

                    Destroy(hit.transform.gameObject); 
            
            
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
                Debug.Log("Enemy Name " + enemy.name + " curr distance " + currDistance + "closestDistance " + closestDistance );
                closestEnemy = enemy;
                closestDistance = currDistance;
            }
        }
        closestEnemy.GetComponent<EnemyController>().InspectFurniture(furniture.transform);
        Destroy(furniture,destroyTime);
    }
    public void setAttack(bool attack)
    {
        this.isAttacking = attack;
    }


 


 }