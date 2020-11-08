using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ChasingEnemy : WalkingEnemy
{
    private PathFinder.Path ChasePath;
    private Entity Target;

    public float MaxChaseRange = 5;

    [Header("Visualise Chase Path")]

    public GameObject Marker;
    public bool ShowMarkers = false;

    private bool InChase = false;

    public override void Move()
    {
        if (Target == null)
            Target = Player;

        if((Target.transform.position - transform.position).sqrMagnitude > MaxChaseRange * MaxChaseRange)
        {
            if (InChase)
            {
                List<Waypoint> points = new List<Waypoint>(Path.waypoints);
                points.Sort((a, b) => (int)(((a.transform.position - transform.position).sqrMagnitude - (b.transform.position - transform.position).sqrMagnitude)*100));

                Progress = Path.waypoints.IndexOf(points.First());
                InChase = false;
            }


            base.Move();
            return;
        }

        InChase = true;

        if (ChasePath.Target == null || ChasePath.Directions == null || ChasePath.Directions.Count == 0)
        {
            ChasePath = new PathFinder.Path();
            Vector3 direction = Target.transform.position - transform.position;

            FacingLeft = direction.x < 0;

            direction = direction.normalized * MovementSpeed * Time.fixedDeltaTime;

            rigidbody.MovePosition(transform.position + direction);
        }
        else
        {
            ChasePath.age -= Time.fixedDeltaTime;
            if (ChasePath.age < 0)
                ChasePath = PathFinder.Instance.GetPathTo(gameObject, Target.gameObject);
            UpdateMarkers();

            if (ChasePath.Directions == null)
                return;
            Vector3 nextTarget = ChasePath.Directions[0];
            Vector3 direction = nextTarget - transform.position;

            if (direction.sqrMagnitude < 1f)
            {
                ChasePath.Directions.Remove(nextTarget);

                RaycastHit2D hit = Physics2D.Raycast(transform.position, Target.transform.position - transform.position, Mathf.Infinity, 512 + 1);
                if (hit && hit.collider && hit.collider.tag == "Player")
                {
                    ChasePath = new PathFinder.Path();
                    UpdateMarkers();
                }
            }

            FacingLeft = direction.x < 0;

            direction = direction.normalized * MovementSpeed * Time.fixedDeltaTime;

            rigidbody.MovePosition(transform.position + direction);
        }
    }


    private new void OnCollisionStay2D(Collision2D collision)
    {
        base.OnCollisionStay2D(collision);

        Entity other = collision.gameObject.GetComponent<Entity>();
        if (other && other.gameObject == Target)
        {
            ChasePath = new PathFinder.Path();
            UpdateMarkers();
        }
        else if (InChase && collision.gameObject.tag == "Obstacle" && Physics2D.Raycast(transform.position, Target.transform.position - transform.position, Mathf.Infinity, 512 + 1).collider.gameObject.tag != "Player")
        {
            ChasePath = PathFinder.Instance.GetPathTo(gameObject, Target.gameObject);
            UpdateMarkers();
        }
    }

    private void UpdateMarkers()
    {
        if (!ShowMarkers)
            return;
        GameObject.FindGameObjectsWithTag("Marker").ToList().ForEach(x => Destroy(x));
        if (ChasePath.Directions != null)
            ChasePath.Directions.ForEach(x => Instantiate(Marker, x, Quaternion.identity));
    }

}
