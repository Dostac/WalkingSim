using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Hops : MonoBehaviour
{
    [Header("gets filled automaticly place in hierachy from begin til end")]
    public Transform[] hopPositions;
    [Header("other componants")]
    public Transform destenation;
    public bool begin;
    public bool end;
    public int index;
    private void Start()
    {
        hopPositions = gameObject.GetComponentsInChildren<Transform>();
    }
    private void Update()
    {
        destenation = hopPositions[index];
    }
}