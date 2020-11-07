using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class TargetFinding : MonoBehaviour
{
    private List<GameObject> characterInRangeList = new List<GameObject>();
    private List<GameObject> visibleCharacterList = new List<GameObject>();

    /**
     * prio -- what target (0 is closest)(1 is next visible)(2 is next visible else closest)(3 is strongest visible else closest)
     * 
     */

    public bool findTarget(out Vector2 direction, Vector2 pos, int enemyPrio = 0, int range = 10, string tag = "Enemy")
    {
        bool isTarget = false;
        updateCharacterList(pos, range);
        if (characterInRangeList.Count > 0)
        {
            switch (enemyPrio)
            {
                case 0:
                    direction = (Vector2)characterInRangeList[0].transform.position - pos;
                    isTarget = true;
                    break;
                case 1:
                    direction = Vector2.zero;
                    isTarget = false;
                    break;
                case 2:
                    direction = Vector2.zero;
                    isTarget = false;
                    break;
                case 3:
                    direction = Vector2.zero;
                    isTarget = false;
                    break;
                default:
                    direction = Vector2.zero;
                    isTarget = false;
                    break;
            }
        }
        else
        {
            direction = Vector2.zero;
        }
        return isTarget;

    }

    private void updateCharacterList(Vector2 pos, int range)
    {
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Enemy");
        Vector3 position = (Vector3)pos;
        characterInRangeList = monsters.Where(x => ((Vector3)pos - x.transform.position).magnitude < range).ToList().OrderBy(a => (a.transform.position - position).magnitude).ToList();
    }


    public void findNextTarget(Vector3 pos, out Vector2 direction)
    {
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closest = monsters[0];
        foreach (GameObject monster in monsters)
        {
            if ((monster.transform.position - pos).magnitude < (closest.transform.position - pos).magnitude)
            {
                closest = monster;
            }
        }
        direction = closest.transform.position - pos;
    }
}
