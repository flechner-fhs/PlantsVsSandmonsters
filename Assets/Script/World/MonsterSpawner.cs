using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public List<GameObject> EnemyPrefabs;

    public List<WalkingPath> WalkingPaths;

    public float Interval = 1;
    private float Timer = 0;

    public List<(GameObject, WalkingPath)> SpawnQueue;

    public void SpawnWave(int size)
    {
        for (int i = 0; i < size; i++)
        {
            GameObject slot = EnemyPrefabs[Random.Range(0, EnemyPrefabs.Count)];
            WalkingPath path = WalkingPaths[Random.Range(0, WalkingPaths.Count)];

            SpawnQueue.Add((slot, path));
        }
    }

    private void Awake()
    {
        SpawnQueue = new List<(GameObject, WalkingPath)>();
    }

    private void Start()
    {
        SpawnWave(5);
    }

    private void Update()
    {
        Timer += Time.deltaTime;

        if(Timer > Interval && SpawnQueue.Count > 0)
        {
            GameObject enemy = Instantiate(SpawnQueue[0].Item1, SpawnQueue[0].Item2.GetStartPosition(), Quaternion.identity);
            enemy.GetComponent<Enemy>().Path = SpawnQueue[0].Item2;
            SpawnQueue.RemoveAt(0);
            Timer -= Interval;
        }
    }
}
