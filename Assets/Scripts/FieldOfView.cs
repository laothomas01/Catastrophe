using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FieldOfView : MonoBehaviour
{

    public List<Transform> visibleTargets = new List<Transform>();
    public float viewAngle;
    public float viewRadius;
    public float meshResolution;

// offset by angle + point in specific direction
    public Vector3 DirFromAngle(float angle, bool globalAngle)
    {
        if (!globalAngle)
        {
            angle += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0, Mathf.Cos(angle * Mathf.Deg2Rad));
    }
    public float getViewAngle()
    {
        return viewAngle;
    }
    public float getViewRadius()
    {
        return viewRadius;
    }
    public float getMeshResolution()
    {
        return meshResolution;
    }

}
