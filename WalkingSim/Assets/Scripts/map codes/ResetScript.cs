using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetScript : MonoBehaviour
{
    private ResetPointScript resetPointHolder;
    private void Start()
    {
        resetPointHolder = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ResetPointScript>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            resetPointHolder.Respawn();
        }
    }
}