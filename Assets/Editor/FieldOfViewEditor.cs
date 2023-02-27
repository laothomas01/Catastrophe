using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof (FieldOfView))]
public class FieldOfViewEditor : Editor
{
    void OnSceneGUI()
    {
        FieldOfView fw = (FieldOfView)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fw.transform.position, Vector3.up, Vector3.forward, 360, fw.viewRadius);
    }
}
