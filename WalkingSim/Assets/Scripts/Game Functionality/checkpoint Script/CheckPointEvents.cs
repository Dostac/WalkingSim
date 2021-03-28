using UnityEngine;
using UnityEngine.SceneManagement;
public class CheckPointEvents : MonoBehaviour
{
    #region public compants
    [Header("componants")]
    [Tooltip("this only needs to get filed in when finish or close finish is used")]
    public GameObject finishUI;
    #endregion
    #region private compants
    private float speedRunTime;
    private float oldTime;
    private bool isSpeedRunnning;
    #endregion
    #region events void
    private void Update()
    {
        if (isSpeedRunnning)
        {
            speedRunTime += Time.deltaTime;
        }
    }
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
    public void StartSpeedRun()
    {
        isSpeedRunnning = true;
    }
    public void EndSpeedRun(string name)
    {
        isSpeedRunnning = false;
        PlayerPrefs.GetFloat(name, oldTime);
        if (speedRunTime > oldTime)
        {
            PlayerPrefs.SetFloat(name, speedRunTime);
        }
        Invoke("ResetTime", 0.25f);
    }
    public void ResetTime()
    {
        speedRunTime = 0;
    }
    #endregion
}