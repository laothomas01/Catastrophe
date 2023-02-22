using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private PlayerMovement move;
    private Vector3 attackDir;

    [SerializeField]
    private int attackRange;  
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

    //collection of all enemies
    GameObject[] enemies;

    //list of raycast hit objects
    // HashSet<RaycastHit> hits;
    void Start()
    {
        
        // hits = new HashSet<RaycastHit>();        
        destroyTime = 3;
       DEBUGGING = false;
        //look for all game objects with Enemy tag
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        maxAttackTime = 1f;
        isHolding = false;
        attacking = true;
       //initialize to maxAttackTime to allow first attack
        attackTime = maxAttackTime;
        move = GetComponent<PlayerMovement>();
        attackDir = new Vector3();
    }


        void Update() {
            

            Debug.DrawRay(transform.position,attackDir * attackRange,Color.blue);
        
            if(attackTime < maxAttackTime)
            {
                attackTime += Time.deltaTime;
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
        if(Physics.Raycast(transform.position,attackDir,out hit,600,layerMask))
        {


                    
                    //attack in look direction
                    if(attacking && attackTime >= maxAttackTime)
                    {


                    

                         GameObject furniture = hit.transform.gameObject;
                    
                         if(furniture.tag == "Pushable")
                        {
                            furniture.transform.GetComponent<Rigidbody>().AddForce(attackDir * forceAmount * forceMultiplier * Time.fixedDeltaTime ,ForceMode.Impulse);
                             if(DEBUGGING)
                   
                           {
                             Destroy(furniture,destroyTime);
                           }
                                attacking = false;
                                attackTime = 0;
                        }
                        //@TODO: finish
                        else if(furniture.tag == "Throwable")
                        {

                            // if(this.transform.childCount > 2)
                            // {
                            //     for(int i = 1; i < this.transform.childCount; ++i)
                            //     {
                            //         this.transform.GetChild(i).SetParent(null);
                            //     }
                            // }
                            // else
                               
                                // if(isHolding == false)
                                // {
                                //     isHolding = true;
                                //     furniture.transform.SetParent(this.transform);
                                //     // furniture.transform.forward = attackDir;
                                //     this.transform.GetChild(1).GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                                // }
                                // else
                                // {
                                //     this.transform.GetChild(1).GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

                                //       this.transform.GetChild(1).SetParent(null);
                                // isHolding = false;
                                // attacking = false;
                                // }
                             
                          
                                    

                                

                           
                  
                        }

                           
                  
                        //@TODO add more attack mechanics if possible
            
                    }
                  
                   
        }
        
     
    }
    void attackInputs()
    {

        //set attack flag
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
                attacking = true;
        }
        else
        {
            attacking = false;
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
}