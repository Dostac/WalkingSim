using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartScript : MonoBehaviour
{
    void Update()
    {
        if (Input.GetButton("R"))
        {
            Restart();
        }
    }
    public void Restart()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
}
