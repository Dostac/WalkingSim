using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class CheckPointManager : MonoBehaviour
{
    //public
    public List<GameObject> checkpoint;
    public GameObject lastCheckPoint;
    public UnityEvent cheatEvent;
    public int maxCheckPoints;
    public int index =0;
    private void Update()
    {
        if(maxCheckPoints<= index)
        {
            lastCheckPoint.GetComponent<LastCheckPoint>().canBeTriggert = true;
        }
        else
        {
            checkpoint[index].GetComponent<CheckPoint>().canBeTriggert = true;
        }        
    }
    public void Complete()
    {
        cheatEvent?.Invoke();
    }
}