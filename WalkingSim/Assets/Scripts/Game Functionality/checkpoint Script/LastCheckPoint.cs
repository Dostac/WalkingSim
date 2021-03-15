using UnityEngine;
public class LastCheckPoint : MonoBehaviour
{
    #region public componants
    [Tooltip("checkpointmanager")]
    public CheckPointManager cpm;
    [Space(5)]
    [Tooltip("a bool that gets checked ingame")]
    public bool canBeTriggert = false;   
    #endregion
    #region private componants
    private bool achieved;
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
            }
        }
    }
    #endregion
}