using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlantProjectile : Projectile
{
    float cd = 0.5f;
    Vector2 oldDirection = Vector2.zero;

    void Update()
    {
        if (cd >= 0.5)
        {
            MovementSpeed = 2;
            TargetFinding tf = new TargetFinding();
            if (tf.findTarget(out Vector2 direction, transform.position, 0, 10, "Enemy"))
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
        Rigidbody.MovePosition((Vector2)transform.position + oldDirection);
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
