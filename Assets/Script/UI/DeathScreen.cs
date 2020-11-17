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
            _multiplier = (float) ((1 - Time.timeScale) * 0.04) * AnimationSpeed;
            animation.transform.localScale += new Vector3(_multiplier, _multiplier, 0);
            //transform.Translate(new Vector3(0, -0.2f, 0));
        }

        for(int i = 0; i < AnimationSpeed; i++)
            Time.timeScale *= 0.9965f;

        if (Time.timeScale <= 0.35)
        {
            Time.timeScale = 0;
        }
    }

    void Update()
    {
        if (Time.timeScale == 0)
        {
            animation.transform.localScale += new Vector3(-0.01f, -0.01f, 0) * AnimationSpeed;

            if (animation.transform.localScale.x < 0.1f)
            {
                animation.SetActive(false);
                restartButton.SetActive(true);

                if (!didButtonExpand)
                {
                    didButtonExpand = true;
                    for (int i = 0; i < 10; i++)
                    {
                        restartButton.transform.localScale += new Vector3(+0.1f, +0.1f, 0);
                    }
                }
            }
        }
    }
}


