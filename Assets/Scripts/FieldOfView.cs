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

    private Color originalColor = new Color(1, 1, 1, 1);

    [SerializeField]
    bool drawRayCone = false;

    public float coneAngle = 45f;
    public float angleIncrement = 5f;
    void Start()
    {
        playerLayerMask = 1 << 7;
        furnitureLayerMask = 1 << 9;
    }
    void Update()
    {
        HandleSingleRaycastFurnitureDetection();
        // HandleConedRaycastFurnitureDetection();
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
    void HandleConedRaycastFurnitureDetection()
    {
           
           
    //     for (float angle = -coneAngle / 2, angle2 = coneAngle / 2; angle <= coneAngle / 2; angle += angleIncrement, angle2 -= angleIncrement)
    //     {
    //         Quaternion rotation = Quaternion.Euler(0f, angle, 0f);
    //         Quaternion rotation2 = Quaternion.Euler(0f, angle2, 0f);
    //         Vector3 rayDirection = rotation * directionCatIsFacing;
    //         Vector3 rayDirection2 = rotation2 * directionCatIsFacing;
    //         GameObject previouslyDetectedObject = currentDetectedObject;
    //         Debug.DrawRay(raycastOrigin.position, rayDirection, Color.red);
    //          if (Physics.Raycast(raycastOrigin.position, rayDirection, out raycastHit, lookDistance, ~furnitureLayerMask) &&
    //     Physics.Raycast(raycastOrigin.position, rayDirection2, out raycastHit, lookDistance, ~furnitureLayerMask))
    // {
    //     return;
    // }
    //       else if (Physics.Raycast(raycastOrigin.position, rayDirection, out raycastHit, lookDistance, furnitureLayerMask) ||
    //          Physics.Raycast(raycastOrigin.position, rayDirection2, out raycastHit, lookDistance, furnitureLayerMask))
    //          {
    //             currentDetectedObject = raycastHit.transform.gameObject;

    //     // Change the color of the furniture object
    //     Renderer objectRenderer = currentDetectedObject.GetComponent<Renderer>();
    //     objectRenderer.material.color = Color.red;
    //          }
    //          else{
    //             if(previouslyDetectedObject != null)
    //             {
    //                 Renderer lastRenderer = currentDetectedObject.GetComponent<Renderer>();
    //                 lastRenderer.material.color = originalColor;
    //             }
    //          }       


    //     }
    }

    // void PerformConedRaycast()
    // {
    //     //figure out a way that is readable/logical
    //     for (float angle = -coneAngle / 2, angle2 = coneAngle / 2; angle <= coneAngle / 2; angle += angleIncrement, angle2 -= angleIncrement)
    //     {
    //         Quaternion rotation = Quaternion.Euler(0f, angle, 0f);
    //         Quaternion rotation2 = Quaternion.Euler(0f, angle2, 0f);
    //         Vector3 rayDirection = rotation * directionCatIsFacing;
    //         Vector3 rayDirection2 = rotation2 * directionCatIsFacing;
    //         GameObject previouslyDetectedObject = currentDetectedObject;


    //         /*
            
    //         - want to cache current obj 
    //         - want to cache prev seen obj as curr obj
    //         - 

    //         A = curr obj
    //         B = A
    //         change B's properties while A stores information 
            
    //         */
    //         // if(Physics.Raycast(raycastOrigin.position, rayDirection, out raycastHit, lookDistance, ~furnitureLayerMask) && 
    //         // Physics.Raycast(raycastOrigin.position, rayDirection2, out raycastHit, lookDistance, ~furnitureLayerMask)
    //         // )
    //         // {
    //         //     return;
    //         // }
            
    //         // else 
            
    //         if (Physics.Raycast(raycastOrigin.position, rayDirection, out raycastHit, lookDistance, furnitureLayerMask) ||
    //             Physics.Raycast(raycastOrigin.position, rayDirection2, out raycastHit, lookDistance, furnitureLayerMask))
    //         {
    //             currentDetectedObject = raycastHit.transform.gameObject;
    //             //previouslyDetectedObject = currentDetectedObject
    //             Renderer objectRenderer = currentDetectedObject.GetComponent<Renderer>();
    //             objectRenderer.material.color = Color.red;
                
    //         }
    //         else
    //         {
    //             if(previouslyDetectedObject != null)
    //             {
    //                 Renderer previousRenderer = previouslyDetectedObject.GetComponent<Renderer>();
    //                 previousRenderer.material.color = originalObjectColor;
                    
    //             }
    //             currentDetectedObject = null;
    //         }



    //         // else if (Physics.Raycast(raycastOrigin.position, rayDirection, out raycastHit, lookDistance, furnitureLayerMask) ||
    //         //     Physics.Raycast(raycastOrigin.position, rayDirection2, out raycastHit, lookDistance, furnitureLayerMask))
    //         // {

    //         //     currentDetectedObject = raycastHit.transform.gameObject;
    //         //     //Check if the currently detected object is different from the previously detected object 
    //         //     if(currentDetectedObject != previouslyDetectedObject)
    //         //     {
    //         //         Renderer previousRenderer = previouslyDetectedObject.GetComponent<Renderer>();
    //         //         if(previouslyDetectedObject != null)
    //         //         {
    //         //             previousRenderer.material.color = originalObjectColor;
    //         //         }
    //         //     }

    //         //     //Change the color of the currently hit object 
    //         //     Renderer currentRenderer = currentDetectedObject.GetComponent<Renderer>();
    //         //     currentRenderer.material.color = Color.red;
    //         // }
    //         // if(currentDetectedObject != null)
    //         // {
    //         //     Renderer lastRenderer = currentDetectedObject.GetComponent<Renderer>();
    //         //     lastRenderer.material.color = originalObjectColor;
    //         // }
            
    //     }

        
    // }


    public Vector3 GetLookDirection()
    {
        return directionCatIsFacing;
    }
    public GameObject GetCurrentDetectedObject()
    {
        return currentDetectedObject;
    }

}
