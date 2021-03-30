using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MaterialChanger))]
public class MaterialEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        MaterialChanger myScript = (MaterialChanger)target;
        if (GUILayout.Button("UpdateMaterials"))
        {
            myScript.UpdateMaterial();
        }
    }
}