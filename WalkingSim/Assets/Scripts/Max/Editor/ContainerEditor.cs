using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ContainerRig))]
public class ContainerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ContainerRig myScript = (ContainerRig)target;
        if (GUILayout.Button("UpdateRotation"))
        {
            myScript.Rotate();
        }
    }
}
