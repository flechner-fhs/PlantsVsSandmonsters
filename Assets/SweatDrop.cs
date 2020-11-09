using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SweatDrop : MonoBehaviour
{
    public float Range = .1f;
    public float Speed = 5;

    private Vector3 StartPos;
    private float Progress = 0;

    private void Awake()
    {
        StartPos = transform.position;
    }

    void Update()
    {
        Progress += Time.deltaTime * Speed;
        transform.position = new Vector3(StartPos.x, StartPos.y + Mathf.Sin(Progress) * Range, 0);
    }
}
