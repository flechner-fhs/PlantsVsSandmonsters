using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MrClusterProjectile : PlantProjectile
{
    public GameObject ThisProjProj;

    protected new void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        GameObject obj = collision.gameObject;

        if (!obj.GetComponent<Plant>() || !obj.GetComponent<Projectile>() || !obj.GetComponent<Player>())
        {
            CreateProjectile(1, 1);
            CreateProjectile(-1, -1);
            CreateProjectile(0, -1);
            CreateProjectile(-1, 1);
            CreateProjectile(1, -1);
        }
    }
    private void CreateProjectile(int x, int y)
    {
        Vector2 direction = new Vector2(x, y);
        GameObject thisProjectile = Instantiate(ThisProjProj);
        thisProjectile.GetComponent<MrClusterSmallProjectile>().Damage = Damage / 7;
        thisProjectile.GetComponent<MrClusterSmallProjectile>().direction = direction;
        thisProjectile.GetComponent<MrClusterSmallProjectile>().AttRange = 2;
        thisProjectile.GetComponent<MrClusterSmallProjectile>().MovementSpeed = MovementSpeed;
        thisProjectile.GetComponent<MrClusterSmallProjectile>().target = target;
        thisProjectile.transform.position = transform.position + (Vector3)(direction.normalized * 0.5f);
        thisProjectile.GetComponent<MrClusterSmallProjectile>().Rigidbody.MovePosition(transform.position + (Vector3)(direction.normalized * 1.1f));
    }
}
