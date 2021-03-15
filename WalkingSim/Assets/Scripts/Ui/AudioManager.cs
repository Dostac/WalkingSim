using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    #region componants
    [Tooltip("put in the mastermixer")]
    public AudioMixer _MasterMixer;
    [Space(5)]

    [Tooltip("put in the masterslider")]
    public Slider slider;
    [Tooltip("put in the sfx slider")]
    public Slider slider1;
    [Tooltip("put in the musicslider")]
    public Slider slider2;
    [Tooltip("put in the uislider")]
    public Slider slider3;

    [Space(10)]
    [Tooltip("these values are getting saved in a other scripts into the unity player prefs")]
    public float sliderF, slider1F, slider2F, slider3F;
    [Space(5)]
    [Tooltip("saved values it gets from player prefs")]
    public List<float> savedValue;
    #endregion
    #region gets voliume on awake
    public void Awake()
    {
        savedValue[0] = PlayerPrefs.GetFloat("mastervalue", 0);
        savedValue[1] = PlayerPrefs.GetFloat("sfxvalue", 0);
        savedValue[2] = PlayerPrefs.GetFloat("musicvalue", 0);
        savedValue[3] = PlayerPrefs.GetFloat("uivalue", 0);

        _MasterMixer.SetFloat("master", savedValue[0]);
        _MasterMixer.SetFloat("sfx", savedValue[1]);
        _MasterMixer.SetFloat("music", savedValue[2]);
        _MasterMixer.SetFloat("ui", savedValue[3]);

        slider.value = savedValue[0];
        slider1.value = savedValue[1];
        slider2.value = savedValue[2];
        slider3.value = savedValue[3];
    }
    #endregion
    #region voids for sliders
    public void SetMasterVolume(Slider volume)
    {
        _MasterMixer.SetFloat("master", volume.value);
        sliderF = volume.value;
        FindObjectOfType<Saves>().SaveEveryThink(sliderF, slider1F, slider2F, slider3F);
    }
    public void SetSFXVolume(Slider volume)
    {
        _MasterMixer.SetFloat("sfx", volume.value);
        slider1F = volume.value;
        FindObjectOfType<Saves>().SaveEveryThink(sliderF, slider1F, slider2F, slider3F);
    }
    public void SetMusicVolume(Slider volume)
    {
        _MasterMixer.SetFloat("music", volume.value);
        slider2F = volume.value;
        FindObjectOfType<Saves>().SaveEveryThink(sliderF, slider1F, slider2F, slider3F);
    }
    public void SetUIVolume(Slider volume)
    {
        _MasterMixer.SetFloat("ui", volume.value);
        slider3F = volume.value;
        FindObjectOfType<Saves>().SaveEveryThink(sliderF, slider1F, slider2F, slider3F);
    }
    #endregion
}
