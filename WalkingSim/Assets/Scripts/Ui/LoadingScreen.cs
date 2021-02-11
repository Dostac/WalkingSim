using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    public int scene;
    public TMP_Text text;
    public Slider slider;
    public GameObject loadingScreen;
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
}