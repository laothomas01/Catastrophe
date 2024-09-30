using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [SerializeField] private float cameraShakeDuration = 0.2f;
    [SerializeField] private float cameraShakeMagnitude = 0.3f;

    private Transform cameraTransform;
    [SerializeField] private float currentCameraShakeDuration = 0;
    public float cameraShakeDecreaseFactor = 2f;
    private bool canShake = false;

    Transform player;
    [SerializeField]
    private Vector3 followPlayerOffset;

    private void Awake()
    {
        player = GameObject.Find("Cat").GetComponent<Transform>();
        cameraTransform = GetComponent<Transform>();
        currentCameraShakeDuration = cameraShakeDuration;
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
        if (currentCameraShakeDuration > 0)
        {
            //shake magnitude is how rough the camera rumbles, how far the camera is shifted from it current position
            // we add that offset position to camera'a current position

            //position + point inside sphere * offset distance
            cameraTransform.localPosition = transform.position + Random.insideUnitSphere * cameraShakeMagnitude;
            currentCameraShakeDuration -= Time.deltaTime * cameraShakeDecreaseFactor;
        }
        else
        {
            //reset camera
            cameraTransform.localPosition = transform.position;
            currentCameraShakeDuration = cameraShakeDuration;
            canShake = false;
        }
    }
}
