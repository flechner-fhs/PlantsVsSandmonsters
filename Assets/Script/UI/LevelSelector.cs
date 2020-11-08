using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class LevelSelector : MonoBehaviour
{

    public GameObject startingButtons;
    public GameObject levelButtons;
    public AudioMixer audioMixer;
    public string exposedParameter;

    
    public List<string> Scenes;

    public void SelectLevels()
    {
        startingButtons.SetActive(false);
        levelButtons.SetActive(true);
    }

    public void chooseButton(int levelNumber)
    {
        int level = Mathf.Clamp(levelNumber - 1, 0, Scenes.Count - 1);
        StartCoroutine(FadeOutMain.StartFade(audioMixer, exposedParameter, 0.5f, 0.01f, level, Scenes));

    }

    public void startButton()
    {
        chooseButton(1);
    }
}
