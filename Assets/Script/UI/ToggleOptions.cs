using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToggleOptions : MonoBehaviour
{
    public GameObject optionsObject;
    public GameObject menuObject;
    public GameObject pixelateCamera;

    // Start is called before the first frame update
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
            

            menuObject.SetActive(true);         
        }
    }

    public void resumeToGame()
    {
        menuObject.SetActive(false);
        Time.timeScale = 1;
        pixeldCamera = pixelateCamera.GetComponent<Pixelate>();
        pixeldCamera.pixelSizeX = 0;
    }

    public void goToSettings()
    {
        menuObject.SetActive(false);
        optionsObject.SetActive(true);
    }

    public void leaveSettings()
    {
        optionsObject.SetActive(false);
        menuObject.SetActive(true);
    }

    public void goToMainscreenn()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
