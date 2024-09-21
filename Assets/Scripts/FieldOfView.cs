using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    [SerializeField] private Transform raycastOrigin;
    [SerializeField] private float raycastDistance = 10f;
    [SerializeField] private float raycastAngle = 90f;
    [SerializeField] private float angleIncrement = 5f;
    [SerializeField] private bool drawRay = false;
    private GameObject currentDetectedObject;
    private GameObject previousObject;
    private Color originalColor = Color.white;
    [SerializeField] private LayerMask detectedLayerMask;
    private void Start()
    {
        // Initialize player rotation to face 90 degrees on the Y-axis (right).
        transform.rotation = Quaternion.Euler(0, 90, 0);
    }

    private void Update()
    {
        DetectFurniture(detectedLayerMask);
    }
    private void DetectFurniture(int detectedLayer)
    {
        GameObject previousObject = currentDetectedObject; // Store the previously detected object
        GameObject detectedObject = null;                  // Track the currently detected object

        // Iterate through the angles for the raycasts
        for (float angle = -raycastAngle / 2; angle <= raycastAngle / 2; angle += angleIncrement)
        {
            Vector3 direction = Quaternion.Euler(0, angle, 0) * transform.forward;

            // Perform the raycast
            if (Physics.Raycast(raycastOrigin.position, direction, out RaycastHit hit, raycastDistance))
            {
                // Check if the detected object is in the desired layer
                if ((1 << hit.transform.gameObject.layer & detectedLayer) != 0)
                {
                    if (hit.transform.gameObject.tag == "Heavy")
                    {
                        detectedObject = hit.transform.gameObject;  // Track the first valid object
                        Debug.DrawRay(raycastOrigin.position, direction * raycastDistance, Color.red);
                        break;  // Stop further raycasts once a valid object is found
                    }
                }
                else
                {
                    // If the object is not in the desired layer, skip this ray
                    Debug.DrawRay(raycastOrigin.position, direction * raycastDistance, Color.green);
                    continue;  // Skip to the next ray without doing anything
                }
            }
        }

        // Check if an object was detected
        if (detectedObject != null)
        {
            currentDetectedObject = detectedObject;

            // If a new object is detected, unhighlight the previous object and highlight the new one
            if (currentDetectedObject != previousObject)
            {
                if (previousObject != null)
                {
                    ResetDetectedObjectColor(previousObject);  // Unhighlight the previous object
                }
                if (currentDetectedObject.gameObject.tag == "Heavy")
                {
                    SetDetectedObjectColor(currentDetectedObject, Color.red);  // Highlight the new object
                }
                // else
                // {
                //     SetDetectedObjectColor(currentDetectedObject, Color.blue);  // Highlight the new object
                // }
            }
        }
        else
        {
            // If no object is detected, unhighlight the current object
            if (currentDetectedObject != null)
            {
                ResetDetectedObjectColor(currentDetectedObject);
                currentDetectedObject = null;
            }
        }

    }

    private void ResetDetectedObjectColor(GameObject obj)
    {
        var renderer = obj.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = originalColor;
        }
    }

    private void SetDetectedObjectColor(GameObject obj, Color color)
    {
        var renderer = obj.GetComponent<Renderer>();
        if (renderer != null)
        {
            originalColor = renderer.material.color;
            renderer.material.color = color;
        }
    }
    public GameObject GetCurrentDetectedObject()
    {
        return currentDetectedObject;
    }

}

