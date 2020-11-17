using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UIElements;

public class PlantProjectile : Projectile
{
    [HideInInspector]
    public float AttRange;
    [HideInInspector]
    public float Knockback;
    [HideInInspector]
    public GameObject target = null;
    [HideInInspector]
    public Vector2 oldDirection = Vector2.zero;

    protected float lifeTime;
    float targetCooldown = 0.5f;
    float cd = 10f;
    void Start()
    {
        lifeTime = AttRange / MovementSpeed * 5;
        targetCooldown = 1 / MovementSpeed;
    }

    void Update()
    {
        Fly();
    }

    public void Fly()
    {
        if (lifeTime > 0)
        {
            if (cd >= targetCooldown)
            {
                TargetFinding tf = new TargetFinding();
                Vector2 direction = oldDirection;
                if (tf.FindSpecialTarget(ref direction, target, transform.position))
                {

                    direction = direction.normalized * MovementSpeed * Time.fixedDeltaTime;
                    oldDirection = direction;
                }

                cd = 0;
            }
            else
            {
                cd += Time.fixedDeltaTime;
            }

            Debug.DrawRay(transform.position, oldDirection, Color.cyan, 1);
            if (((Vector2)transform.position + oldDirection).magnitude < 10000)
            {
                Rigidbody.MovePosition((Vector2)transform.position + oldDirection);
            }
            else
            {
                Destroy(gameObject);
            }
            lifeTime -= Time.fixedDeltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject obj = collision.gameObject;
        if (obj.tag == "Enemy")
        {
            obj.GetComponent<Enemy>().TakeDamage(Damage);
            obj.GetComponent<Enemy>().Rigidbody.AddForce((obj.GetComponent<Enemy>().transform.position - transform.position).normalized * Knockback);
        }

        if (!obj.GetComponent<Plant>() && !obj.GetComponent<Projectile>() && !obj.GetComponent<Player>())
            Destroy(gameObject);
    }
}
