using UnityEngine;
public class VaultWall : MonoBehaviour
{
    #region public componants
    [Header("for other script")]
    [Tooltip("the destentation wall detection wil get and then playermovement")]
    public Transform destenation;
    [Header("Pivot Transforms")]
    [Tooltip("begin transform position check if it is the right position ")]
    public Transform beginPivot;
    [Tooltip("end transform position check if it is the right position ")]
    public Transform endPivot;
    #endregion
    #region private componants
    private GameObject player;
    #endregion
    #region checking for player posistion
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        destenation = beginPivot;
    }
    private void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 playerVec = player.transform.position - transform.position;
        if (Vector3.Dot(forward, playerVec) < 0)
        {
            destenation = endPivot;
        }
        if (Vector3.Dot(forward, playerVec) > 0)
        {
            destenation = beginPivot;
        }
    }
    #endregion
}