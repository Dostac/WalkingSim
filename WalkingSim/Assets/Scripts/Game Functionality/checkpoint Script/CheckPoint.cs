using UnityEngine;
public class CheckPoint : MonoBehaviour
{
    #region public componants
    [Tooltip("the checkpoint manager")]
    public CheckPointManager cpm;
    [Tooltip("dont touch this in code it checks if it can be triggert")]
    public bool canBeTriggert = false;
    [Space(3)]
    [Tooltip("dont touch this in code it checks if it can be triggert")]
    public bool lightOn;
    public bool isSpeedRunPoint;
    public bool isStartSpeedRun;
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
                lightOn = false;
                canBeTriggert = false;
            }
        }
    }
    #endregion
}