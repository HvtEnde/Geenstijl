using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MusicSet : MonoBehaviour
{

    public AudioMixer musicMixer;
    public float musicSaver;
    public Slider musicSlider;

    public void Start()
    {
        musicSlider.value = PlayerPrefs.GetFloat("Music", musicSaver);
    }
    public void SetVolume(float music)
    {
        musicMixer.SetFloat("Music", music);
        musicSaver = music;
        PlayerPrefsSet();
    }
    public void PlayerPrefsSet()
    {
        PlayerPrefs.SetFloat("Music", musicSaver);
    }
}
