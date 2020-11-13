using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TransitionController : MonoBehaviour
{
    public RawImage TransitionOverlay;

    public Color TransitionColor = new Color(0, 0, 0, 0);

    public float TransitionDuration = 1;

    private bool stillInTransition = false;

    private void Awake()
    {
        TransitionOverlay.color = new Color(TransitionColor.r, TransitionColor.g, TransitionColor.b, 0);
        TransitionOverlay.gameObject.SetActive(false);
    }

    public void ChangeScene(string targetScene)
    {
        if (!stillInTransition)
            StartCoroutine(TransitionToScene(targetScene));
    }

    private IEnumerator TransitionToScene(string targetScene)
    {
        stillInTransition = true;
        TransitionOverlay.gameObject.SetActive(true);
        float startVolume = GameManager.Instance.MusicController.Volume;
        float alpha, volume;

        float iterations = TransitionDuration * 100;
        for(int i = 0; i < iterations; i++)
        {
            alpha = Mathf.Lerp(0, 1, i / iterations);
            volume = Mathf.Lerp(startVolume, 0, i / iterations);

            TransitionOverlay.color = new Color(TransitionColor.r, TransitionColor.g, TransitionColor.b, alpha);
            GameManager.Instance.MusicController.SetSoundVolume(volume);
            yield return new WaitForSeconds(TransitionDuration / iterations);
        }

        SceneManager.LoadScene(targetScene);

        for (int i = 0; i < iterations; i++)
        {
            alpha = Mathf.Lerp(1, 0, i / iterations);
            volume = Mathf.Lerp(0, startVolume, i / iterations);

            TransitionOverlay.color = new Color(TransitionColor.r, TransitionColor.g, TransitionColor.b, alpha);
            GameManager.Instance.MusicController.SetSoundVolume(volume);
            yield return new WaitForSeconds(TransitionDuration / iterations);
        }
        TransitionOverlay.gameObject.SetActive(false);
        stillInTransition = false;
    }
}
