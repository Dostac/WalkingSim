using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Saves : MonoBehaviour
{
    #region componants
    [Tooltip("the value that it gets from a other script named (AudioManager)")]
    public List<float> audioSaves;
    #endregion
    #region the save
    public void SaveEveryThink(float master, float sfx, float music, float ui)
    {
        audioSaves[0] = master;
        audioSaves[1] = sfx;
        audioSaves[2] = music;
        audioSaves[3] = ui;

        PlayerPrefs.SetFloat("mastervalue", audioSaves[0]);
        PlayerPrefs.SetFloat("sfxvalue", audioSaves[1]);
        PlayerPrefs.SetFloat("musicvalue", audioSaves[2]);
        PlayerPrefs.SetFloat("uivalue", audioSaves[3]);
    }
    #endregion
}