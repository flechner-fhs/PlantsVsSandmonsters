using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MrClusterSmallProjectile : PlantProjectile
{
    bool first = true;
    public Vector2 direction;

    public new void Fly()
    {
        if (first)
        {
            first = false;
            Rigidbody.MovePosition(direction.normalized * Time.fixedDeltaTime * AttRange);
        }
        else if (lifeTime > 0)
        {
            Destroy(gameObject);
        }
        lifeTime -= Time.fixedDeltaTime;
    }
}
