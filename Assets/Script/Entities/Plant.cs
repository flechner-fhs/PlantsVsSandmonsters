using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : Entity
{
    public float ShootCooldown;
    public float WaterCost = 10;
    public float ProjectileCost;
    public float MaxWaterReservoir = 30;
    public float WaterReservoir;
    //whats priority
    public int Target = 1;
    public float ProjectileSpeed;
    public int AttRange;
    public Sprite Sprite;
    public GameObject ThisPlantProjectile;
    public bool isShoot = true;
    float cd = 0;
    float dmgPlantCd = 0;

    public Vector3 SpawnOffset;
    public Vector3 MenuOffset;

    public new void Start()
    {
        base.Start();
        transform.position += SpawnOffset;
        WaterReservoir = MaxWaterReservoir;
    }

    public override void Move()
    {
        cd += Time.fixedDeltaTime;
        if (cd >= ShootCooldown && isShoot)
        {

            if (WaterReservoir > ProjectileCost)
            {
                Shoot();
            }
            else if (WaterReservoir > 0 && dmgPlantCd > 1)
            {
                ChangeWaterSupply(-1);
                dmgPlantCd = 0;
            }
            else if (WaterReservoir > 0)
            {
                dmgPlantCd += cd;
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
        if (tf.findATarget(out Vector2 direction, out GameObject obj, transform.position, Target, AttRange, "Enemy"))
        {
            GameObject thisProjectile = Instantiate(ThisPlantProjectile);
            thisProjectile.GetComponent<PlantProjectile>().Damage = Damage;
            thisProjectile.GetComponent<PlantProjectile>().MovementSpeed = ProjectileSpeed;
            thisProjectile.GetComponent<PlantProjectile>().AttRange = AttRange;
            thisProjectile.GetComponent<PlantProjectile>().target = obj;
            thisProjectile.GetComponent<PlantProjectile>().oldDirection = transform.position + (Vector3)(direction.normalized * 1.1f);
            thisProjectile.transform.position = transform.position + (Vector3)(direction.normalized * 1.1f);
            WaterReservoir -= ProjectileCost;
        }
    }



    public void ChangeWaterSupply(float value)
    {
        WaterReservoir = Mathf.Clamp(WaterReservoir + value, 0, MaxWaterReservoir);
    }

    public override void Die()
    {
        Destroy(gameObject);
    }
}
