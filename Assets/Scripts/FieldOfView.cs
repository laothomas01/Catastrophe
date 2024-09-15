using UnityEngine;

/*
    - generic field of view component
    - specify a layer mask to detect
    - reusable for any other game that needs a field of view 
*/
public class FieldOfView : MonoBehaviour
{
    [SerializeField] private Transform raycastOrigin;
    [SerializeField] private float raycastDistance = 10f;
    [SerializeField] private float raycastAngle = 90f;
    [SerializeField] private float angleIncrement = 5f;
    [SerializeField] private bool drawRay = false;
    private GameObject currentDetectedObject;
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
        GameObject previousObject = currentDetectedObject;
        bool foundObject = false;

        for (float angle = -raycastAngle / 2; angle <= raycastAngle / 2; angle += angleIncrement)
        {
            Vector3 direction = Quaternion.Euler(0, angle, 0) * transform.forward;
            if (drawRay)
            {
                Debug.DrawRay(raycastOrigin.position, direction * raycastDistance, Color.red);
            }

            // @TODO need to check of current ray is casting at a wall and if so skip that current ray 
            if (Physics.Raycast(raycastOrigin.position, direction, out RaycastHit hit, raycastDistance, detectedLayer))
            {
                foundObject = true;
                currentDetectedObject = hit.transform.gameObject;
                if (currentDetectedObject != previousObject)
                {
                    if (previousObject != null)
                    {
                        ResetDetectedObjectColor(previousObject);
                    }
                    SetDetectedObjectColor(currentDetectedObject, Color.red);
                }
                break;
            }
        }

        if (!foundObject && currentDetectedObject != null)
        {
            ResetDetectedObjectColor(currentDetectedObject);
            currentDetectedObject = null;
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

