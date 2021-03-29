using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuScript : MonoBehaviour
{
    #region componants
    [Header("Ui canvases")]
    [Tooltip("Get the componant ui canvas/ empty object where the main menu is under")]
    public GameObject mainMenu;
    [Space(1)]
    [Tooltip("Get the componant ui canvas/ empty object where the options is under")]
    public GameObject options;
    [Space(1)]
    [Tooltip("Get the componant ui canvas/ empty object where the credits is under")]
    public GameObject credits;
    [Space(1)]
    [Tooltip("Get the componant ui canvas/ empty object where the controlls is under")]
    public GameObject controlls;
    [Space(1)]
    [Tooltip("Get the componant ui canvas/ empty object where the contiune/reset is under")]
    public GameObject conRes;
    [Header("Speed run pannels")]
    public GameObject pannel1, pannel2, pannel3;
    [Header("Sounds")]
    public AudioSource hoverSound;
    public AudioSource buttonPressedSound;
    #endregion
    public void Start()
    {
        Main();
        Time.timeScale = 1;
    }
    #region button functions
    public void QuitGame()
    {
        Application.Quit();
    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void Options()
    {
        UiOff();
        options.SetActive(true);
        mainMenu.SetActive(false);
    }
    public void Credits()
    {
        UiOff();
        credits.SetActive(true);
    }
    public void Main()
    {
        UiOff();
        mainMenu.SetActive(true);
    }
    public void ConRes()
    {
        UiOff();
        conRes.SetActive(true);
    }
    public void Control()
    {
        UiOff();
        controlls.SetActive(true);
    }
    public void HoverSound()
    {
        buttonPressedSound.Stop();
        hoverSound.Play();
    }
    public void PressedSound()
    {
        hoverSound.Stop();
        buttonPressedSound.Play();
    }
    public void SpeedRunPannel1()
    {
        UiOff();
        pannel1.SetActive(true);
    }
    public void SpeedRunPannel2()
    {
        UiOff();
        pannel2.SetActive(true);
    }
    public void SpeedRunPannel3()
    {
        UiOff();
        pannel3.SetActive(true);
    }
    #endregion
    #region ui set active clear void
    public void UiOff()
    {
        mainMenu.SetActive(false);
        options.SetActive(false);
        credits.SetActive(false);
        controlls.SetActive(false);
        conRes.SetActive(false);
        pannel1.SetActive(false);
        pannel2.SetActive(false);
        pannel3.SetActive(false);
    }
    #endregion
}