using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AudioOnTriggerEnter : MonoBehaviour
{
    public AudioSource bushSound;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            bushSound.Play();
        }
    }
}