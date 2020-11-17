using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VictoryButtonScript : MonoBehaviour
{
    public int numberOfLevels;
    public Button quitButton;
    public Button playAgainButton;
    public Button playNextButton;
    public void Start()
    {
        if ((GetCurrentLevel()) == numberOfLevels)
        {
            quitButton.transform.localPosition = new Vector3(-200, -170, 0);
            playAgainButton.transform.localPosition = new Vector3(200, -170, 0);
            playNextButton.transform.localScale = new Vector3(0, 0, 0);
        }
    }

    public void QuitButton()
    {
        GameManager.Instance.TransitionController.ChangeScene("TitleScene");
    }

    public void PlayAgainButton()
    {
        GameManager.Instance.TransitionController.ChangeScene("Level " + GameManager.Instance.CurrentLevel);
    }

    public void PlayNextButton()
    {
        string nextLevel = "Level " + (GetCurrentLevel() + 1).ToString();
        GameManager.Instance.TransitionController.ChangeScene(nextLevel);
    }

    public int GetCurrentLevel()
    {
        string nextLevel = "Level " + GameManager.Instance.CurrentLevel;
        int currentLevel = int.Parse(nextLevel.Substring(nextLevel.Length - 1, 1));
        return currentLevel;
    }
}
