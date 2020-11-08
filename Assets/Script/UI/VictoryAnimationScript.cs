using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class VictoryAnimationScript : MonoBehaviour
{
    public GameObject victoryAnim;
    public GameObject monsterAnim;
    public GameObject victorySprite;
    public GameObject victoryText;



    private bool isBeingChased;
    private bool isChasing;

    private SpriteRenderer playerSpriteRenderer;
    private SpriteRenderer monsterSpriteRenderer;
 
    // Start is called before the first frame update
    void Start()
    {
        isBeingChased = true;
        Time.timeScale = 3;
        playerSpriteRenderer = victoryAnim.GetComponent<SpriteRenderer>();
        monsterSpriteRenderer = monsterAnim.GetComponent<SpriteRenderer>();
        isChasing = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isBeingChased)
        {
            victoryAnim.transform.Translate(new Vector3(0.08f, 0, 0));
            monsterAnim.transform.Translate(new Vector3(0.08f, 0, 0));

            if (monsterAnim.transform.position.x > 25)
            {
                victoryAnim.transform.Translate(new Vector3(0, -5, 0));
                monsterAnim.transform.Translate(new Vector3(0, -5, 0));
                isBeingChased = false;
                isChasing = true;
            }
        }

        else if (isChasing)
        {
            playerSpriteRenderer.flipX = true;
            monsterSpriteRenderer.flipX = true;
            victoryAnim.transform.Translate(new Vector3(-0.08f, 0, 0));
            monsterAnim.transform.Translate(new Vector3(-0.08f, 0, 0));

            if (victoryAnim.transform.position.x < -18)
            {
                victoryAnim.transform.Translate(new Vector3(0, 2, 0));
                monsterAnim.SetActive(false);
                Time.timeScale = 1;
                isChasing = false;
                playerSpriteRenderer.flipX = false;
            }
        }

        else
        {
            if (victoryAnim.transform.position.x < 0)
            {
                victoryAnim.transform.Translate(new Vector3(0.05f, 0, 0));
            }
            else
            {
                victoryAnim.SetActive(false);
                victorySprite.SetActive(true);
                victoryText.SetActive(true);
            }
        }

    }
}
