using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private PlayerMovement move;
    private Vector3 attackDir;

    [SerializeField]
    private float attackRange;  
    [SerializeField]  
    private bool attacking;
    [SerializeField]
    private bool isHolding;
    [SerializeField]
    private float forceAmount;
    [SerializeField]
    private float forceMultiplier;

[SerializeField]
    private float attackTime;
    
    [SerializeField]
    private float maxAttackTime;

    [SerializeField]
    bool DEBUGGING;
    [SerializeField]
    private float destroyTime;
    [SerializeField]
    private float maxAttackRange;
    [SerializeField]
    private float minAttackRange;

    //collection of all enemies
    GameObject[] enemies;

    [SerializeField]
    private float offsetValue;

    private bool canThrow = false;
    //list of raycast hit objects
    // HashSet<RaycastHit> hits;
    void Start()
    {
        
        // hits = new HashSet<RaycastHit>();   
           
        destroyTime = 3;
        DEBUGGING = false;
        //look for all game objects with Enemy tag
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        maxAttackTime = 0.75f;
        isHolding = false;
        
       //initialize to maxAttackTime to allow first attack
        attackTime = maxAttackTime;
        move = GetComponent<PlayerMovement>();
        attackDir = new Vector3();
        attacking = false;
    }


        void Update() {
            

            attackDir = attackDir * attackRange;
            Debug.DrawRay(transform.position,attackDir);

        if(attackTime < maxAttackTime)
            {
                attackTime += Time.deltaTime;
            }

            if(attacking)
            {
                attackTime = 0;
            }


  
            attackInputs();

  

        }
    void FixedUpdate()
    {
        RaycastHit hit;
         // Bit shift the index of the layer (9) to get a bit mask

         //e.g: 1 << 9 checks layer 9 ("Furniture layer") 
        int layerMask = 1 << 9;

        // This would cast rays only against colliders in layer 8.
        //We want to collide againast layer 8

        attackDir = move.getLookDirection();
        // attackDir = Vector3.ClampMagnitude(attack)
        //@TODO 200 = testing attack range


//find better way to set,reset color
        GameObject hitObj;
        if(Physics.Raycast(transform.position,attackDir,out hit,attackDir.magnitude,layerMask))
        {
                //--------------Perform Phyics------------------



                hitObj = hit.transform.gameObject;

                Color customColor = new Color(0.4f, 0.9f, 0.7f, 1.0f);
                hitObj.GetComponent<Renderer>().material.SetColor("_Color",customColor); // need a way to change hit object's color back
                    
                   
               if(attacking)
               {
                 if(hitObj.gameObject.tag == "Pushable")
                {
                    hitObj.transform.gameObject.GetComponent<Rigidbody>().AddForce(attackDir * forceAmount * forceMultiplier * Time.fixedDeltaTime ,ForceMode.Impulse);
                }
                // else if(hitObj.gameObject.tag == "Throwable")
                // {
                //    if(!isHolding)
                //    {
                //       Debug.Log("Pick UP");
                //      isHolding = true;
                //     holdingObj = hitObj;
                //    }
                // }
               }
          

                   
        }
       
       
        //  hitObj.GetComponent<Renderer>().material.color = new Color(1,1,1,1); // need a way to change hit object's color back
        
        //[x] hold object
        //[] restrict held object to player's position, offset by some value and clamping down the attack direction. 
        //[] add throw force
        // if(isHolding)
        // {

                        
        //                 holdingObj.transform.position = this.transform.position + attackDir * 0.5f * offsetValue;
                    
        // }
     
    }
    void attackInputs()
    {

        //set attack flag
        if(Input.GetKeyDown(KeyCode.Mouse0) && attackTime >= maxAttackTime)
        {
                   
                    setAttack(true);
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
        this.attacking = attack;
    }
}