using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFinding : MonoBehaviour
{
    public void findNextTarget(out Vector2 direction, Vector3 pos)
    {
        GameObject monster = GameObject.Find("LilMonster");
        direction = monster.transform.position - pos;
    }
}
