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
    private bool canAttack;
    [SerializeField]
    private float forceAmount;
    [SerializeField]
    private float forceMultiplier;

[SerializeField]
    private float attackTime;
    [SerializeField]
    private float maxAttackTime;


    //collection of all enemies
    GameObject[] enemies;
    void Start()
    {
       
       //initialize to maxAttackTime to allow first attack
        
        maxAttackTime = 1f;
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
        if(Physics.Raycast(transform.position,attackDir,out hit,200,layerMask))
        {
            
                    //attack in look direction
                    if(canAttack && attackTime >= maxAttackTime)
                    {

                        Debug.Log("ATTACKING");

                        GameObject hit_obj = hit.transform.gameObject;
                    
                         if(hit_obj.tag == "Pushable")
                        {
                            hit.transform.GetComponent<Rigidbody>().AddForce(attackDir * forceAmount * forceMultiplier * Time.fixedDeltaTime ,ForceMode.Impulse);
                            // Destroy(hit_obj,2);
                        }
                        attackTime = 0;
            
                    }
                   
        }
    }
    void attackInputs()
    {

        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
                canAttack = true;
        }
        else
        {
                canAttack = false;
        }
    
        
        
    }


//calls enemy 
 void AlertEnemy(GameObject furniture)
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
        //time out destroy
        Destroy(furniture);
    }
}