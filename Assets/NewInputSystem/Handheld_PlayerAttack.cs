using UnityEngine;
using UnityEngine.InputSystem;

public class Handheld_PlayerAttack : MonoBehaviour
{

    PlayerInput playerInput; // unit new input system


//@TODO:  move to a player animation manager 
    // Animator animator;
    

    // private Camera cam; //we will handle this in another script maybe
    public Transform lineOfSightStartPoint; // location where line of sight begins 
    public float lineOfSightDistance; //how far the player can see
    
    [SerializeField]
    private float forceAmount;  // how hard to push an object
    [SerializeField]
    private float forceMultiplier; 

    Rigidbody rigidBody;


    // @TODO: move to a furniture script where we handle this function on an OnDestroy() function
    // we will probably want to add behaviors to furniture in the future
    // ex: exploding kitchen ovens
    // GameObject[] enemies; // put this into a script for destroyable furniture

    private Color originalDetectedFurnitureColor;
    int furnitureLayerMask;
    GameObject detectedFurniture;
    private bool furnitureColorChanged;

    //@TODO: move to a player audio manager script
    // AudioManager audioManager;
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        rigidBody = null;
        furnitureColorChanged = false;
        originalDetectedFurnitureColor = new Color();
        // detectedObjectOriginalColor = new Color();
        // currentObjectColor = new Color();
        detectedFurniture = null;
        // animator = GetComponent<Animator>();
        // camera = Camera.main;
        // alertedEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        furnitureLayerMask = 1 << 7; 
        // audioManager = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // Attack(playerInput.actions["ToggleAttack"].triggered);
        if (playerInput.actions["ToggleAttack"].triggered)
        {
            Attack();
        }
        // else
        // {
        //     animator.SetBool("HeavyAttacking", false);
        // }
        HandleLineOfSight();
        // Debug.Log(playerInput.actions["Attack"].triggered);
        Debug.DrawRay(lineOfSightStartPoint.position, lineOfSightStartPoint.forward * lineOfSightDistance, Color.red);

    }
    void HandleLineOfSight()
    {
        RaycastHit hit;
        if (Physics.Raycast(lineOfSightStartPoint.position, lineOfSightStartPoint.forward, out hit, lineOfSightDistance, ~furnitureLayerMask))
        {
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Furniture"))
            {
                // Debug.Log("Furniture");
                detectedFurniture = hit.transform.gameObject;
                Renderer furnitureRenderer = hit.transform.GetComponent<Renderer>();

                if (!furnitureColorChanged)
                {
                    originalDetectedFurnitureColor = furnitureRenderer.material.color;
                    if (detectedFurniture.tag == "Heavy")
                    {
                        furnitureRenderer.material.color = Color.red;
                    }
                    else if(detectedFurniture.tag == "Pushable")
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

    /// <summary>
    /// Method: Attack
    /// Approach: use a raycast for handling palyer's line of sight, detect a furniture that is pushable or heavy.
    ///           handle behavior of furniture based on type of furniture
    /// </summary>
    /// 

    //@TODO: need to check if object has already been hit and destroyed to prevent camera shake spamming
    void Attack()
    {
        Debug.Log("Attack!");
        RaycastHit hit;
        // animator.SetBool("HeavyAttacking", true);
        if (Physics.Raycast(lineOfSightStartPoint.position, lineOfSightStartPoint.forward, out hit, lineOfSightDistance, ~furnitureLayerMask))
        {
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Furniture"))
            {
                // ColorChanged();
                detectedFurniture = hit.transform.gameObject;
                if (detectedFurniture.tag == "Pushable")
                {
                    rigidBody = detectedFurniture.GetComponent<Rigidbody>() == null ? detectedFurniture.AddComponent<Rigidbody>() : detectedFurniture.GetComponent<Rigidbody>();
                    rigidBody.constraints = RigidbodyConstraints.FreezePositionY;
                    rigidBody.AddForce(lineOfSightStartPoint.forward * forceAmount * forceMultiplier, ForceMode.Impulse); // pushing a game object
                    // cam.GetComponent<Follow_Player>().setCanShake(true);
                    // AlertEnemy(hit.transform.gameObject, 1);
                    Destroy(hit.transform.gameObject,1);
                    // detectedFurniture.AddComponent<ObjectCollision>();

                }
                else if (detectedFurniture.tag == "Heavy")
                {       

                    Destroy(hit.transform.gameObject,0);
                    // AlertEnemy(hit.transform.gameObject, 0);
                    // camera.GetComponent<Follow_Player>().setCanShake(true);
                }
            }
        }
        // animator.SetBool("HeavyAttacking", false);
    }
}
