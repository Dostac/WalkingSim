using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckPointEvents : MonoBehaviour
{
    [Header("componants")]
    public GameObject finishUI;
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
}