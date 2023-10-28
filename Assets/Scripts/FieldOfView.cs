using UnityEditor;
using UnityEngine;

/// <summary>
/// handle functionality related to player's field of view
/// </summary>
public class FieldOfView : MonoBehaviour
{
    private Vector3 cursorPosition;
    private Ray screenPointToWorldRay;
    private RaycastHit raycastHit;
    private Vector3 lookDirection;
    public Transform raycastOrigin;
    int playerLayerMask;
    int furnitureLayerMask;
    public float lookDistance;
    //Store the currently detected object
    private GameObject currentDetectedObject;
    private Color originalObjectColor;

    [SerializeField]
    bool drawRayCone = false;
    void Start()
    {
        playerLayerMask = 1 << 7;
        furnitureLayerMask = 1 << 9;
    }
    void Update()
    {
        // HandleSingleRaycastFurnitureDetection();
        // ConedRaycastFurnitureDetection();
        PerformConedRaycast();
    }
    void FixedUpdate()
    {
        HandleLookAtMouseCursor();
    }

    // on screen mouse position converted to world coordinates and casts a ray
    void HandleLookAtMouseCursor()
    {
        screenPointToWorldRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(screenPointToWorldRay, out raycastHit, Mathf.Infinity, ~playerLayerMask))
        {
            cursorPosition.Set(raycastHit.point.x, transform.position.y, raycastHit.point.z);

            transform.LookAt(cursorPosition);
        }

        lookDirection = cursorPosition - transform.position;

        lookDirection = lookDirection.normalized * lookDistance;
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
    - put into fixed update because we want detection to handle at a different frame rate than normal: prevents jank rotation when raycast hits player
    - we will update this with a coned detection for better accuracy 

    */
    void HandleSingleRaycastFurnitureDetection()
    {
        Debug.DrawRay(raycastOrigin.position, lookDirection, Color.red);
        GameObject previouslyDetectedObject = currentDetectedObject;

        //dont think a switch statement is useful here
        //if you raycasted first and did not hit a furniture, do nothing;
        if (Physics.Raycast(raycastOrigin.position, lookDirection, out raycastHit, lookDirection.magnitude, ~furnitureLayerMask))
        {
            return;
        }
        //else if you raycasted to a furniture, set color to red
        else if (Physics.Raycast(raycastOrigin.position, lookDirection, out raycastHit, lookDirection.magnitude, furnitureLayerMask))
        {
            currentDetectedObject = raycastHit.transform.gameObject;
            if (currentDetectedObject != previouslyDetectedObject)
            {
                // //Reset the color of previously detected object
                if (previouslyDetectedObject != null)
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
        //if you raycasted to a funriture and stopped change its color back to original
        else
        {
            //Reset the color of the previosuly detected object
            if (previouslyDetectedObject != null)
            {
                Renderer previousRender = previouslyDetectedObject.GetComponent<Renderer>();
                previousRender.material.color = originalObjectColor;
            }
            //Reset the current detected object
            currentDetectedObject = null;
        }

    }

    void PerformConedRaycast()
    {
        //@TODO: need to make these variables public for unity editor value adjustments
        float coneAngle = 45f; // Adjust as per your needs
        float angleIncrement = 5f;
        GameObject previouslyDetectedObject = currentDetectedObject;

        //TODO: explain this logic
        for (float angle = -coneAngle / 2, angle2 = coneAngle / 2; angle <= coneAngle / 2; angle += angleIncrement, angle2 -= angleIncrement)
        {
            Quaternion rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 rayDirection = rotation * lookDirection;

            // Debug.DrawRay(raycastOrigin.position, rayDirection, Color.red);
            //@TODO: add physics raycast detection
            if(Physics.Raycast(raycastOrigin.position,rayDirection,out raycastHit,lookDistance,~furnitureLayerMask))
            {
                return;
            }
            
            // if (Physics.Raycast(raycastOrigin.position, rayDirection, out hit, maxDistance))
            // {
            //     // Handle raycast hit
            //     Debug.DrawRay(raycastOrigin.position, rayDirection * hit.distance, Color.red);
            // }
            // else
            // {
            //     // Handle no hit
            //     Debug.DrawRay(raycastOrigin.position, rayDirection * maxDistance, Color.green);
            // }
        }
    }


    public Vector3 GetLookDirection()
    {
        return lookDirection;
    }
    public GameObject GetCurrentDetectedObject()
    {
        return currentDetectedObject;
    }

}
