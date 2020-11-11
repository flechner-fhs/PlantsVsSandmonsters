using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    private List<MonsterWave> Waves;

    public enum Stage { Rain, Prepare, Wave }
    public Stage WaveStage = Stage.Rain;

    public int Wave = 0;

    public Player Player;

    public float RainDuration = 5;
    public float PrepareDuration = 10;

    public Text Infobox;
    public Text LevelInfo;

    public GameObject RainOverlay;

    private void Start()
    {
        StopAllCoroutines();
        RainOverlay.GetComponent<Fader>().MakeTransparent();

        Waves = GetComponentsInChildren<MonsterWave>().ToList();

        Waves.Sort((a, b) => a.Index - b.Index);

        NextStage();
    }

    public void NextStage()
    {
        WaveStage = (Stage)(((int)WaveStage + 1) % 3);
        UpdateLevelInfo();

        switch (WaveStage)
        {
            case Stage.Rain:
                StartCoroutine(MakeRain());
                break;
            case Stage.Prepare:
                StartCoroutine(StartPreparation());
                break;
            case Stage.Wave:
                StartCoroutine(StartWave());
                break;
        }
    }

    public IEnumerator MakeRain()
    {
        Infobox.text = "Rain Phase";
        RainOverlay.GetComponent<Fader>().FadeIn(1);
        FindObjectsOfType<EarthDrop>().ToList().ForEach(x => x.IsHumid = true);
        List<Plant> plants = FindObjectsOfType<Plant>().ToList();
        for (int i = 0; i < 10; i++)
        {
            Player.AddWaterToSupply(10);
            plants.ForEach(x => x.ChangeWaterSupply(10));
            yield return new WaitForSeconds(RainDuration/10f);
        }
        RainOverlay.GetComponent<Fader>().FadeOut(1);
        NextStage();
    }

    public IEnumerator StartPreparation()
    {
        for(int i = 0; i < PrepareDuration; i++)
        {
            Infobox.text = $"Preparation {(int)PrepareDuration-i}s";
            yield return new WaitForSeconds(1);
        }
        NextStage();
    }

    public IEnumerator StartWave()
    {
        Infobox.text = "Wave incoming!";
        MonsterWave[] waves = Waves.Where(x => x.Index == Wave).ToArray();
        waves.ToList().ForEach(x => x.Spawner.SpawnWave(x.Monsters));
        Coroutine counter = StartCoroutine(UpdateMonsterCounter());
        yield return new WaitForSeconds(5);
        yield return new WaitUntil(() => Enemy.Enemies.Count == 0);
        StopCoroutine(counter);
        Waves.RemoveAll(x => waves.Contains(x));
        Wave++;

        if(Waves.Count == 0)
        {
            if(!FindObjectOfType<Core>().IsDead && !Player.IsDead)
            {
                Player.IsDead = true;
                Player.AnimationController.SetPlayState(AnimationController.PlayState.idleDown);
                CinemachineVirtualCamera vcam = FindObjectOfType<CinemachineVirtualCamera>();
                for(int i = 0; i < 100; i++)
                {
                    vcam.m_Lens.OrthographicSize = Mathf.Lerp(4, 2, i/100f);
                    yield return new WaitForSeconds(.01f);
                }
                GameManager.Instance.StageCleared();
                GameManager.Instance.TransitionController.ChangeScene("VictoryScene");
            }
        }
        else
            NextStage();
    }

    public void UpdateLevelInfo() => LevelInfo.text = SceneManager.GetActiveScene().name + "\nWave " + (Wave + 1); 

    public IEnumerator UpdateMonsterCounter()
    {
        while (true)
        {
            Infobox.text = $"Wave incoming!\n{Enemy.Enemies.Count} left";
            yield return new WaitForEndOfFrame();
        }
    }


    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
