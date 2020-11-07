using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToggleOptions : MonoBehaviour
{
    public GameObject optionsObject;
    public GameObject menuObject;
    // Start is called before the first frame update

    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            Time.timeScale = 0;
            menuObject.SetActive(true);
        }
    }

    public void resumeToGame()
    {
        menuObject.SetActive(false);
        Time.timeScale = 1;
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
