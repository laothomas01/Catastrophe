using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{

    
    private Vector3 cursorPosition;
    private Ray screenPointToWorldRay;
    private RaycastHit raycastHit;
    private Vector3 lookDirection;

    //starting position of the raycast
    public Transform raycastOrigin;

    int playerLayerMask;
    int furnitureLayerMask;
    
    public float lookDistance;

//========= attributes used for wide coned raycast =============    
    public float fieldOfViewAngle;
    public float fieldOfViewMeshResolution;

    private float raycastAngle;
    private int rayCount;
    // ==========================================
    
    //Store the currently detected object
    private GameObject currentDetectedObject;
    private Color originalObjectColor;
    void Start()
    {
        playerLayerMask = 1 << 7;
        furnitureLayerMask = 1 << 9;
    }
    void Update()
    {
        handleSingleRaycastFurnitureDetection();
    }
    void FixedUpdate()
    {
        handleLookAtMouseCursor();
    }

    /*
    
    - take on screen mouse cursor position and cast a ray into the x-z plane form that position
    - rotate player's forward vector in direction of mouse cursor
    
    */
    void handleLookAtMouseCursor()
    {
        screenPointToWorldRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(screenPointToWorldRay,out raycastHit,Mathf.Infinity,~playerLayerMask))
        {
            cursorPosition.Set(raycastHit.point.x,transform.position.y,raycastHit.point.z);

            transform.LookAt(cursorPosition);
        }
    }

    /*
    
    - calculate look direction based on cursor position and player's current position
    - set look direction's magniture to a set look distance
    - draw ray for debugging purposes
    - cache a detected furniture
    - set currently seen furniture's color as red if it's been detected
    - cache that furniture's original render material color
    - set undetected furniture's render material color back to original color
    - this furniture detection function uses a single raycast. 
    */
    void handleSingleRaycastFurnitureDetection()
    {
        lookDirection = cursorPosition - transform.position;

        lookDirection = lookDirection.normalized * lookDistance;
        
        // lookDirection = lookDirection.normalized * lookDistance;
        Debug.DrawRay(raycastOrigin.position,lookDirection,Color.red);

        
        GameObject previouslyDetectedObject = currentDetectedObject;
        if(Physics.Raycast(raycastOrigin.position,lookDirection,out raycastHit,lookDirection.magnitude,furnitureLayerMask))
        {
            currentDetectedObject = raycastHit.transform.gameObject;
            if(currentDetectedObject != previouslyDetectedObject)
            {
                // //Reset the color of previously detected object
                if(previouslyDetectedObject != null)
                {
                    Renderer previousRenderer = previouslyDetectedObject.GetComponent<Renderer>();
                    previousRenderer.material.color = originalObjectColor;
                }
                //Change the color of the newly detected object
                Renderer newRenderer = currentDetectedObject.GetComponent<Renderer>();
                originalObjectColor = newRenderer.material.color;
                newRenderer.material.color = Color.red;
            }
        }
        else
        {
            //Reset the color of the previosuly detected object
            if(previouslyDetectedObject != null)
            {
                Renderer previousRender = previouslyDetectedObject.GetComponent<Renderer>();
                previousRender.material.color = originalObjectColor;
            }
            //Reset the current detected object
            currentDetectedObject = null;
        }

    }

    //coned ray casted hit detection

    // void handleWideFieldOfView()
    // {
    //     rayCount = (int)( fieldOfViewAngle * fieldOfViewMeshResolution);
    //     raycastAngle = fieldOfViewAngle / rayCount;
    //     for(int i = 0; i < rayCount; i++)
    //     {

    //     }
    // }
    // public float viewAngle;
    // public float viewRadius;
    // public Transform cat;

    // public Vector3 GetAnglesPov(float angle, bool globalAngle)
    // {
    //     if (!globalAngle)
    //     {
    //         angle += transform.eulerAngles.y;
    //     }
    //     return new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0, Mathf.Cos(angle * Mathf.Deg2Rad));
    // }

}
