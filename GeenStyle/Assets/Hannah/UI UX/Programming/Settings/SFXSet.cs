using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SFXSet : MonoBehaviour
{
    public AudioMixer sfxMixer;
    public float sfxSaver;
    public Slider sfxSlider;

    public void Start()
    {
        sfxSlider.value = PlayerPrefs.GetFloat("SFX", sfxSaver);
    }
    public void SetVolume(float sfx)
    {
        sfxMixer.SetFloat("SFX", sfx);
        sfxSaver = sfx;
        SfxPlayerPrefsSet();
    }
    public void SfxPlayerPrefsSet()
    {
        PlayerPrefs.SetFloat("SFX", sfxSaver);
    }
}
