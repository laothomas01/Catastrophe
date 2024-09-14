using UnityEngine;

/*
    - generic field of view component
    - specify a layer mask to detect
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
    // private int playerLayerMask;
    [SerializeField] private LayerMask detectedLayerMask;
    // private int furnitureLayerMask; // maybe allow this to be a furniture layer the developer can specify in the inspector 
    // private DeviceManager.PlatformType currentPlatform;
    // PlayerMovement playerMovement;
    // public float rotationSpeed = 1f;
    private void Start()
    {
        // Initialize player rotation to face 90 degrees on the Y-axis (right).
        transform.rotation = Quaternion.Euler(0, 90, 0);
        // playerLayerMask = LayerMask.GetMask("Player");
        // playerMovement = GetComponent<PlayerMovement>();

    }

    private void Update()
    {
        DetectFurniture(detectedLayerMask);
    }

    private void FixedUpdate()
    {
        // if (currentPlatform == DeviceManager.PlatformType.PC)
        // {
        //     LookAtMouseCursor();
        // }
    }


    // player rotation pc mouse controls 
    // private void LookAtMouseCursor()
    // {
    //     Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //     if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, ~playerLayerMask))
    //     {
    //         Vector3 targetPosition = new Vector3(hit.point.x, transform.position.y, hit.point.z);
    //         transform.LookAt(targetPosition);
    //     }
    // }

    //universal function 

    //field of view is a cone based ray cast for detection
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

