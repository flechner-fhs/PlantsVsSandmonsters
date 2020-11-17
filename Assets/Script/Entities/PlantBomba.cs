using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlantBomba : Plant
{

    private List<GameObject> characterInRangeList = new List<GameObject>();

    public List<Sprite> DeathAnimation;

    public override void Die()
    {
        StartCoroutine(DeathAnimator());
    }

    private void KillAllInRange()
    {
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Enemy");
        characterInRangeList = monsters.Where(x => (transform.position - x.transform.position).sqrMagnitude < AttRange * AttRange).ToList();
        foreach (GameObject monster in characterInRangeList)
        {
            monster.GetComponent<Enemy>().TakeDamage(Damage);
            monster.GetComponent<Enemy>().Rigidbody.AddForce((monster.GetComponent<Enemy>().transform.position - transform.position).normalized * Knockback);
        }
    }

    IEnumerator DeathAnimator()
    {
        foreach (var sprite in DeathAnimation)
        {
            yield return new WaitForSeconds(.05f);
            Renderer.sprite = sprite;
            if (DeathAnimation.IndexOf(sprite) == DeathAnimation.Count() / 2)
                KillAllInRange();
        }
        yield return new WaitForSeconds(.05f);
        base.Die();
    }
}
