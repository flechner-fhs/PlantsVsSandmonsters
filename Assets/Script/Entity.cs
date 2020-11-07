using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D), typeof(Rigidbody2D))]
public abstract class Entity : MonoBehaviour
{
    public int Health;
    public float MovementSpeed;
    public float Size;

    public CircleCollider2D collider;
    public Rigidbody2D rigidbody;

    void Start()
    {
        collider.radius = Size;
    }

    private void FixedUpdate()
    {
        Move();
    }

    abstract public void Move();

}
