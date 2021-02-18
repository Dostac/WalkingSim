using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalancingEnd : MonoBehaviour
{
    private WallCollision wc;
    private void Start()
    {
        wc = GameObject.FindGameObjectWithTag("PlayerVisual").GetComponent<WallCollision>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            wc.balanceEnd = true;
        }
    }
}
