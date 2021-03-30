using UnityEngine;
public class BalancingEnd : MonoBehaviour
{
    #region private componant
    private WallCollision wc;
    #endregion
    #region colission detection
    private void Start()
    {
        wc = GameObject.FindGameObjectWithTag("PlayerVisual").GetComponent<WallCollision>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerVisual")
        {
            wc.balanceEnd = true;
        }
    }
    #endregion
}