using UnityEngine;
public class BalancingBegin : MonoBehaviour
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
            wc.balanceBegin = true;
        }
    }
}