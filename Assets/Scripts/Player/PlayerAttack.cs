using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{

    
    PlayerMovement playerMovement; //use player's look direction for attack direction
    public int attackRange; 
    int rayCount = 1; // will use for cone based raycasting
    public Transform lookPoint;  // initial position of raycast for looking and attacking
    // int rayAngleSize 
    Vector3 attackDirection;
    GameObject currentDetectedObject; 
    Color originalObjectColor;
    public int pushForceAmount;
    
    bool isAttacking = false;
    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        currentDetectedObject = null;
    }
    private void Update()
    {
        HandlePlayerFurnitureInteraction();
    }

    /*
       can this function be ported to mobile:
        - maybe. will need to tweak some functionalities.
            - this is in the department of player movment. maybe???? 
    */
    private void HandlePlayerFurnitureInteraction()
    {

        RaycastHit hit;
        attackDirection = Vector3.ClampMagnitude(playerMovement.getLookDirection(), attackRange);
        Debug.DrawRay(lookPoint.position, attackDirection, Color.red);
        int targetLayerMask_ = 1 << 9; // 1<< 9 = furniture layer

        if (Physics.Raycast(lookPoint.position, attackDirection, out hit, attackDirection.magnitude, targetLayerMask_))
        {
            handleDetection(hit.transform.gameObject);
        }
        else
        {
            // Reset the color of the previously detected object
            if (currentDetectedObject != null)
            {
                Renderer previousRenderer = currentDetectedObject.GetComponent<Renderer>();
                previousRenderer.material.color = originalObjectColor;
                currentDetectedObject = null;
            }
        }


        // if(Physics.Raycast(lookPoint.position,playerMovement.getLookDirection()))
        // Debug.DrawRay(lookPoint.position,);
        //  for (int i = 0, j = rayCount - 1; i < rayCount; i++, j--)
        //         {

        //             //the offset angle of the ray cast
        //             float angle = transform.eulerAngles.y - fov.getViewAngle() / 2 + rayAngleSize * i;

        //             if (j < 0)
        //             {
        //                 j = rayCount;
        //             }
        //             // Debug.Log(" i: " + i + "," + " j: " + j);
        //             float angle2 = transform.eulerAngles.y - fov.getViewAngle() / 2 + rayAngleSize * j;

        //             // lookPoint.forward = fov.DirFromAngle(fovDirectionAngle, true);

        //             RaycastHit hit;
        //             // Debug.DrawRay(lookPoint.position, fov.DirFromAngle(angle, true), Color.green);
        //             if (Physics.Raycast(lookPoint.position, fov.DirFromAngle(angle, true), out hit, fov.getViewRadius()) || Physics.Raycast(lookPoint.position, fov.DirFromAngle(angle2, true), out hit, fov.getViewRadius()))
        //             {

        //                 if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Furniture"))
        //                 {
        //                     // Debug.DrawRay(lookPoint.position, fov.DirFromAngle(angle, true) * fov.getViewRadius(), Color.green);

        //                     if (!seenFurniture.Contains(hit.transform.gameObject))
        //                     {
        //                         Renderer render = hit.transform.GetComponent<Renderer>();
        //                         if (hit.transform.tag == "Heavy")
        //                         {
        //                             render.material.color = Color.red;
        //                         }
        //                         else if (hit.transform.tag == "Pushable")
        //                         {
        //                             render.material.color = Color.blue;
        //                         }
        //                         seenFurniture.Enqueue(hit.transform.gameObject);

        //                     }
        //                 }

        //             }

        //             else
        //             {
        //                 if (seenFurniture.Count > 0)
        //                 {
        //                     GameObject target = seenFurniture.Dequeue();
        //                     if (target != null)
        //                     {
        //                         target.GetComponent<Renderer>().material.color = Color.white;

        //                     }
        //                 }
        //             }

    }
    //can this function be translated to mobile? 
    // yes
    void handleDetection(GameObject gameObject)
    {
        if (gameObject.tag == "Heavy")
        {
            GameObject detectedObject = gameObject;
            if (detectedObject != currentDetectedObject)
            {
                //Reset the color of the previously detected object
                if (currentDetectedObject != null)
                {
                    Renderer previousRenderer = currentDetectedObject.GetComponent<Renderer>();
                    previousRenderer.material.color = originalObjectColor;
                }
                Renderer renderer = detectedObject.GetComponent<Renderer>();
                originalObjectColor = renderer.material.color;
                renderer.material.color = Color.red;
                //Update the current detected object refernce
                currentDetectedObject = detectedObject;

            }
        }
        else if (gameObject.tag == "Pushable")
        {
            GameObject detectedObject = gameObject;
            if (detectedObject != currentDetectedObject)
            {
                //Reset the color of the previously detected object
                if (currentDetectedObject != null)
                {
                    Renderer previousRenderer = currentDetectedObject.GetComponent<Renderer>();
                    previousRenderer.material.color = originalObjectColor;
                }
                Renderer renderer = detectedObject.GetComponent<Renderer>();
                originalObjectColor = renderer.material.color;
                renderer.material.color = Color.blue;
                //Update the current detected object refernce
                currentDetectedObject = detectedObject;

            }
        }
    }


//can this code be translated to mobile? 
    public void OnAttack(InputAction.CallbackContext contxt)
    {
        switch (contxt.phase)
        {
            case InputActionPhase.Performed:
                Debug.Log("Attacking");
                isAttacking = true;
                GetComponent<Animator>().SetBool("HeavyAttacking",isAttacking);
                if (currentDetectedObject != null)
                {
                    if (currentDetectedObject.tag == "Heavy")
                    {
                        /*
                        @TODO: write a 
                        */
                        Destroy(currentDetectedObject,0.2f);
                    }
                    else if (currentDetectedObject.tag == "Pushable")
                    {
                        currentDetectedObject.AddComponent<Rigidbody>();
                        Rigidbody rb = currentDetectedObject.GetComponent<Rigidbody>();
                        rb.AddForce(attackDirection * pushForceAmount, ForceMode.Impulse);
                    }
                }
                break;
            case InputActionPhase.Canceled:
                isAttacking = false;
                GetComponent<Animator>().SetBool("HeavyAttacking",isAttacking);
                Debug.Log("Not Attacking!");
                break;
        }
    }

}


// ========================== DO NOT DELETE THE CODE BELOW. DOING CODE CLEAN UP ====================================
// using TMPro;
// public class PlayerAttack : MonoBehaviour

// {
//     private Camera cam;
//     public Transform lookPoint;
//     public float hitDistance;
//     private Queue<GameObject> seenFurniture;
//     private Queue<GameObject> destroyedObjects;

//     FieldOfView fov;
//     [SerializeField]
//     private float forceAmount;
//     [SerializeField]
//     private float forceMultiplier;

//     Rigidbody rb;

//     GameObject[] enemies;

//     private Color originalColor;

//     int layerMask;
//     GameObject hitObj;
//     private bool colorChanged;
//     AudioManager audioManager;
//     Mesh viewMesh;

//     public MeshFilter viewMeshFilter;

//     public LayerMask targetMask, obstacleMask;
//     int rayCount;
//     float rayAngleSize;
//     void Start()
//     {


//         viewMesh = new Mesh();
//         // viewMesh.name = "View Mesh";
//         // viewMeshFilter.mesh = viewMesh;
//         fov = GetComponent<FieldOfView>();
//         rb = null;
//         colorChanged = false;
//         originalColor = new Color();
//         hitObj = new GameObject();
//         cam = Camera.main;
//         seenFurniture = new Queue<GameObject>();
//         destroyedObjects = new Queue<GameObject>();
//         //look for all game objects with Enemy tag
//         enemies = GameObject.FindGameObjectsWithTag("Enemy");
//         layerMask = 1 << 7; //ignore layer

//         audioManager = FindObjectOfType<AudioManager>();
//     }





//     void Update()
//     {
//         // Debug.DrawRay(lookPoint.position,lookPoint.forward*hitDistance * 4,Color.green);
//         DetectFurniture();

//         // if(fov.getMeshResolution() < 0)
//         // {
//         //     fov.meshResolution = 0;
//         // }
//         DrawVision();
//         if (Input.GetMouseButtonDown(0))
//         {
//             Attack();
//         }

//     }

//     //calls enemy 
//     void AlertEnemy(GameObject furniture, float destroyTime)
//     {
//         GameObject closestEnemy = enemies.Length > 0 ? enemies[0] : null;
//         float closestDistance = Mathf.Infinity;
//         Transform furn = furniture.transform;
//         if (destroyTime > 0)
//         {
//             Destroy(furniture, destroyTime);
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
//         if (closestEnemy != null)
//         {
//             closestEnemy.GetComponent<EnemyController>().InspectFurniture(furn.transform);
//         }

//     }


//     void DetectFurniture()
//     {

//         rayCount = Mathf.RoundToInt(fov.getViewAngle() * fov.getMeshResolution());
//         rayAngleSize = fov.getViewAngle() / rayCount;


//         //calculating index of maximum and minimum angle
//         for (int i = 0, j = rayCount - 1; i < rayCount; i++, j--)
//         {

//             //the offset angle of the ray cast
//             float angle = transform.eulerAngles.y - fov.getViewAngle() / 2 + rayAngleSize * i;

//             if (j < 0)
//             {
//                 j = rayCount;
//             }
//             // Debug.Log(" i: " + i + "," + " j: " + j);
//             float angle2 = transform.eulerAngles.y - fov.getViewAngle() / 2 + rayAngleSize * j;

//             // lookPoint.forward = fov.DirFromAngle(fovDirectionAngle, true);

//             RaycastHit hit;
//             // Debug.DrawRay(lookPoint.position, fov.DirFromAngle(angle, true), Color.green);
//             if (Physics.Raycast(lookPoint.position, fov.DirFromAngle(angle, true), out hit, fov.getViewRadius()) || Physics.Raycast(lookPoint.position, fov.DirFromAngle(angle2, true), out hit, fov.getViewRadius()))
//             {

//                 if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Furniture"))
//                 {
//                     // Debug.DrawRay(lookPoint.position, fov.DirFromAngle(angle, true) * fov.getViewRadius(), Color.green);

//                     if (!seenFurniture.Contains(hit.transform.gameObject))
//                     {
//                         Renderer render = hit.transform.GetComponent<Renderer>();
//                         if (hit.transform.tag == "Heavy")
//                         {
//                             render.material.color = Color.red;
//                         }
//                         else if (hit.transform.tag == "Pushable")
//                         {
//                             render.material.color = Color.blue;
//                         }
//                         seenFurniture.Enqueue(hit.transform.gameObject);

//                     }
//                 }

//             }

//             else
//             {
//                 if (seenFurniture.Count > 0)
//                 {
//                     GameObject target = seenFurniture.Dequeue();
//                     if (target != null)
//                     {
//                         target.GetComponent<Renderer>().material.color = Color.white;

//                     }
//                 }
//             }

//         }




//     }
//     void DrawVision()
//     {
//         int rayCount = Mathf.RoundToInt(fov.getViewAngle() * fov.getMeshResolution());
//         float rayAngleSize = fov.getViewAngle() / rayCount;
//         List<Vector3> viewPoints = new List<Vector3>();
//         for (int i = 0; i <= rayCount; i++)
//         {
//             float angle = transform.eulerAngles.y - fov.getViewAngle() / 2 + rayAngleSize * i;
//             Debug.DrawLine(transform.position, transform.position + fov.DirFromAngle(angle, true) * fov.getViewRadius(), Color.red);
//             ViewCastInfo newViewCast = ViewCast(angle);
//             viewPoints.Add(newViewCast.point);
//         }


//         int vertexCount = viewPoints.Count + 1;
//         Vector3[] vertices = new Vector3[vertexCount];
//         int[] triangles = new int[(vertexCount - 2) * 3];
//         vertices[0] = Vector3.zero;
//         for (int i = 0; i < vertexCount - 1; i++)
//         {
//             vertices[i + 1] = transform.InverseTransformPoint(viewPoints[i]);
//             if (i < vertexCount - 2)
//             {
//                 triangles[i * 3] = 0;
//                 triangles[i * 3 + 1] = 1 + 1;
//                 triangles[i * 3 + 2] = i + 2;
//             }
//         }
//         viewMesh.Clear();
//         viewMesh.vertices = vertices;
//         viewMesh.triangles = triangles;
//         viewMesh.RecalculateNormals();

//     }

//     ViewCastInfo ViewCast(float globalAngle)
//     {
//         Vector3 dir = fov.DirFromAngle(globalAngle, true);
//         RaycastHit hit;

//         if (Physics.Raycast(transform.position, dir, out hit, fov.getViewRadius()))
//         {
//             GameObject hitObject = hit.transform.gameObject;
//             if (hitObject.tag == "Walls" || hitObject.layer == LayerMask.NameToLayer("Furniture"))
//                 return new ViewCastInfo(true, hit.point, hit.distance, globalAngle);
//         }
//         return new ViewCastInfo(false, transform.position + dir * fov.getViewRadius(), fov.getViewRadius(), globalAngle);
//     }

//     //Raycast info struct
//     public struct ViewCastInfo
//     {
//         public bool hit;
//         public Vector3 point;
//         public float dst;
//         public float angle;

//         public ViewCastInfo(bool _hit, Vector3 _point, float _dst, float _angle)
//         {
//             hit = _hit;
//             point = _point;
//             dst = _dst;
//             angle = _angle;
//         }
//     }


//     void Attack()
//     {

//         int rayCount = Mathf.RoundToInt(fov.getViewAngle() * fov.getMeshResolution());
//         float rayAngleSize = fov.getViewAngle() / rayCount;


//         for (int i = 0, j = rayCount; i <= rayCount; i++, j--)
//         {


//             //minimum offset angle
//             float angle = transform.eulerAngles.y - fov.getViewAngle() / 2 + rayAngleSize * i;


//             if (j < 0)
//             {
//                 j = rayCount;
//             }
//             //maximum offset angle
//             float angle2 = transform.eulerAngles.y - fov.getViewAngle() / 2 + rayAngleSize * j;
//             // Debug.DrawRay(lookPoint.position, fov.DirFromAngle(angle, true) * fov.getViewRadius(), Color.red);

//             RaycastHit hit;

//             if (Physics.Raycast(lookPoint.position, fov.DirFromAngle(angle, true), out hit, fov.getViewRadius()) || Physics.Raycast(lookPoint.position, fov.DirFromAngle(angle2, true), out hit, fov.getViewRadius()))
//             {

//                 if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Furniture"))
//                 {
//                     // float destroyTime;
//                     if (hit.transform.tag == "Heavy")
//                     {
//                         destroyedObjects.Enqueue(hit.transform.gameObject);
//                         cam.GetComponent<Follow_Player>().setCanShake(true);
//                         break;
//                     }
//                     else if (hit.transform.tag == "Pushable")
//                     {
//                         GameObject target = hit.transform.gameObject;
//                         // destroyTime = 0.5f;
//                         //dont give object rigid body if already has rigid body
//                         if (target.GetComponent<Rigidbody>() == null)
//                         {
//                             target.AddComponent<Rigidbody>();
//                         }
//                         rb = target.GetComponent<Rigidbody>();
//                         rb.AddForce(fov.DirFromAngle(angle, true) * forceAmount * forceMultiplier, ForceMode.Impulse);

//                         rb.constraints = RigidbodyConstraints.FreezePositionY;

//                         /*

//                         Handle potential collisions with other furniture

//                         */

//                         destroyedObjects.Enqueue(target);
//                         cam.GetComponent<Follow_Player>().setCanShake(true);

//                         break;
//                     }
//                 }
//             }

//         }


//         if (destroyedObjects.Count > 0)
//         {
//             GameObject target = destroyedObjects.Dequeue();
//             if (target != null)
//             {
//                 if (target.tag == "Heavy")
//                 {
//                     AlertEnemy(target, 0);
//                 }
//                 else if (target.tag == "Pushable")
//                 {
//                     AlertEnemy(target, 0.5f);

//                 }
//             }
//         }

//     }




// }