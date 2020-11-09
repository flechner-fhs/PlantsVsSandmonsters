using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using UnityEngine;

public class PlantBomba : Plant
{

    private List<GameObject> characterInRangeList = new List<GameObject>();

    public override void Die()
    {
        base.Die();

        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Enemy");
        characterInRangeList = monsters.Where(x => (transform.position - x.transform.position).sqrMagnitude < AttRange * AttRange).ToList();
        foreach (GameObject monster in characterInRangeList)
        {
            monster.GetComponent<Enemy>().TakeDamage(Damage);
            monster.GetComponent<Enemy>().Rigidbody.AddForce((monster.GetComponent<Enemy>().transform.position - transform.position).normalized * Knockback);
        }

    }
}
