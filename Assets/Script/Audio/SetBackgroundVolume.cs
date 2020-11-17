using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class SetBackgroundVolume : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider slider;
    public static float Volume;

    void Start()
    {
        if (!GameManager.Instance)
        {
            slider.value = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
            SetSoundVolume(slider.value);
        }
        
    }

    public void SetLevel(float sliderValue)
    {
        if (!GameManager.Instance)
        {
            SetSoundVolume(sliderValue);
            PlayerPrefs.SetFloat("MusicVolume", sliderValue);
        }
        else
            GameManager.Instance.MusicController.SetSoundVolume(sliderValue);
    }

    private void SetSoundVolume(float ratio)
    {
        Volume = ratio;
        float db = Mathf.Lerp(-40, 12, ratio);
        mixer.SetFloat("BackgroundMusicVolume", db < -35 ? -80 : db);
    }
}
