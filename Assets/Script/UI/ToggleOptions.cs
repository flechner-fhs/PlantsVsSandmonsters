using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToggleOptions : MonoBehaviour
{
    public GameObject settingsObject;
    public GameObject pauseObject;
    public GameObject pixelateCamera;

    private Pixelate pixeldCamera;

    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            pixeldCamera = pixelateCamera.GetComponent<Pixelate>();
            Time.timeScale = 0;

            float timeElapsed = 0;
            while (timeElapsed < 2)
            {
                timeElapsed += Time.deltaTime;
                pixeldCamera.pixelSizeX = (int) (timeElapsed * 9);
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
