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
    public GameObject victoryCanvas;
    public AudioSource smackAudio;
    public AudioSource vicoryAudio;
    public AudioSource pianoAudio;
    public Text victoryText;



    private bool isBeingChased;
    private bool isChasing;

    private SpriteRenderer playerSpriteRenderer;
    private SpriteRenderer monsterSpriteRenderer;
 
    // Start is called before the first frame update
    void Start()
    {
        pianoAudio.Play();
        isBeingChased = true;
        Time.timeScale = 3;
        playerSpriteRenderer = victoryAnim.GetComponent<SpriteRenderer>();
        monsterSpriteRenderer = monsterAnim.GetComponent<SpriteRenderer>();
        isChasing = false;
        victoryText.color = new Color(victoryText.color.r, victoryText.color.g, victoryText.color.b, 0);
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
                pianoAudio.Stop();
                smackAudio.Play(0);
                vicoryAudio.PlayDelayed(2);
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
                victoryCanvas.SetActive(true);
                StartCoroutine("FadeInText");
            }
        }

    }
    IEnumerator FadeInText()
    {
        while (victoryText.color.a < 1)
        {
            victoryText.color = new Color(victoryText.color.r, victoryText.color.g, victoryText.color.b, victoryText.color.a + 0.001f);
            Debug.Log("a");
            yield return null;
        }
        
    }

}
