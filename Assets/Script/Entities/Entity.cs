using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D), typeof(Rigidbody2D))]
public abstract class Entity : MonoBehaviour
{
    [Header("Basic Stats")]
    public string UnitName = "Entity";

    public float MaxHealth = 20;
    public float Health = 20;
    public float Damage = 1;
    public float Knockback = 500;

    public int Team = 0;

    public float MovementSpeed = 10;
    public float Size = 0.4f;

    [HideInInspector]
    public CircleCollider2D collider;
    [HideInInspector]
    public Rigidbody2D rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<CircleCollider2D>();
        Health = MaxHealth;
    }

    void Start()
    {
        collider.radius = Size;
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;

        if (Health <= 0)
            Die();
    }

    public void Heal(float healing)
    {
        Health = Mathf.Min(MaxHealth, Health + healing);
    }

    public void FixedUpdate()
    {
        Move();
    }

    abstract public void Move();
    abstract public void Die();

}
