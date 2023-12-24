using UnityEngine;

/// <summary>
/// handle functionality related to player's field of view
/// </summary>
public class FieldOfView : MonoBehaviour
{
    private Vector3 mouseCursorScreenPosition;
    private Ray screenPointToWorldRaycast;
    private RaycastHit raycastHit;
    private Vector3 directionCatIsFacing;
    public Transform raycastOrigin;
    int playerLayerMask;
    int furnitureLayerMask;
    public float lookDistance;

    //globally store currently detected object
    private GameObject currentDetectedObject = null;
    private GameObject prevDetectedObject = null;

    private Color originalColor = new Color(1, 1, 1, 1);

    [SerializeField]
    bool drawRay = false;

    public float coneAngle = 90f;
    public float angleIncrement = 5f;

    void Start()
    {
        playerLayerMask = 1 << 7;
        furnitureLayerMask = 1 << 9;
    }
    void Update()
    {
        // HandleSingleRaycastFurnitureDetection();
        HandleConedRaycastFurnitureDetection();
        // PerformConedRaycast();
    }
    void FixedUpdate()
    {
        HandleLookAtMouseCursor();
    }

    // on screen mouse position converted to world coordinates and casts a ray
    void HandleLookAtMouseCursor()
    {
        screenPointToWorldRaycast = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(screenPointToWorldRaycast, out raycastHit, Mathf.Infinity, ~playerLayerMask))
        {
            mouseCursorScreenPosition.Set(raycastHit.point.x, transform.position.y, raycastHit.point.z);

            transform.LookAt(mouseCursorScreenPosition);
        }

        directionCatIsFacing = mouseCursorScreenPosition - transform.position;

        directionCatIsFacing = directionCatIsFacing.normalized * lookDistance;
    }


    // /*

    // - calculate look direction based on cursor position and player's current position
    // - set look direction's magniture to a set look distance
    // - draw ray for debugging purposes
    // - cache a detected furniture
    // - set currently seen furniture's color as red if it's been detected
    // - cache that furniture's original render material color
    // - set undetected furniture's render material color back to original color
    // - this furniture detection function uses a single raycast. 
    // - put into fixed update because we want detection to handle at a different frame rate than normal: prevents jank rotation when raycast hits player
    // - we will update this with a coned detection for better accuracy 



    //will temporarily will use this until i can figure out the coned raycast 

    // */
    void HandleSingleRaycastFurnitureDetection()
    {
        Debug.DrawRay(raycastOrigin.position, directionCatIsFacing, Color.red);
        GameObject previouslyDetectedObject = currentDetectedObject;

        //dont think a switch statement is useful here
        //if you raycasted first and did not hit a furniture, do nothing;
        if (Physics.Raycast(raycastOrigin.position, directionCatIsFacing, out raycastHit, directionCatIsFacing.magnitude, ~furnitureLayerMask))
        {
            return;
        }
        //else if you raycasted to a furniture, set color to red
        else if (Physics.Raycast(raycastOrigin.position, directionCatIsFacing, out raycastHit, directionCatIsFacing.magnitude, furnitureLayerMask))
        {
            currentDetectedObject = raycastHit.transform.gameObject;
            if (currentDetectedObject != previouslyDetectedObject)
            {
                // //Reset the color of previously detected object
                if (previouslyDetectedObject != null)
                {
                    Renderer previousRenderer = previouslyDetectedObject.GetComponent<Renderer>();
                    previousRenderer.material.color = originalColor;
                }
                //Change the color of the newly detected object
                Renderer newRenderer = currentDetectedObject.GetComponent<Renderer>();
                originalColor = newRenderer.material.color;
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
                previousRender.material.color = originalColor;
            }
            //Reset the current detected object
            currentDetectedObject = null;
        }

    }

    /*
    


    */
    void HandleConedRaycastFurnitureDetection()
    {
        GameObject previouslyDetectedObject = currentDetectedObject;
        bool isHitting = false; // Flag to track if any raycast is hitting

        for (float angle = 0, angle2 = 0; angle <= coneAngle; angle += angleIncrement, angle2 -= angleIncrement)
        {
            Quaternion rotation = Quaternion.Euler(0f, angle, 0f);
            Quaternion rotation2 = Quaternion.Euler(0f, angle2, 0f);
            Vector3 rayDirection1 = rotation * directionCatIsFacing;
            Vector3 rayDirection2 = rotation2 * directionCatIsFacing;

            if (drawRay)
            {
                Debug.DrawRay(raycastOrigin.position, rayDirection1, Color.red);
                Debug.DrawRay(raycastOrigin.position, rayDirection2, Color.red);
            }

            if (Physics.Raycast(raycastOrigin.position, rayDirection1, out raycastHit, lookDistance, furnitureLayerMask) ||
                Physics.Raycast(raycastOrigin.position, rayDirection2, out raycastHit, lookDistance, furnitureLayerMask))
            {
                isHitting = true;
                currentDetectedObject = raycastHit.transform.gameObject;

                if (currentDetectedObject != previouslyDetectedObject)
                {
                    // Set color of previously detected furniture back to normal
                    if (previouslyDetectedObject != null)
                    {
                        previouslyDetectedObject.GetComponent<Renderer>().material.color = originalColor;
                    }

                    // Set color of current detected furniture to red
                    currentDetectedObject.GetComponent<Renderer>().material.color = Color.red;
                    previouslyDetectedObject = currentDetectedObject;
                }
            }
        }

        if (!isHitting)
        {
            // Set color of previously detected furniture back to normal
            if (previouslyDetectedObject != null)
            {
                previouslyDetectedObject.GetComponent<Renderer>().material.color = Color.white;
                previouslyDetectedObject = null;
            }

            Debug.Log("Not Hitting");
        }
    }

    public Vector3 GetLookDirection()
    {
        return directionCatIsFacing;
    }
    public GameObject GetCurrentDetectedObject()
    {
        return currentDetectedObject;
    }

}
