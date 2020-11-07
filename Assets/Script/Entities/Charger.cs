using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using System.Linq;

public class Charger : WalkingEnemy
{

    // Update is called once per frame
    float cd = 0;
    public float Range = 2;
    public void Update()
    {
        cd += Time.deltaTime;
        if (cd >= 1)
        {
            if (Health > 1)
            {
                MovementSpeed += 1;
                Health -= 1;
            }
            cd = 0;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Entity entity = collision.gameObject.GetComponent<Entity>();
        if (entity && entity.Team != Team)
        {
            TakeDamage(Health);
        }
    }
    public override void Die()
    {
        base.Die();
        FindObjectsOfType<Entity>()
            .Where(x => x.Team != Team)
            .Where(x => (x.transform.position - transform.position).magnitude < Range)
            .ToList()
            .ForEach(x => x.TakeDamage(Damage));
    }
}
