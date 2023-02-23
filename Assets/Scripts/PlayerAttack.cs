using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private PlayerMovement move;
    private Camera camera;
    private Vector3 attackDir;

    [SerializeField]
    private float attackRange;  
    [SerializeField]  
    private bool attacking;

    [SerializeField]
    private float forceAmount;
    [SerializeField]
    private float forceMultiplier;

[SerializeField]
    private float attackTime;
    
    [SerializeField]
    private float maxAttackTime;

    [SerializeField]
    bool DEBUG_CAMERA,DEBUG_DESTROY;
    [SerializeField]
    private float destroyTime;
 
    GameObject[] enemies;
 

    [SerializeField]
    private Color originalColor;
    List<Color> originalColors;
    int furnitureLayer;
       GameObject hitObj;
       private bool colorChanged;
    void Start()
    {

            camera = Camera.main;
            
        colorChanged = false;
            originalColor = new Color();
        originalColors = new List<Color>();
        hitObj = new GameObject();

        
        furnitureLayer = 9;
        destroyTime = 3;
   
  

        //look for all game objects with Enemy tag
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
      
        
        maxAttackTime = 0.5f;
        attackTime = maxAttackTime;
        move = GetComponent<PlayerMovement>();
        attackDir = new Vector3();
        attacking = false;
    }


        void Update() {
            

            attackDir = Vector3.ClampMagnitude(attackDir,(attackDir * attackRange).magnitude);
            //@TODO: clamp down a min and max attack range
            Debug.DrawRay(transform.position,attackDir,Color.blue);

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


        attackDir = move.getLookDirection();
        // Debug.Log(hitObj.name);

        if(Physics.Raycast(transform.position,attackDir,out hit,attackDir.magnitude,layerMask))
        {
                hitObj = hit.transform.gameObject;

              
               if(hitObj != null)
               {
                 if(!colorChanged)
                {
                   
                    originalColor = hitObj.GetComponent<Renderer>().material.color;
                    hitObj.GetComponent<Renderer>().material.color = Color.blue;
                }
                
               if(attacking)
               {
        
        
                if(hitObj.gameObject.tag == "Pushable")
                {
                    hitObj.transform.gameObject.GetComponent<Rigidbody>().AddForce(attackDir * forceAmount * forceMultiplier * Time.fixedDeltaTime ,ForceMode.Impulse);
                    
                  
                    if(DEBUG_CAMERA)
                    {
                        camera.GetComponent<Follow_Player>().setCanShake(true);
                    }
                    if(DEBUG_DESTROY)
                    {
                        // AlertEnemy(hit.transform.gameObject,3);
                        Destroy(hit.transform.gameObject,3);
                    }
                }
            
               }
              
       
               
               }

               colorChanged = true;
        }
        else
        {

            if(hitObj != null)
            {
                if(hitObj.layer == LayerMask.NameToLayer("Furniture"))
            {
                colorChanged = false;
                hitObj.GetComponent<Renderer>().material.color = originalColor;
            }
            
            }
        
        }
       
        


   
    }
    //
    private void OnCollisionEnter(Collision other) {
            
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


  public void ColorFurniture(GameObject furniture)
   {
      
    //   int currSeenIndex = 0;
    //   for(int i = 0; i < furnitures.Length; i++)
    //   {
    //     if(furnitures[i] == furniture)
    //     {
    //         currSeenIndex = i;
    //     }
        // else
        // {
        //     furnitures[currSeenIndex].GetComponent<Renderer>().material.color = Color.green; //  all furniture stays as its default color: currently set as green for testing
        // }
      
   }


 }