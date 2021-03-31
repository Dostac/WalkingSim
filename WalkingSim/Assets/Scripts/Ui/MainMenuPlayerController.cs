using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MainMenuPlayerController : MonoBehaviour
{
    #region public componants
    public Animator anim;
    public float timeForRandomAnim;
    #endregion
    #region private componants
    private float time;
    private bool doingRandom;
    #endregion
    private void Update()
    {
        if (timeForRandomAnim > time)
        {
            time += Time.deltaTime;
        }
        else if (timeForRandomAnim <= time)
        {
            if (!doingRandom)
            {
            RandomAnim();
            }
            time = 0;
        }
    }
    public void RandomAnim()
    {
        int randoNumber;
        randoNumber = Random.Range(1, 6);
        if (randoNumber == 1)
        {
            AnimReset();
            anim.SetBool("action 1", true);
        }
        else if (randoNumber == 2)
        {
            AnimReset();
            anim.SetBool("action 2", true);
        }
        else if (randoNumber == 3)
        {
            AnimReset();
            anim.SetBool("action 3", true);
        }
        else if (randoNumber == 4)
        {
            AnimReset();
            anim.SetBool("action 4", true);
        }
        else if (randoNumber == 5)
        {
            AnimReset();
            anim.SetBool("action 5", true);
        }
        else if (randoNumber == 6)
        {
            AnimReset();
            anim.SetBool("action 6", true);
        }
    }
    public void AnimReset()
    {
        anim.SetBool("action 1", false);
        anim.SetBool("action 2", false);
        anim.SetBool("action 3", false);
        anim.SetBool("action 4", false);
        anim.SetBool("action 5", false);
        anim.SetBool("action 6", false);
    }
    #region Button Voids
    public void MainMenu()
    {

    }
    public void Settings()
    {

    }
    public void Controlls()
    {

    }
    public void Credits()
    {

    }
    public void LoadingScreen()
    {

    }
    public void ResetContinue()
    {

    }
    #endregion
}