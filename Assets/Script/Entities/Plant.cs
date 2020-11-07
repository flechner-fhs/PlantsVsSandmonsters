using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : Entity
{
    public float ShootCooldown;
    public float WaterCost = 10;
    public float ProjectileCost;
    public float WaterReservoir;
    //
    public int Target = 1;
    public int AttRange;
    public Sprite Sprite;
    public GameObject ThisPlantProjectile;
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
            if (WaterReservoir > ProjectileCost)
            {
                Shoot();
            }
            else if (WaterReservoir > 0)
            {
                WaterReservoir--;
            }
            else
            {
                TakeDamage(ProjectileCost);
            }
            cd = 0;
        }
    }

    private void Shoot()
    {
        TargetFinding tf = new TargetFinding();
        if (tf.findTarget(out Vector2 direction, transform.position, Target, AttRange, "Enemy"))
        {
            GameObject thisProjectile = Instantiate(ThisPlantProjectile);
            thisProjectile.GetComponent<Projectile>().Damage = Damage;
            thisProjectile.transform.position = transform.position + (Vector3)(direction.normalized * Size);
            WaterReservoir -= ProjectileCost;
        }
    }

    public override void Die()
    {
        Destroy(gameObject);
    }
}
