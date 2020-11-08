using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
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

    [HideInInspector]
    public Collider2D collider;
    [HideInInspector]
    public Rigidbody2D rigidbody;

    [HideInInspector]
    public bool IsDead = false;

    [HideInInspector]
    public bool FacingLeft;

    public void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
    }

    public void Start()
    {
        Health = MaxHealth;
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;

        if (Health <= 0 && !IsDead)
        {
            IsDead = true;
            Die();
        }
    }

    public void Heal(float healing)
    {
        Health = Mathf.Min(MaxHealth, Health + healing);
    }

    public void FixedUpdate()
    {
        if(!IsDead)
            Move();
    }

    abstract public void Move();
    abstract public void Die();

}
