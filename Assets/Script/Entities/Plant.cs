using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Plant : Entity
{
    public PlantAssetMenu stats;
    public Sprite Sprite;
    public GameObject ThisPlantProjectile;
    public GameObject SweatDropPrefab;
    [Range(0, 1)]
    public float SweatThreshhold = .5f;


    [HideInInspector]
    public float WaterCost;
    [HideInInspector]
    public float AttRange;
    [HideInInspector]
    public Vector3 MenuOffset;
    private SweatDrop SweatDrop;
    private Vector3 SpawnOffset;
    private float ShootCooldown;
    private float ProjectileCost;
    private float MaxWaterReservoir;
    private float WaterReservoir;
    //whats priority
    private int Target;
    private float ProjectileSpeed;
    private bool isShoot;



    float cd = 0;
    float dmgPlantCd = 0;

    public new void Awake()
    {
        base.Awake();
        ReadStats();
    }

    public new void Start()
    {
        base.Start();
        transform.position += SpawnOffset;
        DrawRange(AttRange);
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
                ChangeWaterSupply(-ProjectileCost);
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

    private void ReadStats()
    {
        UnitName = stats.Name;
        Health = stats.Health;
        Damage = stats.Damage;
        Knockback = stats.Knockback * 1000;
        WaterCost = stats.WaterCost;
        AttRange = stats.AttackRange;
        SpawnOffset = (Vector3)stats.SpawnOffset;
        MenuOffset = (Vector3)stats.MenuOffset;
        isShoot = stats.Shooter;
        if (isShoot)
        {
            ShootCooldown = stats.ShootCooldown;
            ProjectileCost = stats.ProjectileCost;
            MaxWaterReservoir = stats.MaxWaterReservoir;
            ProjectileSpeed = stats.ProjectileSpeed;
            Target = stats.TargetSelection;
        }
    }

    private void Shoot()
    {
        TargetFinding tf = new TargetFinding();
        if (tf.FindATarget(out Vector2 direction, out GameObject obj, transform.position, Target, AttRange, "Enemy"))
        {
            GameObject thisProjectile = Instantiate(ThisPlantProjectile);
            thisProjectile.GetComponent<PlantProjectile>().Damage = Damage;
            thisProjectile.GetComponent<PlantProjectile>().MovementSpeed = ProjectileSpeed;
            thisProjectile.GetComponent<PlantProjectile>().AttRange = AttRange;
            thisProjectile.GetComponent<PlantProjectile>().Knockback = Knockback;
            thisProjectile.GetComponent<PlantProjectile>().target = obj;
            thisProjectile.GetComponent<PlantProjectile>().oldDirection = (Vector3)(direction.normalized * 1.1f);
            thisProjectile.GetComponent<PlantProjectile>().transform.position = transform.position;
            ChangeWaterSupply(-ProjectileCost);
        }
    }

    public void ChangeWaterSupply(float value)
    {
        WaterReservoir = Mathf.Clamp(WaterReservoir + value, 0, MaxWaterReservoir);

        if (!SweatDrop && SweatThreshhold > WaterReservoir / MaxWaterReservoir)
            SweatDrop = Instantiate(SweatDropPrefab, transform).GetComponent<SweatDrop>();
        else if (SweatDrop && SweatThreshhold < WaterReservoir / MaxWaterReservoir)
            Destroy(SweatDrop.gameObject);
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
