using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public List<MonsterWave> Waves;

    public enum Stage { Rain, Prepare, Wave }
    public Stage WaveStage = Stage.Rain;

    public int Wave = 0;

    public Player Player;

    public float RainDuration = 5;
    public float PrepareDuration = 10;

    public Text Infobox;

    private void Start()
    {
        StopAllCoroutines();

        if (Waves.Count == 0)
            Waves = GetComponents<MonsterWave>().ToList();

        Waves.Sort((a, b) => a.Index - b.Index);

        NextStage();
    }

    public void NextStage()
    {
        WaveStage = (Stage)(((int)WaveStage + 1) % 3);

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
        Infobox.text = "Rain Stage " + (Wave + 1);
        Player.WaterSupply = 100;
        yield return new WaitForSeconds(RainDuration);
        NextStage();
    }
    public IEnumerator StartPreparation()
    {
        Infobox.text = "Preparation Stage " + (Wave + 1);
        yield return new WaitForSeconds(PrepareDuration);
        NextStage();
    }
    public IEnumerator StartWave()
    {
        Infobox.text = "Wave " + (Wave + 1);
        MonsterWave[] waves = Waves.Where(x => x.Index == Wave).ToArray();
        waves.ToList().ForEach(x => x.Spawner.SpawnWave(x.Monsters));
        yield return new WaitForSeconds(5);
        yield return new WaitUntil(() => Enemy.Enemies.Count == 0);
        Waves.RemoveAll(x => waves.Contains(x));
        Wave++;

        if(Waves.Count == 0)
        {
            SceneManager.LoadScene("TitleScene");
        }
        else
            NextStage();
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
