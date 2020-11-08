using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlantProjectile : Projectile
{
    float cd = 0.5f;
    [HideInInspector]
    public GameObject target = null;
    Vector2 oldDirection = Vector2.zero;

    void Update()
    {
        if (cd >= 0.5)
        {
            TargetFinding tf = new TargetFinding();
            if (tf.findSpecialTarget(out Vector2 direction, target, transform.position))
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
        Rigidbody.MovePosition((Vector2)transform.position + oldDirection * 1.2f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(Damage);
        }
        if (!collision.gameObject.GetComponent<Plant>())
            Destroy(gameObject);
    }
}
