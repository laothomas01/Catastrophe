using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furniture : MonoBehaviour
{
    /// <summary>
    /// - handle properties of the furniture
    /// - handle furniture destruction
    /// </summary>
    // Start is called before the first frame update

    CameraShake cam;
    void Start()
    {
        cam = Camera.main.GetComponent<CameraShake>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// purpose: handle event of furniture destruction
    /// return: void
    /// </summary>
    private void OnDestroy() {
        //add cam shake 
                // cam.shake();

        //add particle effect
            if(this.tag == "Heavy")
            {
                Score score = FindObjectOfType<Score>();
                score.decrementCurrentHeavyFurnitureCount();
            }
    }
}