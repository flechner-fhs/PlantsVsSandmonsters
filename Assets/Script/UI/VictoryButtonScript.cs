using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class VictoryButtonScript : MonoBehaviour
{
    // Start is called before the first frame update

    public void QuitButton()
    {
        SceneManager.LoadScene("TitleScene");
    }

    public void playAgainButton()
    {
        SceneManager.LoadScene("Level 1");
    }
}
