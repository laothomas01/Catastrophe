using UnityEngine;
using UnityEngine.InputSystem;

public class Attack_AndroidControls : MonoBehaviour
{
    Animator animator;
    PlayerInput playerInput;

    //reference variable used for camera SFX triggered by player attack events
    Camera camera;
    public Transform attackFieldOfView;
    public float hitDistance;
    [SerializeField]
    float attackForceAmount;
    [SerializeField]
    float attackForceMultiplier;
    Rigidbody rb;

    //array of enemy AIs
    GameObject[] alertedEnemies;

    Color currentObjectColor;
    bool colorChanged;

    //ignore every other layer number except current layer i want
    int furnitureLayerMask;
    GameObject currentHitObj;
    //reference variable audo manager for sound SFX triggered by player attack events 
    AudioManager audioManager;
    void Awake()
    {
        rb = null;
        colorChanged = false;
        currentObjectColor = new Color();
        currentHitObj = new GameObject();
        animator = GetComponent<Animator>();
        camera = Camera.main;
        alertedEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        furnitureLayerMask = 1 << 7;
        audioManager = FindObjectOfType<AudioManager>();
    }
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        Attack(playerInput.actions["ToggleAttack"].triggered);
        // Debug.Log(playerInput.actions["Attack"].triggered);
        Debug.DrawRay(attackFieldOfView.position, attackFieldOfView.forward * hitDistance, Color.red);

    }
    void ColorChanged()
    {
        RaycastHit hit;
        if (Physics.Raycast(attackFieldOfView.position,
        attackFieldOfView.forward, out hit, hitDistance, ~furnitureLayerMask))
        {
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Furniture"))
            {
                currentHitObj = hit.transform.gameObject;
                Renderer renderer = hit.transform.GetComponent<Renderer>();

                if (!colorChanged)
                {
                    currentObjectColor = renderer.material.color;

                    if (currentHitObj.tag == "Heavy")
                    {
                        renderer.material.color = Color.red;
                    }

                    else if (currentHitObj.tag == "Pushable")
                    {
                        renderer.material.color = Color.blue;
                    }
                    colorChanged = true;
                }
            }
            else
            {
                if (currentHitObj != null)
                {
                    if (currentHitObj.layer == LayerMask.NameToLayer("Furniture"))
                    {
                        currentHitObj.GetComponent<Renderer>().material.color = currentObjectColor;
                        colorChanged = false;
                    }
                }
            }
        }
    }
    void Attack(bool attack)
    {
        animator.SetBool("HeavyAttacking", attack);
        if (attack)
        {
            RaycastHit hit;
            if (Physics.Raycast(attackFieldOfView.position, attackFieldOfView.forward, out hit, hitDistance, ~furnitureLayerMask))
            {
                if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Furniture"))
                {
                    currentHitObj = hit.transform.gameObject;
                    if (currentHitObj.tag == "Pushable")
                    {
                        rb = currentHitObj.GetComponent<Rigidbody>() == null ? currentHitObj.AddComponent<Rigidbody>() : currentHitObj.GetComponent<Rigidbody>();
                        rb.constraints = RigidbodyConstraints.FreezePositionY;
                        rb.AddForce(attackFieldOfView.forward * attackForceAmount * attackForceMultiplier, ForceMode.Impulse);
                        camera.GetComponent<Follow_Player>().setCanShake(true);
                        // AlertEnemy(hit.transform.gameObject, 1);
                        currentHitObj.AddComponent<ObjectCollision>();

                    }
                    else if (currentHitObj.tag == "Heavy")
                    {
                        // AlertEnemy(hit.transform.gameObject, 0);
                        camera.GetComponent<Follow_Player>().setCanShake(true);
                    }
                }
            }
        }
    }
}
