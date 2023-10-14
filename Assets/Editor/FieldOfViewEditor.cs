using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// [CustomEditor (typeof (FieldOfView))]
// public class FieldOfViewEditor : Editor
// {
//     void OnSceneGUI()
//     {
//         FieldOfView fw = (FieldOfView)target;
//         Handles.color = Color.white;
//         Handles.DrawWireArc(fw.transform.position, Vector3.up, Vector3.forward, 360, fw.viewRadius);
//         Vector3 viewAngleA = fw.GetAnglesPov(-fw.viewAngle/2, false);
//         Vector3 viewAngleB = fw.GetAnglesPov(fw.viewAngle / 2, false);

//         Handles.DrawLine(fw.transform.position, fw.transform.position + viewAngleA * fw.viewRadius);
//         Handles.DrawLine(fw.transform.position, fw.transform.position + viewAngleB * fw.viewRadius);
//     }
// }
