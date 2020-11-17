using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class DeathScreen : MonoBehaviour
{
    public GameObject animation;
    public GameObject text;
    public GameObject restartButton;
    public bool didButtonExpand;

    public float AnimationSpeed = 3;
    
    void Start()
    {
        didButtonExpand = false;
    }

    void FixedUpdate()
    {
        float _multiplier;
        if (Time.timeScale > 0.25)
        {
            _multiplier = (float) ((1 - Time.timeScale) * 0.03) * AnimationSpeed;
            animation.transform.localScale += new Vector3(_multiplier, _multiplier, 0);
            //transform.Translate(new Vector3(0, -0.2f, 0));
        }

        for(int i = 0; i < AnimationSpeed; i++)
            Time.timeScale *= 0.9975f;

        if (Time.timeScale <= 0.25)
        {
            Time.timeScale = 0;
        }
    }

    void Update()
    {
        if (Time.timeScale == 0)
        {
            animation.transform.localScale += new Vector3(-0.005f, -0.005f, 0) * AnimationSpeed;

            if (animation.transform.localScale.x < 0.1f)
            {
                animation.SetActive(false);

                if (!didButtonExpand)
                {
                    didButtonExpand = true;
                    StartCoroutine("MoveRetryButton");
                }
            }
        }
    }

    IEnumerator MoveRetryButton()
    {
        while (restartButton.transform.localPosition.y < 60)

        {
            restartButton.transform.localScale += new Vector3(+0.01f, +0.01f, 0);
            restartButton.transform.Translate(new Vector3(0, 3, 0));
            yield return null;
        }
    }
}


