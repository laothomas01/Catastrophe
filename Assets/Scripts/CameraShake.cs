using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    // Start is called before the first frame update
    Transform camTransform;
    
	public float shakeDuration = 0f;
	
	// Amplitude of the shake.
    // A larger value shakes the camera harder.
	public float shakeAmount = 0f;

    public float decreaseFactor = 0f;
    void Start()
    {
        camTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    public void shake()
    {
        Debug.Log("Shake!");
        //  if (shakeDuration > 0)
        //  {
        //     camTransform.localPosition = this.transform.position + Random.insideUnitSphere * shakeAmount;
        //     shakeDuration -= Time.deltaTime * decreaseFactor;
        //  }
        //  else
        //  {
        //     shakeDuration = 0.2f;
        //     camTransform.localPosition = this.transform.position;
        //  }


    }
}
