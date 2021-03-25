using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerRig : MonoBehaviour
{
    public float front_L;
    public float front_R;
    public float back_L;
    public float back_R;

    public GameObject[] transformFront_L;
    public GameObject[] transformFront_R;
    public GameObject[] transformBack_L;
    public GameObject[] transformBack_R;


    public void Rotate()
    {
        Vector3 f_l = new Vector3(0, 0, front_L);
        for (int i = 0; i < 3; i++)
        {
            transformFront_L[i].transform.localEulerAngles = f_l;
        }

        Vector3 f_r = new Vector3(0, 0, front_R);
        for (int i = 0; i < 3; i++)
        {
            transformFront_R[i].transform.localEulerAngles = f_r;
            print("Rotation succesfull");
        }

        Vector3 b_l = new Vector3(0, 0, back_L);
        for (int i = 0; i < 3; i++)
        {
            transformBack_L[i].transform.localEulerAngles = b_l;
        }

        Vector3 b_r = new Vector3(0, 0, back_R);
        for (int i = 0; i < 3; i++)
        {
            transformBack_R[i].transform.localEulerAngles = b_r;
        }

        

    }
}
