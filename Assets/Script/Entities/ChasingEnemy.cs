using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ChasingEnemy : Enemy
{
    PathFinder.Path Path;
    Entity Target;

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

            Vector3 nextTarget = Path.Directions[0];
            Vector3 direction = nextTarget - transform.position;

            if (direction.sqrMagnitude < 1f)
            {
                Path.Directions.Remove(nextTarget);

                RaycastHit2D hit = Physics2D.Raycast(transform.position, Target.transform.position - transform.position, (Target.transform.position - transform.position).magnitude, 1024 + 256);
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
        if (Path.Directions != null)
            Path.Directions.ForEach(x => Instantiate(Marker, x, Quaternion.identity));
    }

}
