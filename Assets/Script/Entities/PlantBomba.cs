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
        try
        {
            GameObject[] monsters = GameObject.FindGameObjectsWithTag("Enemy");
            characterInRangeList = monsters.Where(x => ((Vector3)transform.position - x.transform.position).magnitude < AttRange).ToList();
            foreach (GameObject monster in monsters)
            {
                monster.GetComponent<Enemy>().TakeDamage(2);
            }
        }
        catch (Exception e)
        {

        }
    }
}
