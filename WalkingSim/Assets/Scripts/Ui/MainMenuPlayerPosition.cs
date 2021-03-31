using UnityEngine;
public class MainMenuPlayerPosition : MonoBehaviour
{
    #region public componants
    [Tooltip ("put here the main player")]
    public Transform player;
    #endregion
    private void Update()
    {
        transform.position = player.position;
    }
}