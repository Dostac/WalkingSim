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
    #endregion
    public void Start()
    {
        Main();
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
    #endregion
    #region ui set active clear void
    public void UiOff()
    {
        mainMenu.SetActive(false);
        options.SetActive(false);
        credits.SetActive(false);
        controlls.SetActive(false);
        conRes.SetActive(false);
    }
    #endregion
}