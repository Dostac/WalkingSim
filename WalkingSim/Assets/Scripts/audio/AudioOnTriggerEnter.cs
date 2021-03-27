using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AudioOnTriggerEnter : MonoBehaviour
{
    public AudioSource bushSound;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            bushSound.Play();
        }
    }
}