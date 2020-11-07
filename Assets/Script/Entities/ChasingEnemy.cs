using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ChasingEnemy : Enemy
{
    private PathFinder.Path Path;
    private Entity Target;

    [Header("Visualise Chase Path")]

    public GameObject Marker;
    public bool ShowMarkers = false;

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
            if (Path.age < 0)
                Path = PathFinder.Instance.GetPathTo(gameObject, Target.gameObject);
            UpdateMarkers();

            if (Path.Directions == null)
                return;
            Vector3 nextTarget = Path.Directions[0];
            Vector3 direction = nextTarget - transform.position;

            if (direction.sqrMagnitude < 1f)
            {
                Path.Directions.Remove(nextTarget);

                RaycastHit2D hit = Physics2D.Raycast(transform.position, Target.transform.position - transform.position, Mathf.Infinity, 512 + 1);
                if (hit && hit.collider && hit.collider.tag == "Player")
                {
                    Path = new PathFinder.Path();
                    UpdateMarkers();
                }
            }

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
            Path = new PathFinder.Path();
            UpdateMarkers();
        }
        else if (collision.gameObject.tag == "Obstacle" && Physics2D.Raycast(transform.position, Target.transform.position - transform.position, Mathf.Infinity, 512 + 1).collider.gameObject.tag != "Player")
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
        if (Path.Directions != null)
            Path.Directions.ForEach(x => Instantiate(Marker, x, Quaternion.identity));
    }

}
