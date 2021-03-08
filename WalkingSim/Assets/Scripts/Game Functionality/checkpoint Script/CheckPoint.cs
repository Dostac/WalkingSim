using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CheckPoint : MonoBehaviour
{ 
    //public
    public CheckPointManager cpm;
    public bool canBeTriggert=false;
    //private
    private bool achieved;
    public void Add()
    {
        cpm.index++;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (canBeTriggert)
        {
            if (!achieved && other.gameObject.CompareTag("Player"))
            {
                Add();
                achieved = false;
            }
        }
    }
}