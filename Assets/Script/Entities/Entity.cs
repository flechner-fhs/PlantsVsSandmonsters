using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D), typeof(Rigidbody2D))]
public abstract class Entity : MonoBehaviour
{
    public string UnitName = "Entity";

    public float Health = 20;
    public float Damage = 1;
    public float Knockback = 5;
    [HideInInspector]
    public float Sleep = 0;

    public int Team = 0;

    public float MovementSpeed = 10;
    public float Size = 0.4f;

    public CircleCollider2D collider;
    public Rigidbody2D rigidbody;

    public bool DoesSleep { get => Sleep > 0;  }

    public void TakeDamage(float damage)
    {
        Health -= damage;

        if (Health <= 0)
            Destroy(gameObject);
    }

    void Start()
    {
        collider.radius = Size;
    }

    private void FixedUpdate()
    {
        if (!DoesSleep)
            Move();
        else
            Sleep -= Time.fixedDeltaTime;
    }

    abstract public void Move();

}
