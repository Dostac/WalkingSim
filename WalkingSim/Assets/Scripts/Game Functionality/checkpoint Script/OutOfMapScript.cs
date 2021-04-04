using UnityEngine;
using TMPro;
public class OutOfMapScript : MonoBehaviour
{
    #region private componants
    private OutMapManager omm;
    #endregion
    #region colission
    private void Start()
    {
        omm = FindObjectOfType<OutMapManager>();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag ("Player"))
        {
            omm.isOutMap = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            omm.isOutMap = false;
            omm.Ui.SetActive(false); ;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            omm.Ui.SetActive(true); 
        }
    }
    #endregion
    #region respawn void
    public void ResetPlayer()
    {
        FindObjectOfType<ResetPointScript>().Respawn();
    }
    #endregion
}