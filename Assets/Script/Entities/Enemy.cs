using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy : Entity
{
    //PathFinder.Path Path;
    public WalkingPath Path;
    [HideInInspector]
    public int Progress = 0;

    public float PointTolerance = .02f;

    Player Player;
    Core Core;

    public float AttackSleep = .5f;

    /*
    public GameObject Marker;
    public bool ShowMarkers = false;
    */

    private void Start()
    {
        Player = FindObjectOfType<Player>();
        Core = FindObjectOfType <Core> ();
    }

    public void Attack(Entity other)
    {
        other.TakeDamage(Damage);

        other.rigidbody.AddForce((other.transform.position - transform.position).normalized * Knockback);
        Sleep = AttackSleep;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Entity other = collision.gameObject.GetComponent<Entity>();
        if (other && other.Team != Team)
        {
            Attack(other);
        }
    }

    public override void Move()
    {
        if (Progress >= Path.waypoints.Count)
            return;

        Waypoint nextPoint = Path.waypoints[Progress];
        Vector3 direction = nextPoint.Position - transform.position;

        if(direction.sqrMagnitude < PointTolerance)
        {
            Progress++;
            nextPoint = Path.waypoints[Progress];
            direction = nextPoint.Position - transform.position;
        }
            
        direction = direction.normalized * MovementSpeed * Time.fixedDeltaTime;

        rigidbody.MovePosition(transform.position + direction);
    }


    /*
    public override void Move()
    {
        if (Target == null)
            Target = Player;

        if (Path.Target == null || Path.Directions == null || Path.Directions.Count == 0)
        {
            Path = new PathFinder.Path();
            Vector3 direction = Target.transform.position - transform.position;
            direction = direction.normalized * MovementSpeed * Time.fixedDeltaTime;

            rigidbody.MovePosition(transform.position + direction);
        }
        else
        {
            Path.age -= Time.fixedDeltaTime;
            if(Path.age < 0)
                Path = PathFinder.Instance.GetPathTo(gameObject, Target.gameObject);
            UpdateMarkers();

            Vector3 nextTarget = Path.Directions[0];
            Vector3 direction = nextTarget - transform.position;

            if (direction.sqrMagnitude < 1f)
            {
                Path.Directions.Remove(nextTarget);

                RaycastHit2D hit = Physics2D.Raycast(transform.position, Target.transform.position - transform.position, (Target.transform.position - transform.position).magnitude, 1024+256);
                if (!hit || !hit.collider || hit.collider.tag != "Obstacle")
                {
                    Debug.Log("Delete Path");
                    Debug.Log(hit.collider);
                    Path = new PathFinder.Path();
                }
            }

            direction = direction.normalized * MovementSpeed * Time.fixedDeltaTime;

            rigidbody.MovePosition(transform.position + direction);
        }
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        Entity other = collision.gameObject.GetComponent<Entity>();
        if (other && other.gameObject == Target)
        {
            Path = new PathFinder.Path();
        }
        else if (collision.gameObject.tag == "Obstacle" && Physics2D.Raycast(transform.position, Target.transform.position - transform.position, 1, 1024 + 256))
        {
            Path = PathFinder.Instance.GetPathTo(gameObject, Target.gameObject);
            UpdateMarkers();
        }
    }

    private void UpdateMarkers()
    {
        if (!ShowMarkers) 
            return;
        GameObject.FindGameObjectsWithTag("Marker").ToList().ForEach(x => Destroy(x));
        if(Path.Directions != null)
            Path.Directions.ForEach(x => Instantiate(Marker, x, Quaternion.identity));
    }
    */
}
