using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    /*
     * [X] Patrol patterns (list of positions that the enemy patrols in)
     * [X] Look around the room for a set amount of seconds and then go back
     * [X] If the Cat is spotted, game over
     * [] Add animations
     * [] Add sounds
     */
    // Start is called before the first frame update
    public Transform cat;
    NavMeshAgent agent;
    public float lerpSpeed,rotateTime,rotateAmount, playerDetectDistance;
    private float rotateRight, rotateLeft, timer, rotateTimer;
    private bool rotating=false,triggered=false;
    public Transform face,lookPoint;
    public GameObject gameOverScreen;
    private Quaternion currentRotation;
    public Vector3 faceAngles;



    Animator animator;

    //patrolling
    public Vector3[] patrolPoints;
    public float waitBetweenPatrol;
    private float patrolWaitTimer;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        patrolWaitTimer = waitBetweenPatrol;
        rotateTimer = 0;
        rotateRight = rotateAmount;
        rotateLeft = rotateAmount;
        agent = GetComponent<NavMeshAgent>();
        Quaternion currentRotation = transform.rotation;

    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(face.position, new Vector3(face.forward.x + faceAngles.x,face.forward.y + faceAngles.y, face.forward.z + faceAngles.z) *playerDetectDistance);
        if (Input.GetKey(KeyCode.Space))
        {
            LookAround();
        }

        if (AgentReachedDestination(agent))
        {
            LookAround();
        }
        animator.SetBool("isWalking", agent.velocity.magnitude > 0);
        Patrol();
    }

    public void InspectFurniture(Transform furniture)
    {
        triggered = true;
        rotateRight = rotateLeft = 90;
        rotating = false;
        
        agent.SetDestination(furniture.position);
       
    }

    bool AgentReachedDestination(NavMeshAgent a)
    {
        if (triggered && !a.pathPending)
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
        if (patrolPoints.Length > 0 && !triggered && patrolWaitTimer >= waitBetweenPatrol )
        {
            int patrolPoint = Random.Range(0, patrolPoints.Length);
            agent.SetDestination(patrolPoints[patrolPoint]);
            patrolWaitTimer = 0;
        }
            patrolWaitTimer += Time.deltaTime;
        
    }

    void LookAround()
    {
        if (!rotating)
        {
            //Debug.Log("inside the rotating condition" + currentRotation + " rotate Right: "+rotateRight);
            //making sure the angle stays between 0 and 360. If over, subtract 360. If under, add the negative number to 360
            rotateRight = currentRotation.eulerAngles.y + rotateRight > 360 ? (currentRotation.eulerAngles.y + rotateRight) - 360 : currentRotation.eulerAngles.y + rotateRight;
            rotateLeft = currentRotation.eulerAngles.y - rotateLeft < 0 ? 360 + (currentRotation.eulerAngles.y - rotateLeft) : currentRotation.eulerAngles.y - rotateLeft;
            rotateAmount = rotateRight;
            rotating = true;
            //Debug.Log("rotate right: " + rotateRight + " rotate left " + rotateLeft + " rotateAmount: " + rotateAmount);
        }
        else
        {
            currentRotation = transform.rotation;
            transform.rotation = Quaternion.RotateTowards(currentRotation, Quaternion.Euler(0, rotateAmount, 0), timer);
            timer += lerpSpeed * Time.deltaTime;
            if (Mathf.Abs((int)transform.eulerAngles.y) == Mathf.Abs((int)rotateAmount))
            {
                //invert the rotation
                rotateAmount = rotateAmount == rotateRight ? rotateLeft : rotateRight;
                timer = 0;
                rotateTimer++;
                if (rotateTimer >= rotateTime)
                {
                    rotateRight = rotateLeft = 90;
                    triggered = false;
                    rotateTimer = 0;
                    rotating = false;
                }
            }
        }

    }

    private void FixedUpdate()
    {
        RaycastHit hit,hit2;
        int layerMask = 1 << 8;
        Debug.DrawRay(lookPoint.position, lookPoint.forward * playerDetectDistance);
        if (Physics.Raycast(lookPoint.position, lookPoint.forward, out hit, playerDetectDistance, ~layerMask))
        {
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                gameOverScreen.SetActive(true);
            }

        }
        if (Physics.Raycast(face.position, new Vector3(face.forward.x + faceAngles.x, face.forward.y + faceAngles.y, face.forward.z + faceAngles.z), out hit2, playerDetectDistance, ~layerMask))
        {
 
            if (hit2.transform.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                gameOverScreen.SetActive(true);
            }

        }
    }


}
