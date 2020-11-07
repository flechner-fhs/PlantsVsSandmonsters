using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFinding
{
    public void findNextTarget(Vector3 pos, out Vector2 direction)
    {
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Enemy");
        if(monsters.Length > 0)
        {
            GameObject closest = monsters[0];
            foreach (GameObject monster in monsters)
            {
                if ((monster.transform.position - pos).magnitude < (closest.transform.position - pos).magnitude)
                {
                    closest = monster;
                }
            }
            direction = closest.transform.position - pos;
            return;
        }
        direction = Vector2.zero;
    }
}
