using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class ToggleOptions : MonoBehaviour
{
    public GameObject settingsObject;
    public GameObject pauseObject;
    public GameObject pixelateCamera;
    public GameObject lifebars;
    public AudioMixer audioMixer;
    public string exposedParameter;

    private Pixelate pixeldCamera;


    void Start()
    {
        StartCoroutine(FadeInAudio.StartFade(audioMixer, exposedParameter, 2f, 0.75f));
    }
    void Update()
    {
        if (Input.GetKeyDown("escape") && (pauseObject.activeSelf == false) && (settingsObject.activeSelf == false))
        {
            lifebars.SetActive(false);
            pixeldCamera = pixelateCamera.GetComponent<Pixelate>();
            Time.timeScale = 0;

            float timeElapsed = 0;
            while (timeElapsed < 2)
            {
                timeElapsed += Time.deltaTime;
                pixeldCamera.pixelSizeX = (int)(timeElapsed * 9);
            }
            pauseObject.SetActive(true);
        }
    }

    public void resumeToGame()
    {
        pauseObject.SetActive(false);
        Time.timeScale = 1;
        pixeldCamera = pixelateCamera.GetComponent<Pixelate>();
        pixeldCamera.pixelSizeX = 0;
        lifebars.SetActive(true);
    }

    public void goToSettings()
    {
        pauseObject.SetActive(false);
        settingsObject.SetActive(true);
    }

    public void leaveSettings()
    {
        settingsObject.SetActive(false);
        pauseObject.SetActive(true);
    }

    public void goToMainscreen()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
