using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MainMenuCamereaScript : MonoBehaviour
{
    #region public componants
    [Header("Script Componants")]
    public GameObject cam;
    public float movementSpeed = 1.5f;
    public float rotationSpeed = 1.5f;
    [Header("Positions")]
    public GameObject mainMenPos;
    public GameObject settingsPos;
    public GameObject creditsPos;
    public GameObject controllsPos;
    public GameObject loadingScreenPos;
    public GameObject ressetContrinuePos;
    #endregion
    #region private componants
    private bool lerp;
    #endregion
    #region public componants
    private Transform destenation;
    #endregion
    private void Start()
    {
        destenation = mainMenPos.transform;
    }
    private void Update()
    {
        if (lerp)
        {
            cam.transform.position = Vector3.Lerp(cam.transform.position, destenation.position,  movementSpeed * Time.deltaTime);
            cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, destenation.rotation, rotationSpeed * Time.deltaTime);
            if (Vector3.Distance(cam.transform.position, destenation.position) < 0.1f)
            {
                lerp = false;
            }
        }
    }
    #region Button Voids
    public void MainMenu()
    {
        destenation = mainMenPos.transform;
        lerp = true;
    }
    public void Settings()
    {
        destenation = settingsPos.transform;
        lerp = true;
    }
    public void Controlls()
    {
        destenation = controllsPos.transform;
        lerp = true;
    }
    public void Credits()
    {
        destenation = creditsPos.transform;
        lerp = true;
    }
    public void LoadingScreen()
    {
        destenation = loadingScreenPos.transform;
        lerp = true;
    }
    public void ResetContinue()
    {
        destenation = ressetContrinuePos.transform;
        lerp = true;
    }
    #endregion
}