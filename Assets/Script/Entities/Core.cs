using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Core : Entity
{

    private new void Awake()
    {
        base.Awake();

        GetComponentInChildren<SpriteRenderer>().gameObject.SetActive(false);
    }

    public override void Die()
    {
        Debug.Log("You lost!");
        SceneManager.LoadScene(3);
    }

    public override void Move()
    {
        //I don't move
    }


}
