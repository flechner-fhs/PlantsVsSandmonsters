using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/PlantStats")]
public class PlantAssetMenu : ScriptableObject
{
    public string Name;
    public float Health;
    public float Damage;
    public float Knockback;
    public float WaterCost;
    public float AttackRange;
    public Vector2 SpawnOffset;
    public Vector2 MenuOffset;

    public bool Shooter;
    public float ShootCooldown;
    public float ProjectileCost;
    public float MaxWaterReservoir;
    public float ProjectileSpeed;
    public int TargetSelection;
}
