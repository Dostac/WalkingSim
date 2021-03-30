using UnityEngine;
public class BalancingBegin : MonoBehaviour
{
    #region private componant
    private WallCollision wc;
    #endregion
    #region collision detection
    private void Start()
    {
        wc = GameObject.FindGameObjectWithTag("PlayerVisual").GetComponent<WallCollision>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerVisual")
        {
            wc.balanceBegin = true;
        }
    }
    #endregion
}