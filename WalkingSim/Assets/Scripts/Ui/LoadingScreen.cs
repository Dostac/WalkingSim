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
    public TMP_Text text;
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
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            slider.value = progress;
            text.text = progress * 100f + "%";
            yield return null;
        }
    }
    #endregion
}