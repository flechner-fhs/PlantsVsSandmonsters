using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public List<GameObject> EnemyPrefabs;

    public List<float> EnemyProbability;

    public List<WalkingPath> WalkingPaths;

    public float Interval = 1;
    private float Timer = 0;

    public List<(GameObject, WalkingPath)> SpawnQueue;

    public bool EndlessMode = false;

    public void SpawnWave(int size)
    {
        float chanceTotal = EnemyProbability.Sum();
        for (int i = 0; i < size; i++)
        {
            float draw = Random.Range(0, chanceTotal);
            float val = 0;

            for(int j = 0; j < EnemyPrefabs.Count; j++)
            {
                if (draw >= val && draw <= val + EnemyProbability[j])
                {
                    GameObject slot = EnemyPrefabs[j];
                    WalkingPath path = WalkingPaths[Random.Range(0, WalkingPaths.Count)];

                    SpawnQueue.Add((slot, path));
                    break;
                }
                val += EnemyProbability[j];
            }
        }
    }

    private void Awake()
    {
        GetComponentInChildren<SpriteRenderer>().gameObject.SetActive(false);

        SpawnQueue = new List<(GameObject, WalkingPath)>();

        while (EnemyProbability.Count < EnemyPrefabs.Count)
            EnemyProbability.Add(1);
    }

    private void Start()
    {
        SpawnWave(10);
    }

    private void Update()
    {
        Timer += Time.deltaTime;

        if(Timer > Interval && SpawnQueue.Count > 0)
        {
            GameObject enemy = Instantiate(SpawnQueue[0].Item1, SpawnQueue[0].Item2.GetStartPosition(), Quaternion.identity);
            WalkingEnemy wEnemy = enemy.GetComponent<WalkingEnemy>();
            if(wEnemy)
                wEnemy.Path = SpawnQueue[0].Item2;
            SpawnQueue.RemoveAt(0);
            Timer -= Interval;

            if (EndlessMode && SpawnQueue.Count == 0)
                StartCoroutine(AddWaveIn(5));
        }
    }

    IEnumerator AddWaveIn(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SpawnWave(10);
    }
}
