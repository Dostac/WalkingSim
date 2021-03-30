using UnityEngine;
public class ResetScript : MonoBehaviour
{
    #region private componants
    private ResetPointScript resetPointHolder;
    #endregion
    #region colision
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
    #endregion
}