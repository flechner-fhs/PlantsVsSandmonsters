using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : Entity
{
    public float ShootCooldown;

    public Sprite Sprite;
    public float WaterCost = 10;

    public GameObject projectile;
    public int waterReservoir;
    float cd = 0;

    public Vector3 SpawnOffset;

    public new void Start()
    {
        base.Start();
        transform.position += SpawnOffset;
    }

    public override void Move()
    {
        cd += Time.fixedDeltaTime;

        if (cd >= ShootCooldown)
        {
            if (waterReservoir > 0)
            {
                Shoot();
            }
            else
            {
                TakeDamage(1);
            }
            cd = 0;
        }
    }

    private void Shoot()
    {
        TargetFinding tf = new TargetFinding();
        tf.findNextTarget(transform.position, out Vector2 direction);
        GameObject projectilexy = GameObject.Instantiate(projectile);
        projectilexy.GetComponent<PlantProjectile>().Damage = Damage;
        projectilexy.transform.position = transform.position + (Vector3)(direction.normalized * Size);
        waterReservoir--;
    }


    public override void Die()
    {
        Destroy(gameObject);
    }
}
