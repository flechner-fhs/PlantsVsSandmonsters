using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Projectile : MonoBehaviour
{
    [HideInInspector]
    public float MovementSpeed = 1;
    [HideInInspector]
    public float Damage;
    [HideInInspector]
    public Rigidbody2D Rigidbody;

    public void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
    }
}
