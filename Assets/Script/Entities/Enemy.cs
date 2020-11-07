using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : Entity
{
    [HideInInspector]
    public Player Player;
    [HideInInspector]
    public Core Core;

    [Header("Enemy Stats")]

    public float AttackSleep = .5f;

    private void Start()
    {
        Player = FindObjectOfType<Player>();
        Core = FindObjectOfType <Core> ();
    }

    public void Attack(Entity other)
    {
        other.TakeDamage(Damage);

        other.rigidbody.AddForce((other.transform.position - transform.position).normalized * Knockback);
        Sleep = AttackSleep;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Entity other = collision.gameObject.GetComponent<Entity>();
        if (other && other.Team != Team)
        {
            Attack(other);
        }
    }

}
