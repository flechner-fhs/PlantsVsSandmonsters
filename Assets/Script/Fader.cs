using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Fader : MonoBehaviour
{
    private List<Image> Images;

    private List<float> defaultAlpha;

    private void Awake()
    {
        Images = GetComponentsInChildren<Image>().ToList();

        defaultAlpha = new List<float>();
        foreach (Image i in Images)
            defaultAlpha.Add(i.color.a);
    }

    public void MakeTransparent()
    {
        foreach (Image i in Images)
            i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
    }

    public void MakeVisible()
    {
        for (int image = 0; image < Images.Count; image++)
            Images[image].color = new Color(Images[image].color.r, Images[image].color.g, Images[image].color.b, defaultAlpha[image]);
    }

    public void FadeOut(float duration)
    {
        StopAllCoroutines();
        StartCoroutine(OutFader(duration, 100));
    }

    private IEnumerator OutFader(float duration, int steps = 100)
    {
        for (int image = 0; image < Images.Count; image++)
            Images[image].color = new Color(Images[image].color.r, Images[image].color.g, Images[image].color.b, defaultAlpha[image]);

        for (float i = 0; i < steps; i++)
        {
            for (int image = 0; image < Images.Count; image++)
            {
                Images[image].color = new Color(Images[image].color.r, Images[image].color.g, Images[image].color.b, Mathf.Lerp(defaultAlpha[image], 0, i/steps));
                yield return new WaitForSeconds(duration / steps);
            }
        }
        foreach (Image i in Images)
            i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
    }

    public void FadeIn(float duration)
    {
        StopAllCoroutines();
        StartCoroutine(InFader(duration, 100));
    }

    private IEnumerator InFader(float duration, int steps = 100)
    {
        foreach (Image i in Images)
            i.color = new Color(i.color.r, i.color.g, i.color.b, 0);

        for (float i = 0; i < steps; i++)
        {
            for (int image = 0; image < Images.Count; image++)
            {
                Images[image].color = new Color(Images[image].color.r, Images[image].color.g, Images[image].color.b, Mathf.Lerp(0, defaultAlpha[image], i / steps));
                yield return new WaitForSeconds(duration / steps);
            }
        }

        for (int image = 0; image < Images.Count; image++)
            Images[image].color = new Color(Images[image].color.r, Images[image].color.g, Images[image].color.b, defaultAlpha[image]);
    }
}
