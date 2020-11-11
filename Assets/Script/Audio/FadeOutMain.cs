using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public static class FadeOutMain
{
    [Obsolete("StartCoroutine(FadeOutMain.StartFade(...)) is deprecated, please use GameManager.Instance.TransitionController.ChangeScene(...) instead.")]
    public static IEnumerator StartFade(AudioMixer audioMixer, string exposedParam, float duration, float targetVolume, int level, List<string> Scenes)
    {
        float currentTime = 0;
        float currentVol;
        audioMixer.GetFloat(exposedParam, out currentVol);
        currentVol = Mathf.Pow(10, currentVol / 20);
        float targetValue = Mathf.Clamp(targetVolume, 0.0001f, 1);

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float newVol = Mathf.Lerp(currentVol, targetValue, currentTime / duration);

            audioMixer.SetFloat(exposedParam, Mathf.Log10(newVol) * 20);
            yield return null;
        }
        SceneManager.LoadScene(Scenes[level]);
        yield break;
    }
}