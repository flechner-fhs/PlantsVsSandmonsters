using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingEnemy : Enemy
{
    [Header("Walking Path")]

    public WalkingPath Path;
    [HideInInspector]
    public int Progress = 0;

    public float PathTolerance = .02f;

    public override void Move()
    {
        if (Progress >= Path.waypoints.Count)
            return;

        Waypoint nextPoint = Path.waypoints[Progress];
        Vector3 direction = nextPoint.Position - transform.position;

        Debug.Log(direction.sqrMagnitude);
        if (direction.sqrMagnitude < PathTolerance)
        {
            Progress++;
            nextPoint = Path.waypoints[Progress];
            direction = nextPoint.Position - transform.position;
        }
        FacingLeft = direction.x < 0;

        direction = direction.normalized * MovementSpeed * Time.fixedDeltaTime;

        rigidbody.MovePosition(transform.position + direction);
    }
}
