using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class CheckPointEvents : MonoBehaviour
{
    #region public compants
    [Header("componants")]
    [Tooltip("this only needs to get filed in when finish or close finish is used")]
    public GameObject speedRunUi;
    public TMP_Text timeText;
    public TMP_Text nameText;
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
    public void Test(string textToPrint)
    {
        print(textToPrint);
    }
    public void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Finish()
    {
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
    } 
    public void CloseUI()
    {
        speedRunUi.SetActive(false);
    }
    public void StartSpeedRun()
    {
        isSpeedRunnning = true;
        speedRunTime = 0;
    }
    public void EndSpeedRun(string name)
    {
        isSpeedRunnning = false;
        oldTime=PlayerPrefs.GetFloat(name);
        if (speedRunTime <= oldTime)
        {
            PlayerPrefs.SetFloat(name, speedRunTime);
        }
        else if (oldTime == 0)
        {
            PlayerPrefs.SetFloat(name, speedRunTime);
        }
        speedRunUi.SetActive(true);
        nameText.text = speedRunTime.ToString();
        timeText.text = name;
        Invoke("CloseUI", 1.25f);
        Invoke("ResetTime", 1.25f);

    }
    public void ResetTime()
    {
        speedRunTime = 0;
    }
    #endregion
}