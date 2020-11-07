using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlantProjectile : Projectile
{

    // Update is called once per frame
    void Update()
    {
        TargetFinding tf = new TargetFinding();
        Vector2 direction;
        tf.findNextTarget(transform.position, out direction);
        direction = direction.normalized * MovementSpeed * Time.fixedDeltaTime;
        Rigidbody.MovePosition((Vector2)transform.position + direction);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(Damage);
        }
        Destroy(this.gameObject);

    }
}
