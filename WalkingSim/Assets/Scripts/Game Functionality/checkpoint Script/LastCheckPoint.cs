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
    #endregion
    #region private componants
    private bool achieved;
    private void Update()
    {
        if (!lightOn)
        {
            gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
        else
        {
            gameObject.GetComponent<MeshRenderer>().enabled = true;
        }
    }
    #endregion
    #region colission
    private void OnTriggerEnter(Collider other)
    {
        if (canBeTriggert)
        {
            if (!achieved && other.gameObject.CompareTag ("Player"))
            {
                cpm.Complete();
                achieved = true;
                lightOn = false;
            }
        }
    }
    #endregion
}