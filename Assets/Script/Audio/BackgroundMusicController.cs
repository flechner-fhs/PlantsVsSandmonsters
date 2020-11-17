using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class BackgroundMusicController : MonoBehaviour
{
    public float Volume;
    public AudioMixer AudioMixer;

    private void Awake()
    {
        InitSoundVolume();
    }

    private void InitSoundVolume()
    {
        SetSoundVolume(PlayerPrefs.GetFloat("MusicVolume", 0.75f));
    }

    public void SetSoundVolume(float ratio)
    {
        Volume = ratio;
        float db = Mathf.Lerp(-40, 12, ratio);
        AudioMixer.SetFloat("BackgroundMusicVolume", db < -35 ? -80 : db);
        PlayerPrefs.SetFloat("MusicVolume", ratio);
    }
}
