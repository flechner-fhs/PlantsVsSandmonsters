using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public int Index = 0;

    public Vector3 Position { get => transform.position; }

    void Awake()
    {
        GetComponentInChildren<SpriteRenderer>().enabled = false;
    }
}
