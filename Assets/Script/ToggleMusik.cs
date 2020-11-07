using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleMusik : MonoBehaviour
{
    AudioSource backgroundMusic;
    Toggle m_Toggle;
    GameObject thisToggle;

    public void ToggleMusikMethod(bool isMusicOn)
    {
        m_Toggle = GetComponent<Toggle>();
        
        if (backgroundMusic == null)
        {
            thisToggle = GameObject.Find("MusikToggle");
            backgroundMusic = GetComponent<AudioSource>();
            backgroundMusic.loop = true;
        }


        if (m_Toggle.isOn == true)
        {
            backgroundMusic.Play();
        }
        else
        {
            backgroundMusic.Stop();
        }
    }
}
