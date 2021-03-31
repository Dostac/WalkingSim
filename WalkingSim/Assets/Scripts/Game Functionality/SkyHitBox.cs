using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class SkyHitBox : MonoBehaviour
{
    public UnityEvent onSkyBoxHit;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            onSkyBoxHit?.Invoke();
        }
    }
}