using UnityEngine;
using TMPro;
public class OutOfMapScript : MonoBehaviour
{
    #region public componants
    [Header("Script Componants")]
    [Tooltip("ui to be set active")]
    public GameObject Ui;
    [Tooltip("the text that wil be displaying the time he can stil be out of the map")]
    public TMP_Text timeText;
    [Header("Script Values")]
    [Tooltip("the time the player can be out of the map")]
    public float maxTimeOutMap;
    #endregion
    #region private componants
    private float currentTime=0;
    #endregion
    #region colission
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag ("Player"))
        {
            currentTime += Time.deltaTime;
            timeText.text = currentTime.ToString();

            if (currentTime> maxTimeOutMap)
            {
                ResetPlayer();
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            currentTime = 0;
            Ui.SetActive(false); ;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Ui.SetActive(true); 
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