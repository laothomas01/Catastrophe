// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// //================================== OBSELETE CODE ======================
// public class Follow_Player : MonoBehaviour
// {
//     // Transform of the camera to shake. Grabs the gameObject's transform
// 	// if null.
// 	public Transform camTransform;
	
// 	// How long the object should shake for.
// 	public float shakeDuration = 0f;
	
// 	// Amplitude of the shake. A larger value shakes the camera harder.
// 	public float shakeAmount = 0.7f;
// 	public float decreaseFactor = 1.0f;
	

//     bool canShake;

// 	Vector3 originalPos;

//     [SerializeField]
//     private Transform player;
//     public Vector3 offset;

//     void Awake()
// 	{
// 		if (camTransform == null)
// 		{
// 			camTransform = GetComponent(typeof(Transform)) as Transform;
// 		}
// 	}
//     void OnEnable()
// 	{
// 		originalPos = camTransform.localPosition;
// 	}
//     void Start() {
//         canShake = false;
//         Time.timeScale = 1;
//         Cursor.visible = false;
//     }

//     void Update()
//     {
       
//         follow_player();
        
//         if(canShake)
//         {
//             camera_shake();
            
//         }

//     }
//     public void follow_player()
//     {
//  transform.position = player.transform.position + offset;
//     }
//     public void camera_shake()
//     {
//         //camera shake
//         if (shakeDuration > 0)
// 		{
//             //shake and  shake at its current position
// 			camTransform.localPosition = this.transform.position + Random.insideUnitSphere * shakeAmount;
			
// 			shakeDuration -= Time.deltaTime * decreaseFactor;
// 		}
// 		else
// 		{
// 			shakeDuration = 0.2f;
            
//             // return to current position
// 			camTransform.localPosition = this.transform.position;
//             setCanShake(false);
// 		}
//     }
//     public void setCanShake(bool shake)
//     {
//         this.canShake = shake;
//     }
// }


// // ==================================================