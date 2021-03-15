using UnityEngine;
public class CheckPoint : MonoBehaviour
{
    #region public componants
    [Tooltip("the checkpoint manager")]
    public CheckPointManager cpm;
    [Tooltip("dont touch this in code it checks if it can be triggert")]
    public bool canBeTriggert = false;
    #endregion
    #region private componants
    private bool achieved;
    #endregion
    #region collision
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
    #endregion
}