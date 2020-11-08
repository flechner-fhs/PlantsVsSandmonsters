using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject startingButtons;
    public GameObject levelButtons;
    private GameObject nextLevel;

    public List<string> Scenes;

    public void SelectLevels()
    {
        startingButtons.SetActive(false);
        levelButtons.SetActive(true);
    }

    public void chooseButton(int levelNumber)
    {
        Debug.Log(levelNumber);
        //SceneManager.LoadScene(/**levelNumber**/2);
        int level = Mathf.Clamp(levelNumber - 1, 0, Scenes.Count - 1);
        SceneManager.LoadScene(Scenes[level]);
    }

    public void startButton()
    {
        chooseButton(1);
        //SceneManager.LoadScene(2);
    }
}
