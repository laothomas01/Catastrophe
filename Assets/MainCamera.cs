using Mono.CompilerServices.SymbolWriter;
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

    // public void Shake()
    // {
    //     originalPosition = cameraTransform.localPosition;
    //     currentShakeDuration = shakeDuration;
    // }


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
            cameraTransform.localPosition = transform.position + Random.insideUnitSphere * shakeMagnitude;
            currentShakeDuration -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            cameraTransform.localPosition = transform.position;
            currentShakeDuration = shakeDuration;
            canShake = false;
        }
    }
}
