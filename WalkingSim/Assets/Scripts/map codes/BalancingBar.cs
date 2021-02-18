using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalancingBar : MonoBehaviour
{
    [Header("for other script")]
    public Transform destenation;
    [Header("Pivot Transforms")]
    public Transform beginPivot;
    public Transform endPivot;

    private GameObject player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        destenation = beginPivot;
    }
    private void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 playerVec = player.transform.position - transform.position;
        if (Vector3.Dot(forward, playerVec) < 0)
        {
            destenation = endPivot;
        }
        if (Vector3.Dot(forward, playerVec) > 0)
        {
            destenation = beginPivot;
        }
    }
}
