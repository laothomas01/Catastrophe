using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerAttack : MonoBehaviour

{
    private Camera cam;
    public Transform lookPoint;
    public float hitDistance;
    private Queue<GameObject> seenFurniture;
    private Queue<GameObject> destroyedObjects;

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

    public LayerMask targetMask, obstacleMask;
    int rayCount;
    float rayAngleSize;
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
        seenFurniture = new Queue<GameObject>();
        destroyedObjects = new Queue<GameObject>();
        //look for all game objects with Enemy tag
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        layerMask = 1 << 7; //ignore layer

        audioManager = FindObjectOfType<AudioManager>();
    }

    //isAttacking = ;
    // if (Input.GetMouseButtonDown(0))
    // {
    //     Attack();
    // }
    //  DrawVision();



    void Update()
    {
        // Debug.DrawRay(lookPoint.position,lookPoint.forward*hitDistance * 4,Color.green);
        DetectFurniture();

        // if(fov.getMeshResolution() < 0)
        // {
        //     fov.meshResolution = 0;
        // }

        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }

    }

    //calls enemy 
    void AlertEnemy(GameObject furniture, float destroyTime)
    {
        GameObject closestEnemy = enemies.Length > 0 ? enemies[0] : null;
        float closestDistance = Mathf.Infinity;
        Transform furn = furniture.transform;
        if (destroyTime > 0)
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
        if (closestEnemy != null)
        {
            closestEnemy.GetComponent<EnemyController>().InspectFurniture(furn.transform);
        }

    }


    void DetectFurniture()
    {

        rayCount = Mathf.RoundToInt(fov.getViewAngle() * fov.getMeshResolution());
        rayAngleSize = fov.getViewAngle() / rayCount;

        // Debug.Log(rayCount);

        // int i = 0;
        // int j = rayCount - 1;
        // while (i < j)
        // {
        //     if (i >= j)
        //     {
        //         i = 0;
        //         j = rayCount - 1;
        //     }
        //     float left = transform.eulerAngles.y - fov.getViewAngle() / 2 + rayAngleSize * i;
        //     float right = transform.eulerAngles.y - fov.getViewAngle() / 2 + rayAngleSize * j;

        //     // Debug.Log(" i: " + i + " left: " + left + "," + " j: " + j + " right: " + right);
        //     RaycastHit hit;
        //     if (Physics.Raycast(lookPoint.position, fov.DirFromAngle(left, true) * fov.getViewRadius(), out hit, fov.getViewRadius())

        //     // || Physics.Raycast(lookPoint.position, fov.DirFromAngle(right, true) * fov.getViewRadius(), out hit, fov.getViewRadius())

        //     )
        //     {

        //         Debug.DrawRay(lookPoint.position, fov.DirFromAngle(left, true) * fov.getViewRadius(), Color.green);

        //         Debug.Log("hit object:" + hit.transform.name);
        //     }
        //     // else if (Physics.Raycast(lookPoint.position, fov.DirFromAngle(right, true) * fov.getViewRadius(), out hit, fov.getViewRadius()))
        //     // {

        //     //     Debug.DrawRay(lookPoint.position, fov.DirFromAngle(right, true) * fov.getViewRadius(), Color.green);

        //     //     Debug.Log("Ray: " + i + "at angle:" + right + "hit object:" + hit.transform.name);
        //     // }
        //     i++;
        //     j--;
        // }
        // for (int i = 0,j = rayCount; i < rayCount; i++)
        // {
        //     float angle = transform.eulerAngles.y - fov.getViewAngle() / 2 + rayAngleSize * i;
        //     // Debug.Log(" left side angle: " + i + " at " + angle + ":" + " right side angle: " + j + " at " + angle2);
        //     RaycastHit hit;
        //     if (Physics.Raycast(lookPoint.position, fov.DirFromAngle(angle, true) * fov.getViewRadius(), out hit, fov.getViewRadius()))
        //     {
        //         if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Furniture"))
        //         {

        //             Debug.DrawRay(lookPoint.position, fov.DirFromAngle(angle, true) * fov.getViewRadius(), Color.green);

        //             Debug.Log("Ray: " + i + "at angle:" + angle + "hit object:" + hit.transform.name);
        //         }
        //     }
        //     // else if (Physics.Raycast(lookPoint.position, fov.DirFromAngle(angle2, true) * fov.getViewRadius(), out hit, fov.getViewRadius()))
        //     // {
        //     //     if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Furniture"))
        //     //     {

        //     //         // Debug.DrawRay(lookPoint.position, fov.DirFromAngle(angle, true) * fov.getViewRadius(), Color.green);

        //     //         Debug.Log("Ray: " + j + "at angle:" + angle2 + "hit object:" + hit.transform.name);
        //     //     }
        //     // }
        // }
        // for (int j = rayCount; j >= 0; j--)
        // {
        //     float angle = transform.eulerAngles.y - fov.getViewAngle() / 2 + rayAngleSize * j;
        //     // Debug.Log(" left side angle: " + i + " at " + angle + ":" + " right side angle: " + j + " at " + angle2);
        //     RaycastHit hit;
        //     if (Physics.Raycast(lookPoint.position, fov.DirFromAngle(angle, true) * fov.getViewRadius(), out hit, fov.getViewRadius()))
        //     {
        //         if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Furniture"))
        //         {

        //             Debug.DrawRay(lookPoint.position, fov.DirFromAngle(angle, true) * fov.getViewRadius(), Color.green);

        //             Debug.Log("Ray: " + i + "at angle:" + angle + "hit object:" + hit.transform.name);
        //         }
        //     }
        //     // else if (Physics.Raycast(lookPoint.position, fov.DirFromAngle(angle2, true) * fov.getViewRadius(), out hit, fov.getViewRadius()))
        //     // {
        //     //     if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Furniture"))
        //     //     {

        //     //         // Debug.DrawRay(lookPoint.position, fov.DirFromAngle(angle, true) * fov.getViewRadius(), Color.green);

        //     //         Debug.Log("Ray: " + j + "at angle:" + angle2 + "hit object:" + hit.transform.name);
        //     //     }
        //     // }
        // }



        // Debug.Log(seenFurniture.Count);
        for (int i = 0, j = rayCount - 1; i < rayCount; i++, j--)
        {

            //the offset angle of the ray cast
            float angle = transform.eulerAngles.y - fov.getViewAngle() / 2 + rayAngleSize * i;
            if (j < 0)
            {
                j = rayCount;
            }
            // Debug.Log(" i: " + i + "," + " j: " + j);
            float angle2 = transform.eulerAngles.y - fov.getViewAngle() / 2 + rayAngleSize * j;

            // lookPoint.forward = fov.DirFromAngle(fovDirectionAngle, true);

            RaycastHit hit;
            Debug.DrawRay(lookPoint.position, fov.DirFromAngle(angle, true), Color.green);
            if (Physics.Raycast(lookPoint.position, fov.DirFromAngle(angle, true), out hit, fov.getViewRadius()) || Physics.Raycast(lookPoint.position, fov.DirFromAngle(angle2, true), out hit, fov.getViewRadius()))
            {

                if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Furniture"))
                {
                    Debug.DrawRay(lookPoint.position, fov.DirFromAngle(angle, true) * fov.getViewRadius(), Color.green);

                    // Debug.Log("DETECTED:" + hit.transform.name);
                    if (!seenFurniture.Contains(hit.transform.gameObject))
                    {
                        Renderer render = hit.transform.GetComponent<Renderer>();
                        if (hit.transform.tag == "Heavy")
                        {
                            render.material.color = Color.red;
                        }
                        else if (hit.transform.tag == "Pushable")
                        {
                            render.material.color = Color.blue;
                        }
                        seenFurniture.Enqueue(hit.transform.gameObject);

                    }
                }

            }

            else
            {
                // seenFurniture.Dequeue();
                // Debug.Log("NOT SEEING ANYTHING!");
                if (seenFurniture.Count > 0)
                {
                    GameObject target = seenFurniture.Dequeue();
                    if (target != null)
                    {
                        target.GetComponent<Renderer>().material.color = Color.white;

                    }
                }
            }

        }




    }
    void DrawVision()
    {
        int rayCount = Mathf.RoundToInt(fov.getViewAngle() * fov.getMeshResolution());
        float rayAngleSize = fov.getViewAngle() / rayCount;
        List<Vector3> viewPoints = new List<Vector3>();
        for (int i = 0; i <= rayCount; i++)
        {
            float angle = transform.eulerAngles.y - fov.getViewAngle() / 2 + rayAngleSize * i;
            Debug.DrawLine(transform.position, transform.position + fov.DirFromAngle(angle, true) * fov.getViewRadius(), Color.red);
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
        Vector3 dir = fov.DirFromAngle(globalAngle, true);
        RaycastHit hit;

        if (Physics.Raycast(transform.position, dir, out hit, fov.getViewRadius()))
        {
            GameObject hitObject = hit.transform.gameObject;
            if (hitObject.tag == "Walls" || hitObject.layer == LayerMask.NameToLayer("Furniture"))
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

        int rayCount = Mathf.RoundToInt(fov.getViewAngle() * fov.getMeshResolution());
        float rayAngleSize = fov.getViewAngle() / rayCount;
        for (int i = 0; i <= rayCount; i++)
        {
            //the offset angle of each ray cast
            float angle = transform.eulerAngles.y - fov.getViewAngle() / 2 + rayAngleSize * i;

            // Debug.DrawRay(lookPoint.position, fov.DirFromAngle(angle, true) * fov.getViewRadius(), Color.red);

            RaycastHit hit;
            if (Physics.Raycast(lookPoint.position, fov.DirFromAngle(angle, true), out hit, fov.getViewRadius()))
            {

                if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Furniture"))
                {
                    // float destroyTime;
                    if (hit.transform.tag == "Heavy")
                    {
                        // destroyTime = 0
                        // Destroy(hit.transform.gameObject, destroyTime);
                        // AlertEnemy(hit.transform.gameObject,0);
                        destroyedObjects.Enqueue(hit.transform.gameObject);
                        cam.GetComponent<Follow_Player>().setCanShake(true);

                        break;
                    }
                    else if (hit.transform.tag == "Pushable")
                    {
                        GameObject target = hit.transform.gameObject;
                        // destroyTime = 0.5f;
                        //dont give object rigid body if already has rigid body
                        if (target.GetComponent<Rigidbody>() == null)
                        {
                            target.AddComponent<Rigidbody>();
                        }
                        rb = target.GetComponent<Rigidbody>();
                        rb.AddForce(fov.DirFromAngle(angle, true) * forceAmount * forceMultiplier, ForceMode.Impulse);
                        rb.constraints = RigidbodyConstraints.FreezePositionY;
                        destroyedObjects.Enqueue(hit.transform.gameObject);
                        /*

                        Handle potential collisions with other furniture

                        */



                        // Destroy(hit.transform.gameObject, destroyTime);
                        cam.GetComponent<Follow_Player>().setCanShake(true);

                        //break because we hit with 1 raycast and dont need to multiple the physics by N amount of raycasts
                        break;
                    }
                }
            }

        }


        if (destroyedObjects.Count > 0)
        {
            GameObject target = destroyedObjects.Dequeue();
            if (target != null)
            {
                float destroyTime;
                if (target.tag == "Heavy")
                {
                    destroyTime = 0;
                    AlertEnemy(target, destroyTime);
                }
                else if (target.tag == "Pushable")
                {
                    destroyTime = 1.5f;
                    AlertEnemy(target, destroyTime);
                }
            }
        }

    }




}