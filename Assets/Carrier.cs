using System.Linq;
using UnityEngine;

public class Carrier : WalkingEnemy
{
    [Header("Carrier Specific")]
    public GameObject ExplosionEffect;

    public float Range = 2;

    public int SpawnAmount = 5;
    public GameObject SpawnPrefab;

    public override void Die()
    {
        base.Die();
        GameObject go = Instantiate(ExplosionEffect, transform.position, Quaternion.identity);
        go.transform.localScale = Vector3.one * Range;

        FindObjectsOfType<Entity>()
            .Where(x => x.Team != Team)
            .Where(x => (x.transform.position - transform.position).magnitude < Range)
            .ToList()
            .ForEach(x => x.TakeDamage(Damage));

        for (int i = 0; i < SpawnAmount; i++)
        {
            GameObject spawn = Instantiate(SpawnPrefab, transform.position + Vector3.left * Random.Range(-1, 1f) * Range / 2 + Vector3.up * Random.Range(-1, 1f) * Range / 2, Quaternion.identity);
            WalkingEnemy wEnemy = spawn.GetComponent<WalkingEnemy>();
            if (wEnemy)
            {
                wEnemy.Path = Path;
                wEnemy.Progress = Progress;
            }
        }
    }
}
