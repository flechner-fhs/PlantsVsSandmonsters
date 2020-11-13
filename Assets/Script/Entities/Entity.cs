using System.Collections;
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

    public enum TeamName { Player, Enemy }
    public TeamName Team = 0;

    public float MovementSpeed = 10;

    [HideInInspector]
    public Collider2D Collider;
    [HideInInspector]
    public Rigidbody2D Rigidbody;
    [HideInInspector]
    public SpriteRenderer Renderer;
    public Color BaseColor;

    [HideInInspector]
    public bool IsDead = false;

    [HideInInspector]
    public bool FacingLeft;

    public void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        Collider = GetComponent<Collider2D>();
        Renderer = GetComponentInChildren<SpriteRenderer>();
        BaseColor = Renderer.color;
    }

    public void Start()
    {
        Health = MaxHealth;
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
        StartCoroutine(Flash(new Color(0, .5f, .5f)));

        if (Health <= 0 && !IsDead)
        {
            IsDead = true;
            Die();
        }
    }

    private bool stillFlashing = false;
    public IEnumerator Flash(Color tint, float duration = .1f)
    {
        if (stillFlashing)
            yield break;
        stillFlashing = true;
        Renderer.color = new Color(Renderer.color.r - tint.r, Renderer.color.g - tint.g, Renderer.color.b - tint.b);
        yield return new WaitForSeconds(duration);
        Renderer.color = new Color(Renderer.color.r + tint.r, Renderer.color.g + tint.g, Renderer.color.b + tint.b);
        stillFlashing = false;
    }

    public void Heal(float healing)
    {
        Health = Mathf.Min(MaxHealth, Health + healing);
    }

    public void FixedUpdate()
    {
        if (!IsDead)
            Move();
    }

    abstract public void Move();
    abstract public void Die();

}
