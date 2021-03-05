using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public void CurserLock()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void CurserUnlock()
    {
        Cursor.lockState = CursorLockMode.None;
    }
    public void TimeScaleOn()
    {
        Time.timeScale = 1;
    }
    public void TimeScaleOff()
    {
        Time.timeScale = 0;
    }
}
