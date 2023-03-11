using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerAttack : MonoBehaviour

{
    private Camera cam;
    public Transform lookPoint;
    public float hitDistance;
    
    FieldOfView fov;
    [SerializeField]
    private float forceAmount;
    [SerializeField]
    private float forceMultiplier;

    Rigidbody rb;

    GameObject[] enemies;
    
    private Color originalColor;

    int layerMask;
    GameObject hitObj;
    private bool colorChanged;
    AudioManager audioManager;

    Mesh viewMesh;

     public MeshFilter viewMeshFilter;

    public LayerMask targetMask,obstacleMask;
    void Start()
    {

        viewMesh = new Mesh();
        // viewMesh.name = "View Mesh";
        // viewMeshFilter.mesh = viewMesh;
        fov = GetComponent<FieldOfView>();
        rb = null;
        colorChanged = false;
        originalColor = new Color();
        hitObj = new GameObject();
        cam = Camera.main;
        
        //look for all game objects with Enemy tag
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        layerMask = 1 << 7; //ignore layer

        audioManager = FindObjectOfType<AudioManager>();
    }
    void LateUpdate() {

    //isAttacking = ;
    // if (Input.GetMouseButtonDown(0))
    // {
    //     Attack();
    // }
    //  DrawVision();

   

    }
    void Update()
    {

    //      Debug.DrawRay(lookPoint.position,lookPoint.forward + ,Color.red);
    // Debug.DrawRay(lookPoint.position,lookPoint.forward*hitDistance * 2,Color.blue);
    // Debug.DrawRay(lookPoint.position,lookPoint.forward*hitDistance * 4,Color.green);
        DetectFurniture();

        // if(fov.getMeshResolution() < 0)
        // {
        //     fov.meshResolution = 0;
        // }
    }

//calls enemy 
 void AlertEnemy(GameObject furniture,float destroyTime)
    {
        GameObject closestEnemy = enemies.Length > 0 ? enemies[0]: null;
        float closestDistance = Mathf.Infinity;
        Transform furn = furniture.transform;
        if(destroyTime > 0)
        {
            audioManager.Play("brokenwood");
        }
        else
        {
            Destroy(furniture);
            audioManager.Play("brokenwood");
        }

        foreach (GameObject enemy in enemies)
        {
            float currDistance = Vector3.Distance(enemy.transform.position, furniture.transform.position);
            if (currDistance <= closestDistance)
            {
                closestEnemy = enemy;
                closestDistance = currDistance;
            }
        }
        if(closestEnemy != null)
        {
            closestEnemy.GetComponent<EnemyController>().InspectFurniture(furn.transform);
        }

    }


    void DetectFurniture()
    {
        // Collider [] targetsInViewRadius = Physics.OverlapSphere(lookPoint.position,fov.getViewRadius(),targetMask);
        
        int rayCount = Mathf.RoundToInt(fov.getViewAngle() * fov.getMeshResolution());
        float rayAngleSize = fov.getViewAngle() / rayCount;
        // List<Vector3> viewPoints = new List<Vector3>();
        for (int i = 0; i <= rayCount; i++)
        {
            //the offset angle of the ray cast
            float angle = transform.eulerAngles.y - fov.getViewAngle() / 2 + rayAngleSize * i;
            
            Debug.DrawRay(lookPoint.position,fov.DirFromAngle(angle,true) * fov.getViewRadius(),Color.green);

            RaycastHit hit;
            if(Physics.Raycast(lookPoint.position,fov.DirFromAngle(angle,true),out hit, fov.getViewRadius(),targetMask))
            {
                    
            }
            // Debug.DrawLine(lookPoint.position,lookPoint.position + fov.DirFromAngle(angle,true) * fov.getViewRadius(),Color.green);

            
           
        //     RaycastHit hit;
        //     if(Physics.Raycast(lookPoint.position, fov.DirFromAngle(angle,true) * fov.getViewRadius(),out hit))
        //     {
        //          if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Furniture"))
        //     {
        //         hitObj = hit.transform.gameObject;
        //         Renderer renderer = hit.transform.GetComponent<Renderer>();

        //         if (!colorChanged)
        //         {
        //             originalColor = renderer.material.color;

        //             if (hitObj.tag == "Heavy")
        //             {
        //                 renderer.material.color = Color.red;
        //             }

        //             else if (hitObj.tag == "Pushable")
        //             {
        //                 renderer.material.color = Color.blue;
        //             }
        //             colorChanged = true;
        //         }
        //     }
        //     }

        //     else
        // {
        //     if (hitObj != null)
        //     {
        //         if (hitObj.layer == LayerMask.NameToLayer("Furniture"))
        //         {
        //             hitObj.GetComponent<Renderer>().material.color = originalColor;
        //             colorChanged = false;

        //         }
        //     }


        // }
            // if(Physics.Raycast(lookPoint.position,lookPoint.position + fov.DirFromAngle(angle,true) * fov.getViewRadius(),out hit,)
            // RaycastHit hit;


            // ViewCastInfo newViewCast = ViewCast(angle);
            // viewPoints.Add(newViewCast.point);

        }
            // Collider [] targetsInViewRadius = Physics.OverlapSphere(transform.position,fov.getViewRadius(),targetMask);

            // foreach(Collider c in targetsInViewRadius)
            // {
            //     Transform target = c.transform;
            //     Vector3 dirToTarget = (target.position - transform.position).normalized;
            //     Debug.Log(" ANGLE BETWEEN: " + Vector3.Angle(transform.forward,dirToTarget) + " FOV ANGLE: " + fov.getViewAngle()/2);
            //     Debug.Log(" DISTANCE: " + Vector3.Distance(transform.position,target.position) + " VIEW RADIUS: " + fov.getViewRadius());
            //     if(Vector3.Angle(transform.forward,dirToTarget) < fov.getViewAngle()/2)
            //     {
            //                 float dstToTarget = Vector3.Distance(transform.position,target.position);
            //                 RaycastHit hit;
            //                 if(!Physics.Raycast(lookPoint.position,dirToTarget, out hit, dstToTarget,obstacleMask))
            //                 {
                                
            //                     Debug.DrawLine(lookPoint.position,target.position,Color.blue);

            //                 }
            //     }
            // }
            // int stepCount = Mathf.RoundToInt(fov.getViewAngle() * fov.getMeshResolution() );
            // //size of singular view angle
            // float stepAngleSize = fov.getViewAngle() / stepCount;
            // for(int i = 0; i <= stepCount; i++)
            // {
            //     // float angle = transform.eulerAngles.y - fov.getViewAngle()/2 + stepAngleSize * i;
                
            //     // Debug.DrawLine(lookPoint.position,lookPoint.forward * hitDistance * fov.DirFromAngle(fov.getViewAngle(),true) * fov.getViewRadius(),Color.red);
            // }
        // //     // if( < fov.getViewAngle()/2)
        // //     // {
        // //     //     float dstToTarget = Vector3.Distance
        // //     // }
        // // }
        // // RaycastHit hit;

        // // if (Physics.Raycast(lookPoint.position, lookPoint.forward, out hit, hitDistance, ~layerMask))
        // // {
        // //     if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Furniture"))
        // //     {
        // //         hitObj = hit.transform.gameObject;
        // //         Renderer renderer = hit.transform.GetComponent<Renderer>();

        // //         if (!colorChanged)
        // //         {
        // //             originalColor = renderer.material.color;

        // //             if (hitObj.tag == "Heavy")
        // //             {
        // //                 renderer.material.color = Color.red;
        // //             }

        // //             else if (hitObj.tag == "Pushable")
        // //             {
        // //                 renderer.material.color = Color.blue;
        // //             }
        // //             colorChanged = true;
        // //         }
        // //     }
        // // }
        // // else
        // // {
        // //     if (hitObj != null)
        // //     {
        // //         if (hitObj.layer == LayerMask.NameToLayer("Furniture"))
        // //         {
        // //             hitObj.GetComponent<Renderer>().material.color = originalColor;
        // //             colorChanged = false;

        // //         }
        // //     }


        // }

    }
 void DrawVision()
    {
        int rayCount = Mathf.RoundToInt(fov.getViewAngle() * fov.getMeshResolution());
        float rayAngleSize = fov.getViewAngle() / rayCount;
        List<Vector3> viewPoints = new List<Vector3>();
        for (int i = 0; i <= rayCount; i++)
        {
            float angle = transform.eulerAngles.y - fov.getViewAngle() / 2 + rayAngleSize * i;
            Debug.DrawLine(transform.position,transform.position + fov.DirFromAngle(angle,true) * fov.getViewRadius(),Color.green);
            ViewCastInfo newViewCast = ViewCast(angle);
            viewPoints.Add(newViewCast.point);
        }


        int vertexCount = viewPoints.Count + 1;
        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[(vertexCount - 2) * 3];
        vertices[0] = Vector3.zero;
        for (int i = 0; i < vertexCount - 1; i++)
        {
            vertices[i + 1] = transform.InverseTransformPoint(viewPoints[i]);
            if (i < vertexCount - 2)
            {
                triangles[i * 3] = 0;
                triangles[i * 3 + 1] = 1 + 1;
                triangles[i * 3 + 2] = i + 2;
            }
        }
        viewMesh.Clear();
        viewMesh.vertices = vertices;
        viewMesh.triangles = triangles;
        viewMesh.RecalculateNormals();

    }

       ViewCastInfo ViewCast(float globalAngle)
    {
        Vector3 dir = fov.DirFromAngle(globalAngle,true);
        RaycastHit hit;

        if (Physics.Raycast(transform.position, dir, out hit, fov.getViewRadius()))
        {
            GameObject hitObject = hit.transform.gameObject;
            if(hitObject.tag == "Walls" || hitObject.layer == LayerMask.NameToLayer("Furniture"))
            return new ViewCastInfo(true, hit.point, hit.distance, globalAngle);
        }
            return new ViewCastInfo(false, transform.position + dir * fov.getViewRadius(), fov.getViewRadius(), globalAngle);
    }

    //Raycast info struct
    public struct ViewCastInfo
    {
        public bool hit;
        public Vector3 point;
        public float dst;
        public float angle;

        public ViewCastInfo(bool _hit, Vector3 _point, float _dst, float _angle)
        {
            hit = _hit;
            point = _point;
            dst = _dst;
            angle = _angle;
        }
    }


    void Attack()
    {
        // RaycastHit hit;
        // if (Physics.Raycast(lookPoint.position, lookPoint.forward, out hit, hitDistance, ~layerMask))
        // {
        //     if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Furniture"))
        //     {


        //         hitObj = hit.transform.gameObject;
        //         if (hitObj.tag == "Pushable")
        //         {
        //             rb = hitObj.GetComponent<Rigidbody>() == null ? hitObj.AddComponent<Rigidbody>() : hitObj.GetComponent<Rigidbody>();
        //             rb.constraints = RigidbodyConstraints.FreezePositionY;
        //             rb.AddForce(lookPoint.forward * forceAmount * forceMultiplier, ForceMode.Impulse);
        //             cam.GetComponent<Follow_Player>().setCanShake(true);
        //             AlertEnemy(hit.transform.gameObject, 1);
        //             hitObj.AddComponent<ObjectCollision>();

        //         }
        //         else if (hitObj.tag == "Heavy")
        //         {
        //             AlertEnemy(hit.transform.gameObject, 0);
        //             cam.GetComponent<Follow_Player>().setCanShake(true);
        //         }
        //     }
        // }
    } 
    
   


 }