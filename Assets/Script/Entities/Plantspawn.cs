using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plantspawn : MonoBehaviour
{
    public float ShootCooldown;
    public float Size;
    public int Damage;
    public int Health;

    public GameObject projectile;
    public int waterReservoir;
    float cd = 0;

    void Start()
    {

    }

    void Update()
    {
        cd += Time.deltaTime;

        if (cd >= ShootCooldown)
        {
            if (waterReservoir > 0)
            {
                Shoot();
            }
            else
            {
                Health--;
            }
            cd = 0;
        }

        if (Health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void Shoot()
    {
        TargetFinding tf = new TargetFinding();
        if (tf.findTarget(out Vector2 direction, transform.position, 0, 10, "Enemy"))
        {
            GameObject projectilexy = GameObject.Instantiate(projectile);
            projectilexy.GetComponent<PlantProjectile>().Damage = Damage;
            projectilexy.transform.position = transform.position + (Vector3)(direction.normalized * Size);
            waterReservoir--;
        }

    }
}
