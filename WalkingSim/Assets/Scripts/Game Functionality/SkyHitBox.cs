using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class SkyHitBox : MonoBehaviour
{
    public UnityEvent onSkyBoxHit;
    private PlayerMovement pm;
    private void Start()
    {
        pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!pm.action)
        {
            if (other.CompareTag("Player"))
            {
                onSkyBoxHit?.Invoke();
            }
        }
    }
}