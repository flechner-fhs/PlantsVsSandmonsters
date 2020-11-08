using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartMonsterWave : MonsterWave
{
    public List<int> MonsterAmounts;

    private void Awake()
    {
        List<(GameObject, int)> MonsterData = new List<(GameObject, int)>();

        for(int i = 0; i < Monsters.Count; i++)
        {
            int amount = MonsterAmounts.Count <= i ? 1 : MonsterAmounts[i];
            MonsterData.Add((Monsters[i], amount));
        }

        Monsters.Clear();

        while(MonsterData.Count > 0)
        {
            int i = Random.Range(0, MonsterData.Count);
            Monsters.Add(MonsterData[i].Item1);
            MonsterData[i] = (MonsterData[i].Item1, MonsterData[i].Item2-1);
            if (MonsterData[i].Item2 <= 0)
                MonsterData.RemoveAt(i);
        }
    }
}
