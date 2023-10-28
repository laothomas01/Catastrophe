using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;
/// <summary>
/// for Enemy animations and enemy behaviors
/// </summary>
public class EnemyController : MonoBehaviour
{
    /*
     * [X] Patrol patterns (list of positions that the enemy patrols in)
     * [X] Look around the room for a set amount of seconds and then go back
     * [X] If the Cat is spotted, game over
     * [X] Add animations
     * [] Add sounds
     */
    // Start is called before the first frame update
    public Transform player;
    NavMeshAgent agent;
    public float lerpRotationSpeed;
    public float rotateTime;
    public float rotateAmount;
    private float rotateRight;
    private float rotateLeft;
    private float maxDegrees;
    private float rotateTimer;
    private bool enemyRotating = false;
    private bool enemyAlerted = false;
    public Transform raycastOrigin;
    public GameObject gameOverScreen;
    private Quaternion currentRotation;
    //PlayerDetection
    public MeshFilter viewMeshFilter;
    Mesh viewMesh;
    Animator animator;
    //patrolling
    public Vector3[] patrolPoints;
    public float waitBetweenPatrol;
    private float patrolWaitTimer;
    public float viewAngle;
    public float viewRadius;
    private float catDistance;
    public float visionResoulution;

    //add a delay for when the enemy turns around
    public float waitTimeBeforeRotation = 0.5f;

    void Start()
    {
        viewMesh = new Mesh();
        viewMesh.name = "View Mesh";
        viewMeshFilter.mesh = viewMesh;

        animator = GetComponent<Animator>();
        patrolWaitTimer = waitBetweenPatrol;
        rotateTimer = 0;
        rotateRight = rotateAmount;
        rotateLeft = rotateAmount;
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (VisibleOnScreen())
        {
            // this.GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
            // this.GetComponentInChildren<MeshRenderer>().enabled = true;
            SearchForCat();
        }
        // else
        // {
        //     this.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
        //     this.GetComponentInChildren<MeshRenderer>().enabled = false;
        //     SearchForCat();
        // }

        if (AgentReachedDestination(agent))
        {
            LookAround();
        }
        else
        {
            Patrol();
        }

        animator.SetBool("isWalking", agent.velocity.magnitude > 0);
    }

    /// <summary>
    /// - is enemy is in camera frustrum, begin patrol for cat
    /// 
    /// - is not on screen, turn off renderer?
    /// </summary>
    /// <returns>
    /// enemy is visible on screen
    /// </returns>

    bool VisibleOnScreen()
    {
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
        bool onScreen = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
        return onScreen;
    }

    bool AgentReachedDestination(NavMeshAgent a)
    {
        if (enemyAlerted && !a.pathPending)
        {
            if (a.remainingDistance <= a.stoppingDistance)
            {
                if (!a.hasPath || a.velocity.sqrMagnitude == 0f)
                {
                    return true;
                }
            }
        }
        return false;
    }

    void Patrol()
    {
        //Debug.Log(AgentReachedDestination(agent));
        if (patrolPoints.Length > 0 && !enemyAlerted && patrolWaitTimer >= waitBetweenPatrol)
        {
            int patrolPoint = Random.Range(0, patrolPoints.Length);
            agent.SetDestination(patrolPoints[patrolPoint]);
            patrolWaitTimer = 0;
        }
        patrolWaitTimer += Time.deltaTime;

    }

     void LookAround()
    {
        Debug.Log("Looking Around");
        if (!enemyRotating)
        {
            //making sure the angle stays between 0 and 360. If over, subtract 360. If under, add the negative number to 360
            rotateRight = currentRotation.eulerAngles.y + rotateRight > 360 ? (currentRotation.eulerAngles.y + rotateRight) - 360 : currentRotation.eulerAngles.y + rotateRight;
            rotateLeft = currentRotation.eulerAngles.y - rotateLeft < 0 ? 360 + (currentRotation.eulerAngles.y - rotateLeft) : currentRotation.eulerAngles.y - rotateLeft;
            rotateAmount = rotateRight;
            enemyRotating = true;
        }
        else
        {
            currentRotation = transform.rotation;
            transform.rotation = Quaternion.RotateTowards(currentRotation, Quaternion.Euler(0, rotateAmount, 0), maxDegrees);
            maxDegrees += lerpRotationSpeed * Time.deltaTime;
            if (Mathf.Abs((int)transform.eulerAngles.y) == Mathf.Abs((int)rotateAmount))
            {
                //invert the rotation
                rotateAmount = rotateAmount == rotateRight ? rotateLeft : rotateRight;
                maxDegrees = 0;
                rotateTimer++;
                if (rotateTimer >= rotateTime)
                {
                    rotateRight = rotateLeft = 90;
                    enemyAlerted = false;
                    rotateTimer = 0;
                    enemyRotating = false;
                }
            }
        }

    }

    //given a furniture object from an event, perform inspection

    //i would like the enemy ai to wait a few seconds before inspection begins 

    //i will turn this into a coroutine
    public IEnumerator InspectFurniture(Vector3 position)
    {
        yield return new WaitForSeconds(waitTimeBeforeRotation);
        enemyAlerted = true;
        rotateRight = rotateLeft = 90;
        enemyRotating = false;
        //time before beginning inspection
        agent.SetDestination(position);

       
    }

    Vector3 GetAngleDirection(float angle, bool globalAngle)
    {
        if (!globalAngle)
        {
            angle += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0, Mathf.Cos(angle * Mathf.Deg2Rad));
    }



    void SearchForCat()
    {
        DrawVision();
        catDistance = Vector3.Distance(transform.position, player.position);
        if (catDistance <= viewRadius)
        {
            Vector3 catDir = (player.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, catDir) < viewAngle / 2)
            {
                RaycastHit hit;
                if (Physics.Raycast(raycastOrigin.transform.position, catDir, out hit, catDistance))
                {
                    //Debug.Log("GameOver");
                    if (hit.transform.gameObject.tag == "Player")
                    {
                        // gameOverScreen.GetComponent<GameOver>().toggleGameOverScreen();
                        Debug.Log("Hit Player!");
                    }
                }
            }
        }

    }

    //for coned raycast
    ViewCastInfo ViewCast(float globalAngle)
    {
        Vector3 dir = GetAngleDirection(globalAngle, true);
        RaycastHit hit;

        if (Physics.Raycast(transform.position, dir, out hit, viewRadius))
        {
            GameObject hitObject = hit.transform.gameObject;
            if (hitObject.tag == "Walls" || hitObject.layer == LayerMask.NameToLayer("Furniture"))
                return new ViewCastInfo(true, hit.point, hit.distance, globalAngle);
        }
        return new ViewCastInfo(false, transform.position + dir * viewRadius, viewRadius, globalAngle);
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


    // draws coned raycast
    void DrawVision()
    {
        int rayCount = Mathf.RoundToInt(viewAngle * visionResoulution);
        float rayAngleSize = viewAngle / rayCount;
        List<Vector3> viewPoints = new List<Vector3>();
        for (int i = 0; i <= rayCount; i++)
        {
            float angle = transform.eulerAngles.y - viewAngle / 2 + rayAngleSize * i;
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


}
