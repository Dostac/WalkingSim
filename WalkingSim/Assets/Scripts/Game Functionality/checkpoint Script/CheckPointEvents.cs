using UnityEngine;
using UnityEngine.SceneManagement;
public class CheckPointEvents : MonoBehaviour
{
    #region public compants
    [Header("componants")]
    [Tooltip("this only needs to get filed in when finish or close finish is used")]
    public GameObject finishUI;
    #endregion
    #region events void
    public void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Finish()
    {
        finishUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
    } 
    public void CloseFinish()
    {
        finishUI.SetActive(false);
    }
    #endregion
}