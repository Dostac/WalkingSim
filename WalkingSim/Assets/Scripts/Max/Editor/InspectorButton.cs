using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FencePlacer))]
public class InspectorButton : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        FencePlacer myScript = (FencePlacer)target;
        if(GUILayout.Button("Place next"))
        {
            myScript.PlaceFence();
        }
    }
}
