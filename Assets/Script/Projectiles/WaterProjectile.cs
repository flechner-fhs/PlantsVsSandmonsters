using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaterProjectile : Projectile
{
    public float PlantHeal = 1;
    public float TravelTime = 1;

    public Vector3 Direction;

    public new void Awake()
    {
        base.Awake();
        MovementSpeed = 60;
        Damage = 2;
        TravelTime = .5f;
    }

    private void FixedUpdate()
    {
        Vector3 direction = Direction.normalized * MovementSpeed * Time.fixedDeltaTime;
        Rigidbody.MovePosition(transform.position + direction);

        TravelTime -= Time.fixedDeltaTime;
        if (TravelTime <= 0)
            Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (new string[] { "Enemy", "Obstacle", "Plant" }.Contains(collision.gameObject.tag))
        {
            switch (collision.gameObject.tag)
            {
                case "Enemy":
                    collision.gameObject.GetComponent<Entity>().TakeDamage(Damage);
                    break;
                case "Plant":
                    collision.gameObject.GetComponent<Entity>().Health += PlantHeal;
                    break;
            }

            Destroy(gameObject);
        }

    }
}
