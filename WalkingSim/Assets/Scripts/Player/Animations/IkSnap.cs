using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IkSnap : MonoBehaviour
{
    public bool useIK;
    public bool leftHandIk, rightHandIk;
    public GameObject mainObject;

    private Animator anim;

    public Vector3 leftHandPos, rightHandPos;
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    void FixedUpdate()
    {
        RaycastHit Lhit;
        RaycastHit Rhit;

        if (Physics.Raycast(transform.position + new Vector3(0, 2, 0.5f), -transform.up + new Vector3(-0.5f, 0, 0), out Lhit, 1f))
        {
            leftHandIk = true;
        }
        else
        {
            leftHandIk = false;
        }
        if (Physics.Raycast(transform.position + new Vector3(0, 2, 0.5f), -transform.up + new Vector3(0.5f, 0, 0), out Rhit, 1f))
        {
            rightHandIk = true;
        }
        else
        {
            rightHandIk = false;
        }
    }

    private void Update()
    {
        Debug.DrawRay(mainObject.transform.position + new Vector3(0, 2, 0.5f), -mainObject.transform.up + new Vector3(-0.5f, 0, 0), Color.blue);
        Debug.DrawRay(mainObject.transform.position + new Vector3(0, 2, 0.5f), -mainObject.transform.up + new Vector3(0.5f, 0, 0), Color.green);
    }
}