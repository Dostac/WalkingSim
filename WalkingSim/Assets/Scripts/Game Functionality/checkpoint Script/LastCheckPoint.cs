using UnityEngine;
public class LastCheckPoint : MonoBehaviour
{
    #region public componants
    [Tooltip("checkpointmanager")]
    public CheckPointManager cpm;
    [Space(5)]
    [Tooltip("a bool that gets checked ingame")]
    public bool canBeTriggert = false;
    [Space(3)]
    [Tooltip("a bool that gets checked ingame")]
    public bool lightOn;
    public bool isSpeedRunPoint;
    #endregion
    #region private componants
    private bool achieved;
    #endregion
    #region fucntions
    public void Update()
    {
        if (lightOn)
        {
            gameObject.GetComponent<MeshRenderer>().enabled = true;
        }
        else
        {
            gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (canBeTriggert)
        {
            if (!achieved && other.gameObject.CompareTag ("Player")&&cpm.index>=cpm.checkpoint.Count)
            {
                cpm.Complete();
                cpm.index = 0;
                achieved = false;
                lightOn = false;
                canBeTriggert = false;
            }
        }
    }
    #endregion
}