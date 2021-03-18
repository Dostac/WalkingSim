using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LoadingScreen : MonoBehaviour
{
    #region componants
    [Header("loading screen componants")]
    [Tooltip("set a value for the code to load this scene")]
    public int scene;
    [Space(5)]
    [Tooltip("this text is gonna be the text it says how mutch procent the loading progress is")]
    public TMP_Text progressText;
    [Tooltip("this text is gonna be the text that is the loading...")]
    public TMP_Text loadingText;
    [Space(3)]
    [Tooltip("this slider is gonna be the slider it says how mutch procent the loading progress is")]
    public Slider slider;
    [Space(1)]
    [Tooltip("this is the object that it sets active when called")]
    public GameObject loadingScreen;
    #endregion
    #region fuctions
    private void Start()
    {
        loadingScreen.SetActive(false);
    }
    public void StartLoadingScreen()
    {
        StartCoroutine(LoadSceneEnumerator());
    }
    IEnumerator LoadSceneEnumerator()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(scene);
        loadingScreen.SetActive(true);
        Loading();
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            slider.value = progress;
            progressText.text = progress * 100f + "%";
            yield return null;
        }
    }
    public void Loading()
    {
        loadingText.text = ("loading");
        Invoke("LoadingDot", 0.25f);
    }
    public void LoadingDot()
    {
        loadingText.text = ("loading.");
        Invoke("LoadingDotDot", 0.25f);
    }
    public void LoadingDotDot()
    {
        loadingText.text = ("loading..");
        Invoke("LoadingDotDotDot", 0.25f);
    }
    public void LoadingDotDotDot()
    {
        loadingText.text = ("loading...");
        Invoke("Loading", 0.25f);
    }
    #endregion
}