using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEngine;

public class MoveText : MonoBehaviour
{
    public GameObject animation;
    public GameObject text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
            if(Time.timeScale > 0.25)
            {
                animation.transform.localScale += new Vector3(0.0035f, 0.0035f, 0);
                transform.Translate(new Vector3(0, -0.2f, 0));
            }

            Time.timeScale *= 0.998f;

            if(Time.timeScale < 0.25)
            {
                Time.timeScale = 0;
                animation.transform.localScale += new Vector3(-0.008f, -0.008f, 0);

            if (animation.transform.localScale.x < 0.1f)
                {
                    animation.SetActive(false);
            }
            }

        }
    }
