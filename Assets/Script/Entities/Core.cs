using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Core : Entity
{
    public override void Die()
    {
        Debug.Log("You lost!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public override void Move()
    {
        //I don't move
    }


}
