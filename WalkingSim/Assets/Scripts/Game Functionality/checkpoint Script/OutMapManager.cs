using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OutMapManager : MonoBehaviour
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
    public bool isOutMap;
    #endregion
    #region private componants
    private float currentTime = 0;
    #endregion
    #region respawn void
    public void ResetPlayer()
    {
        FindObjectOfType<ResetPointScript>().ResetPositon();
    }
    #endregion
    void Update()
    {
        if (isOutMap)
        {
            currentTime += Time.deltaTime;
            float seconds = Mathf.FloorToInt(currentTime % 60);
            timeText.text = seconds.ToString();
            if (currentTime > maxTimeOutMap)
            {
                ResetPlayer();
            }
        }
        else
        {
            if(currentTime >= 0)
            {
                currentTime -= Time.deltaTime;
            }
        }
    }
}
