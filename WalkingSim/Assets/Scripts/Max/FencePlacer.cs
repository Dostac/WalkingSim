using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class FencePlacer : MonoBehaviour
{
    public GameObject fence;
    public float distance;

    public void PlaceFence()
    {
        Instantiate(fence,transform.position + transform.TransformDirection(Vector3.left*distance), 
            transform.rotation);
        

    }
}
