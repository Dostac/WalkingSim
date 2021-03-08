using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GravityTrigger : MonoBehaviour
{
    [Header("turinig on or of")]
    public bool gravity;
    private GameObject player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (gravity)
            {
                player.GetComponent<Rigidbody>().useGravity = true;
                player.GetComponent<Rigidbody>().isKinematic = false;
            }
            else
            {
                player.GetComponent<Rigidbody>().useGravity = false;
                player.GetComponent<Rigidbody>().isKinematic = true;
            }
        }
    }
}