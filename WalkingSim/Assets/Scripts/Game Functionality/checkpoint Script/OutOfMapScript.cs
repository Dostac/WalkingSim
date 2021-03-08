using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class OutOfMapScript : MonoBehaviour
{
    [Header("Script Componants")]
    public GameObject Ui;
    public TMP_Text timeText;
    [Header("Script Values")]
    public float maxTimeOutMap;
    private float currentTime=0;
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
    public void ResetPlayer()
    {
        FindObjectOfType<ResetPointScript>().Respawn();
    }
}
