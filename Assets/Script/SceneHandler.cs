using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public void GotoGameScene()
    {
        SceneManager.LoadScene(0);
    }

    public void GotoMainmenuScene()
    {
        SceneManager.LoadScene(1);
    }

    public void ShowOptions()
    {
        SceneManager.LoadScene(2);
    }

    public void GotoPauseMenu()
    {
        SceneManager.LoadScene(1);
    }
}

