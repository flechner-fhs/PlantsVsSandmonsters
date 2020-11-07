﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject startingButtons;
    public GameObject levelButtons;
    private GameObject nextLevel;

    public void SelectLeves()
    {
        startingButtons.SetActive(false);
        levelButtons.SetActive(true);
    }

    public void chooseButton(int levelNumber)
    {
        Debug.Log(levelNumber);
        SceneManager.LoadScene(/**levelNumber**/1);
    }
}
