using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : Entity
{
    public static List<Enemy> Enemies = new List<Enemy>();

    [HideInInspector]
    public Player Player;
    [HideInInspector]
    public Core Core;

    [Header("Enemy Stats")]

    public float AttackSleep = .5f;
    [HideInInspector]
    public float Sleep = 0;

    public Drop Drop;
    public float DropChance = 0.1f;

    private float EscapeTimer = 3;

    public bool DoesSleep { get => Sleep > 0; }

    public new void Awake()
    {
        base.Awake();

        Enemies.Add(this);
    }

    public new void Start()
    {
        base.Start();
        Player = FindObjectOfType<Player>();
        Core = FindObjectOfType<Core>();
    }

    public void Attack(Entity other)
    {
        other.TakeDamage(Damage);

        other.Rigidbody.AddForce((other.transform.position - transform.position).normalized * Knockback);
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        if (!DoesSleep)
        {
            Entity other = collision.gameObject.GetComponent<Entity>();
            if (other && other.Team != Team)
            {
                Attack(other);
                Sleep = AttackSleep;
            }
        }
        if (collision.gameObject.tag == "Obstacle")
        {
            EscapeTimer -= Time.deltaTime;

            if (EscapeTimer <= 0)
            {
                TakeDamage(1);
                EscapeTimer += .5f; ;
            }

        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Obstacle" && EscapeTimer < 2)
        {
            EscapeTimer = 3;
        }
    }

    public new void FixedUpdate()
    {
        base.FixedUpdate();

        if (DoesSleep)
            Sleep -= Time.fixedDeltaTime;
    }

    public override void Die()
    {
        if (Drop && Random.Range(0f, 1f) < DropChance)
            Instantiate(Drop, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        Enemies.Remove(this);
    }

}
