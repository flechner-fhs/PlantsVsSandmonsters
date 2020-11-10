using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnipyProjectile : PlantProjectile
{
    bool first = true;

    public new void Fly()
    {
        if (first)
        {
            first = false;
            TargetFinding tf = new TargetFinding();
            Vector2 direction = Vector2.zero;
            if (tf.FindSpecialTarget(ref direction, target, transform.position))
            {
                Rigidbody.MovePosition(direction.normalized * MovementSpeed * Time.fixedDeltaTime * AttRange);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        else if (lifeTime > 0)
        {
            Destroy(gameObject);
        }
    }
}
