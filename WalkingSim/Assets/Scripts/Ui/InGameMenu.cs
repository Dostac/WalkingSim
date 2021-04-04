using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class InGameMenu : MonoBehaviour
{
    #region public componants
    [Header("Script componants")]
    [Tooltip("insert here ingamemenu gameobject")]
    public GameObject inGameUi;
    [Header("Ui Componants")]
    [Tooltip("insert here the ingame menu main panel")]
    public GameObject main;
    [Tooltip("insert here the options panel")]
    public GameObject options;
    [Tooltip("insert here the loadingScreen panel")]
    public GameObject loadingScreen;
    [Header("Sounds")]
    public AudioSource hoverSound;
    public AudioSource buttonPressedSound;
    #endregion
    #region private componants
    private bool uiVisibility = true;
    private bool loading;
    #endregion

    private void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            if (!loading)
            {
                Escape(uiVisibility);
            }
        }
    }
    public void Escape(bool b)
    {
        uiVisibility = !uiVisibility;
        b = !uiVisibility;
        if (b)
        {
            inGameUi.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
        }
        else
        {
            inGameUi.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Main();
            Time.timeScale = 1;
        }
    }
    #region button voids
    public void Main()
    {
        options.SetActive(false);
        main.SetActive(true);
    }
    public void Options()
    {
        main.SetActive(false);
        options.SetActive(true);
    }
    public void LoadingScreen()
    {
        loading = true;
        main.SetActive(false);
        loadingScreen.SetActive(true);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    #endregion
    #region Sounds
    public void HoverSound()
    {
        Resetsound();
        hoverSound.Play();
    }
    public void PressedSound()
    {
        Resetsound();
        buttonPressedSound.Play();
    }

    public void Resetsound()
    {
        buttonPressedSound.Stop();
        hoverSound.Stop();
    }
    #endregion
}