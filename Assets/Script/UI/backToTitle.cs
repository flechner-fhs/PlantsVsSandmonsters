using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class backToTitle : MonoBehaviour
{
    public void ChangeToTitle()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
