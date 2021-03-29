using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class CheckPointManager : MonoBehaviour
{
    #region public componants
    [Header("script comonants")]
    [Tooltip("checkpoint list put here al the checkpoints in")]
    public List<GameObject> checkpoint;
    [Tooltip("the last checkpoint")]
    public GameObject lastCheckPoint;
    [Tooltip("the begin object of the speed run")]
    public GameObject beginObject;
    [Header("event")]
    [Tooltip("here you can put a void and it wil be executed just like a button")]
    public UnityEvent cheatEvent;
    [Header("script values")]
    [Tooltip("max check points")]
    public int maxCheckPoints;
    [Space(5)]
    [Tooltip("this is accessed by a other script")]
    public int index =0;
    public bool isASpeedRun;
    #endregion
    #region check if index = maxcheckpoints
    private void Update()
    {
        if(maxCheckPoints<= index)
        {
            lastCheckPoint.GetComponent<LastCheckPoint>().canBeTriggert = true;
            if (lastCheckPoint.GetComponent<LastCheckPoint>().isSpeedRunPoint)
            {
                lastCheckPoint.GetComponent<LastCheckPoint>().lightOn = true;
            }
        }
        else
        {
            if (checkpoint[index].GetComponent<CheckPoint>().isSpeedRunPoint)
            {
                checkpoint[index].GetComponent<CheckPoint>().lightOn = true;
            }
            checkpoint[index].GetComponent<CheckPoint>().canBeTriggert = true;
        }        
    }
    #endregion
    #region unity event void
    public void Complete()
    {
        if(isASpeedRun)
        { 
            foreach (GameObject checkPoint in checkpoint)
            {
                if (checkPoint.GetComponent<CheckPoint>().isStartSpeedRun)
                {
                    checkPoint.GetComponent<CheckPoint>().lightOn = true;
                }
            }
            beginObject.GetComponent<CheckPoint>().lightOn = true;
        }

        cheatEvent?.Invoke();
    }
    #endregion
}