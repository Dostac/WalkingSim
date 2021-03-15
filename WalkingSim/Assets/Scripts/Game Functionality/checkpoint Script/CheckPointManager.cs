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
    [Header("event")]
    [Tooltip("here you can put a void and it wil be executed just like a button")]
    public UnityEvent cheatEvent;
    [Header("script values")]
    [Tooltip("max check points")]
    public int maxCheckPoints;
    [Space(5)]
    [Tooltip("this is accessed by a other script")]
    public int index =0;
    #endregion
    #region check if index = maxcheckpoints
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
    #endregion
    #region unity event void
    public void Complete()
    {
        cheatEvent?.Invoke();
    }
    #endregion
}