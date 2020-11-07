using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WalkingPath : MonoBehaviour
{
    [HideInInspector]
    public List<Waypoint> waypoints;

    void Start()
    {
        waypoints = GetComponentsInChildren<Waypoint>().ToList();
        waypoints.Sort((x, y) => x.Index - y.Index);
    }

    public Vector3 GetStartPosition() => waypoints[0].transform.position;
}
