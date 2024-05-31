using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    [SerializeField] private AudioMixer gameMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    private void Start()
    {
        if (PlayerPrefs.HasKey("gameMusicVolume") && PlayerPrefs.HasKey("gameSFXVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetMusicVolume();
            SetSFXVolume();
        }
    }
    
    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        gameMixer.SetFloat("BackgroundMusic", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("gameMusicVolume", volume);
    }

    public void SetSFXVolume()
    {
        float volume = sfxSlider.value;
        gameMixer.SetFloat("SFXLevel", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("gameSFXVolume", volume);
    }

    private void LoadVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("gameMusicVolume");
        SetMusicVolume();
        sfxSlider.value = PlayerPrefs.GetFloat("gameSFXVolume");
        SetSFXVolume();
    }
}
