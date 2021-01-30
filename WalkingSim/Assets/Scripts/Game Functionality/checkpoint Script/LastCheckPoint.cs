using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LastCheckPoint : MonoBehaviour
{
    //public
    public CheckPointManager cpm;
    public bool canBeTriggert = false;
    //private
    private bool achieved;
    private void OnTriggerEnter(Collider other)
    {
        if (canBeTriggert)
        {
            if (!achieved && other.gameObject.tag == "Player")
            {
                cpm.Complete();
                achieved = true;
            }
        }
    }
}