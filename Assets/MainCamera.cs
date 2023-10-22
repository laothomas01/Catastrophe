using UnityEngine;

public class MainCamera : MonoBehaviour
{
    //============ camera shake members ======
    [SerializeField] private float shakeDuration = 0.2f; // Duration of shake effect;
    [SerializeField] private float shakeMagnitude = 0.3f; // Magnitude of the shake effect

    private Transform cameraTransform;
    [SerializeField] private float currentShakeDuration = 0;
    public float decreaseFactor = 2f; // Rate at which the shake effect decreases over time
    private bool canShake = false;
    //=========================================

    //============= camera follow members ======

    Transform player;
    [SerializeField]
    private Vector3 followPlayerOffset;

    //==========================================

    private void Awake()
    {
        player = GameObject.Find("Cat").GetComponent<Transform>();
        cameraTransform = GetComponent<Transform>();
        currentShakeDuration = shakeDuration;
    }

    private void FollowPlayer()
    {
        transform.position = player.position + followPlayerOffset;
    }
    private void Update()
    {


        FollowPlayer();

        if (canShake)
        {
            Shake();
        }

    }
    public void CanShake(bool shake)
    {
        canShake = shake;
    }

    public void Shake()
    {
        Debug.Log("Camera Shake!");
        if (currentShakeDuration > 0)
        {
            //shake magnitude is how rough the camera rumbles, how far the camera is shifted from it current position
            // we add that offset position to camera'a current position

            //position + point inside sphere * offset distance
            cameraTransform.localPosition = transform.position + Random.insideUnitSphere * shakeMagnitude;
            currentShakeDuration -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            //reset camera
            cameraTransform.localPosition = transform.position;
            currentShakeDuration = shakeDuration;
            canShake = false;
        }
    }
}
