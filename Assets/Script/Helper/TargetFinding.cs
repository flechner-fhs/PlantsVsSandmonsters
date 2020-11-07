using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class TargetFinding
{
    private List<GameObject> characterInRangeList = new List<GameObject>();
    private List<GameObject> visibleCharacterList = new List<GameObject>();

    /**
     * prio -- what target 
     * (0 is closest)
     * (1 is next visible)
     * (2 is strongest visible)
     * (3 is strongest(visible idc))
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
                    updateVisibleList(pos, range);
                    if (visibleCharacterList.Count > 0)
                    {
                        direction = (Vector2)visibleCharacterList[0].transform.position - pos;
                        isTarget = true;
                    }
                    else
                    {
                        direction = Vector2.zero;
                        isTarget = false;
                    }
                    break;

                case 2:
                    updateVisibleList(pos, range);
                    if (visibleCharacterList.Count > 0)
                    {
                        sortAfterStrongest(true);
                        direction = (Vector2)visibleCharacterList[0].transform.position - pos;
                        isTarget = true;
                    }
                    else
                    {
                        direction = Vector2.zero;
                        isTarget = false;
                    }
                    break;

                case 3:
                    sortAfterStrongest(false);
                    direction = (Vector2)visibleCharacterList[0].transform.position - pos;
                    isTarget = true;
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

    private void updateVisibleList(Vector2 pos, int range)
    {
        foreach (GameObject character in characterInRangeList)
        {
            Debug.DrawRay(pos, ((Vector2)character.transform.position - pos));
            if (Physics2D.Raycast(pos, ((Vector2)character.transform.position - pos)))
            {
                visibleCharacterList.Add(character);
            }
        }
    }

    private void sortAfterStrongest(bool visible)
    {
        if (visible)
        {
            visibleCharacterList.OrderBy(x => (getPrio(x)));
        }
        else
        {
            characterInRangeList.OrderBy(x => (getPrio(x)));
        }
    }

    private int getPrio(GameObject obj)
    {
        float strength = 0;

        strength += obj.GetComponent<Enemy>().Damage;
        strength += obj.GetComponent<Enemy>().Health;
        strength += 2 / obj.GetComponent<Enemy>().AttackSleep;
        Debug.Log("PRIO STRENGTH: " + strength);
        return (int)strength;
    }
}
