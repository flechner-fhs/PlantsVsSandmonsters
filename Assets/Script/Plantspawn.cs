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
    float cd;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
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
        Vector2 direction;
        tf.findNextTarget(transform.position, out direction);
        GameObject projectilexy = GameObject.Instantiate(projectile);
        projectilexy.GetComponent<PlantProjectile>().Damage = Damage;
        projectilexy.transform.position = transform.position + (Vector3)(direction.normalized * Size);
        waterReservoir--;
    }
}
