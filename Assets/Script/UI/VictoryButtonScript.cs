using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class VictoryButtonScript : MonoBehaviour
{
    // Start is called before the first frame update

    public void QuitButton()
    {
        GameManager.Instance.TransitionController.ChangeScene("TitleScene");
    }

    public void playAgainButton()
    {
        GameManager.Instance.TransitionController.ChangeScene("Level " + GameManager.Instance.CurrentLevel);
    }
}
