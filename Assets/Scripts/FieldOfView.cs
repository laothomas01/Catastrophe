using UnityEngine;
using UnityEngine.InputSystem;

public class FieldOfView : MonoBehaviour
{
    [SerializeField] private Transform raycastOrigin;
    [SerializeField] private float lookDistance = 10f;
    [SerializeField] private float coneAngle = 90f;
    [SerializeField] private float angleIncrement = 5f;
    [SerializeField] private bool drawRay = false;
    private GameObject currentDetectedObject;
    private Color originalColor = Color.white;
    private int playerLayerMask;
    private int furnitureLayerMask;
    private DeviceManager.PlatformType currentPlatform;
    PlayerInput leftJoyStick;
    public float rotationSpeed = 1f;

    private void Start()
    {
        // Initialize player rotation to face 90 degrees on the Y-axis (right).
        transform.rotation = Quaternion.Euler(0, 90, 0);
        playerLayerMask = LayerMask.GetMask("Player");
        furnitureLayerMask = LayerMask.GetMask("Furniture");
        leftJoyStick = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        DetectFurnitureInCone();
    }

    private void FixedUpdate()
    {
        HandleCurrentPlatformControls();
    }

    private void LookAtMouseCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, ~playerLayerMask))
        {
            Vector3 targetPosition = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            transform.LookAt(targetPosition);
        }
    }

    private void DetectFurnitureInCone()
    {
        GameObject previousObject = currentDetectedObject;
        bool foundObject = false;

        for (float angle = -coneAngle / 2; angle <= coneAngle / 2; angle += angleIncrement)
        {
            Vector3 direction = Quaternion.Euler(0, angle, 0) * transform.forward;
            if (drawRay)
            {
                Debug.DrawRay(raycastOrigin.position, direction * lookDistance, Color.red);
            }

            if (Physics.Raycast(raycastOrigin.position, direction, out RaycastHit hit, lookDistance, furnitureLayerMask))
            {
                foundObject = true;
                currentDetectedObject = hit.transform.gameObject;
                HandleObjectDetection(previousObject);
                break;
            }
        }

        if (!foundObject && currentDetectedObject != null)
        {
            ResetObjectColor(currentDetectedObject);
            currentDetectedObject = null;
        }
    }

    private void HandleObjectDetection(GameObject previousObject)
    {
        if (currentDetectedObject != previousObject)
        {
            if (previousObject != null)
            {
                ResetObjectColor(previousObject);
            }
            SetObjectColor(currentDetectedObject, Color.red);
        }
    }

    private void ResetObjectColor(GameObject obj)
    {
        var renderer = obj.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = originalColor;
        }
    }

    private void SetObjectColor(GameObject obj, Color color)
    {
        var renderer = obj.GetComponent<Renderer>();
        if (renderer != null)
        {
            originalColor = renderer.material.color;
            renderer.material.color = color;
        }
    }

    public GameObject GetCurrentDetectedObject() => currentDetectedObject;
    public void SetPlatformControls(DeviceManager.PlatformType platform)
    {
        currentPlatform = platform;
    }

    void HandlePcControls()
    {
        LookAtMouseCursor();
    }

    //for mobile controls, joystick rotation handles player's face direction 
    //controls inspired by ocarina of time :) 
    void HandleMobileControls()
    {
        // Read the joystick input
        Vector2 leftStick = leftJoyStick.actions["Move"].ReadValue<Vector2>();
        if(leftStick.magnitude > 0.1f)
        {
            float targetAngle = Mathf.Atan2(leftStick.x, leftStick.y) * Mathf.Rad2Deg;
            // rotate around the y axis 
            Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
    
    void HandleCurrentPlatformControls()
    {
        switch(currentPlatform)
        {
            case DeviceManager.PlatformType.PC:
                // HandlePcControls();
                HandleMobileControls(); // placed here for testing purposes 
                break;
            case DeviceManager.PlatformType.Mobile:
                HandleMobileControls();
                break;
            default:
                throw new System.Exception("Current device type not found");
        }
    }
}

