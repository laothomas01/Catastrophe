using UnityEngine;
public class Keyboard_PlayerAttack : MonoBehaviour
{
    // Animator animator; // 

    // private Camera cam; //we will handle this in another script maybe
    public Transform lineOfSightStartPoint; // location where line of sight begins 
    public float lineOfSightDistance; //how far the player can see
    
    [SerializeField]
    private float forceAmount;  // how hard to push an object
    [SerializeField]
    private float forceMultiplier; 

    Rigidbody rigidBody;

    // GameObject[] enemies; // put this into a script for destroyable furniture
    
    private Color originalDetectedFurnitureColor;
    int furnitureLayerMask;
    GameObject detectedFurniture;
    private bool furnitureColorChanged;

    //@TODO: move 
    // AudioManager audioManager;

    void Start()
    {
          rigidBody = null;
        furnitureColorChanged = false;
        originalDetectedFurnitureColor = new Color();
        detectedFurniture = null; 
        // cam = Camera.main;
        
        //look for all game objects with Enemy tag
        // enemies = GameObject.FindGameObjectsWithTag("Enemy");
        furnitureLayerMask = 1 << 7; //ignore layer

        // audioManager = FindObjectOfType<AudioManager>();
    }
    void Update() {

    //isAttacking = ;
    if (Input.GetMouseButtonDown(0))
    {
        Attack();
    }
    HandleLineOfSight();

    Debug.DrawRay(lineOfSightStartPoint.position,lineOfSightStartPoint.forward*lineOfSightDistance,Color.red);

    }

    //calls enemy 
    //  void AlertEnemy(GameObject furniture,float destroyTime)
    //     {
    //         GameObject closestEnemy = enemies.Length > 0 ? enemies[0]: null;
    //         float closestDistance = Mathf.Infinity;
    //         Transform furn = furniture.transform;
    //         if(destroyTime > 0)
    //         {
    //             audioManager.Play("brokenwood");
    //         }
    //         else
    //         {
    //             Destroy(furniture);
    //             audioManager.Play("brokenwood");
    //         }

    //         foreach (GameObject enemy in enemies)
    //         {
    //             float currDistance = Vector3.Distance(enemy.transform.position, furniture.transform.position);
    //             if (currDistance <= closestDistance)
    //             {
    //                 closestEnemy = enemy;
    //                 closestDistance = currDistance;
    //             }
    //         }
    //         if(closestEnemy != null)
    //         {
    //             closestEnemy.GetComponent<EnemyController>().InspectFurniture(furn.transform);
    //         }

    //     }

    /// <summary>
    /// Method: DetectFurniture
    /// Approach: raycast from lookpoint position, if detected object has a furniture layer 
    /// and its color has not been changed change its color
    /// </summary>
    void HandleLineOfSight()
    {
        RaycastHit hit;
        if (Physics.Raycast(lineOfSightStartPoint.position, lineOfSightStartPoint.forward, out hit, lineOfSightDistance, ~furnitureLayerMask))
        {
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Furniture"))
            {
                detectedFurniture = hit.transform.gameObject;
                Renderer furnitureRenderer = hit.transform.GetComponent<Renderer>();
                if (!furnitureColorChanged)
                {
                    originalDetectedFurnitureColor = furnitureRenderer.material.color;
                    if (detectedFurniture.tag == "Heavy")
                    {
                        furnitureRenderer.material.color = Color.red;
                    }
                    else if (detectedFurniture.tag == "Pushable")
                    {
                        furnitureRenderer.material.color = Color.blue;
                    }
                    furnitureColorChanged = true;
                }
            }
        }
        else
        {
            if (detectedFurniture != null)
            {
                if (detectedFurniture.layer == LayerMask.NameToLayer("Furniture"))
                {
                    detectedFurniture.GetComponent<Renderer>().material.color = originalDetectedFurnitureColor;
                    furnitureColorChanged = false;
                }
            }


        }
    }

    void Attack()
    {
        RaycastHit hit;
        if (Physics.Raycast(lineOfSightStartPoint.position, lineOfSightStartPoint.forward, out hit, lineOfSightDistance, ~furnitureLayerMask))
        {
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Furniture"))
            {
                detectedFurniture = hit.transform.gameObject;
                if (detectedFurniture.tag == "Pushable")
                {
                    rigidBody = detectedFurniture.GetComponent<Rigidbody>() == null ? detectedFurniture.AddComponent<Rigidbody>() : detectedFurniture.GetComponent<Rigidbody>();
                    rigidBody.constraints = RigidbodyConstraints.FreezePositionY;

                    rigidBody.AddForce(lineOfSightStartPoint.forward * forceAmount * forceMultiplier, ForceMode.Impulse); // pushing a game object
                    // cam.GetComponent<Follow_Player>().setCanShake(true);
                    // AlertEnemy(hit.transform.gameObject, 1);
                    Destroy(hit.transform.gameObject,1);
                    
                    // detectedFurniture.AddComponent<ObjectCollision>(); // toss this script and figure out another solution

                }
                else if (detectedFurniture.tag == "Heavy")
                {
                    // AlertEnemy(hit.transform.gameObject, 0);
                    Destroy(hit.transform.gameObject, 0);

                    // cam.GetComponent<Follow_Player>().setCanShake(true);
                }
            }
        }
    }


 }