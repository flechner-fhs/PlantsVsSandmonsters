using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
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
        DrawRange((float)AttRange);
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

    void DrawRange(float radius)
    {
        int vertexNumber = 50;
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = 0.01f;
        lineRenderer.endWidth = 0.01f;
        lineRenderer.loop = true;
        float angle = 2 * Mathf.PI / vertexNumber;
        lineRenderer.positionCount = vertexNumber;

        for (int i = 0; i < vertexNumber; i++)
        {
            Matrix4x4 rotationMatrix = new Matrix4x4(new Vector4(Mathf.Cos(angle * i), Mathf.Sin(angle * i), 0, 0),
                                                     new Vector4(-1 * Mathf.Sin(angle * i), Mathf.Cos(angle * i), 0, 0),
                                       new Vector4(0, 0, 1, 0),
                                       new Vector4(0, 0, 0, 1));
            Vector3 initialRelativePosition = new Vector3(0, radius, 0);
            lineRenderer.SetPosition(i, transform.position + rotationMatrix.MultiplyPoint(initialRelativePosition));

        }
    }
}
