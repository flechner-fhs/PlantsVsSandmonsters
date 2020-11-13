using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TargetFinding
{
    private List<GameObject> characterInRangeList;
    private List<GameObject> visibleCharacterList;
    private bool showRayCasts;

    /**
     * prio -- what target 
     * (0 is closest)
     * (1 is next visible)
     * (2 is strongest visible)
     * (3 is strongest(visible idc))
     */
    public TargetFinding()
    {
        characterInRangeList = new List<GameObject>();
        visibleCharacterList = new List<GameObject>();
        showRayCasts = false;
    }

    public bool FindATarget(out Vector2 direction, out GameObject obj, Vector2 pos, int enemyPrio = 1, float range = 10, string tag = "Enemy")
    {
        bool isTarget = false;
        UpdateCharacterList(pos, range, tag);
        if (characterInRangeList.Count > 0)
        {
            switch (enemyPrio)
            {
                case 0:
                    if (showRayCasts)
                        Debug.DrawRay(pos, ((Vector2)characterInRangeList[0].transform.position - pos), Color.red, 0.5f);
                    direction = (Vector2)characterInRangeList[0].transform.position - pos;
                    obj = characterInRangeList[0];
                    isTarget = true;
                    break;

                case 1:
                    UpdateVisibleList(pos, range);
                    if (visibleCharacterList.Count > 0)
                    {
                        if (showRayCasts)
                            Debug.DrawRay(pos, ((Vector2)visibleCharacterList[0].transform.position - pos), Color.blue, 0.5f);
                        direction = (Vector2)visibleCharacterList[0].transform.position - pos;
                        obj = visibleCharacterList[0];
                        isTarget = true;
                    }
                    else
                    {
                        direction = Vector2.zero;
                        obj = null;
                        isTarget = false;
                    }
                    break;

                case 2:
                    UpdateVisibleList(pos, range);
                    if (visibleCharacterList.Count > 0)
                    {
                        SortAfterStrongest(true);
                        if (showRayCasts)
                            Debug.DrawRay(pos, ((Vector2)visibleCharacterList[0].transform.position - pos), Color.red, 0.5f);
                        direction = (Vector2)visibleCharacterList[0].transform.position - pos;
                        obj = visibleCharacterList[0];
                        isTarget = true;
                    }
                    else
                    {
                        direction = Vector2.zero;
                        obj = null;
                        isTarget = false;
                    }
                    break;

                case 3:
                    SortAfterStrongest(false);
                    if (showRayCasts)
                        Debug.DrawRay(pos, ((Vector2)characterInRangeList[0].transform.position - pos), Color.red, 0.5f);
                    direction = (Vector2)characterInRangeList[0].transform.position - pos;
                    obj = characterInRangeList[0];
                    isTarget = true;

                    break;

                default:
                    direction = Vector2.zero;
                    obj = null;
                    isTarget = false;
                    break;
            }
        }
        else
        {
            direction = Vector2.zero;
            obj = null;
        }
        return isTarget;

    }
    public bool FindSpecialTarget(ref Vector2 direction, GameObject target, Vector2 pos)
    {
        bool available;
        try
        {
            direction = (Vector2)target.transform.position - pos;
            available = true;
        }
        catch (Exception)
        {
            available = false;
        }
        return available;
    }

    private void UpdateCharacterList(Vector2 pos, float range, string tag)
    {
        GameObject[] monsters = GameObject.FindGameObjectsWithTag(tag);
        Vector3 position = (Vector3)pos;
        characterInRangeList = monsters.Where(x => ((Vector3)pos - x.transform.position).sqrMagnitude < range * range).ToList().OrderBy(a => (a.transform.position - position).magnitude).ToList();
    }

    private void UpdateVisibleList(Vector2 pos, float range)
    {
        foreach (GameObject character in characterInRangeList)
        {
            //Debug.DrawRay(pos, ((Vector2)character.transform.position - pos), Color.red, 0.5f);
            RaycastHit2D hit = Physics2D.Raycast(pos, ((Vector2)character.transform.position - pos), range, 65535 ^ 0b111001000100110);
            if (hit && hit.collider.GetComponent<Enemy>())
            {
                //Debug.DrawRay(pos, ((Vector2)character.transform.position - pos), Color.green);
                visibleCharacterList.Add(character);
            }
        }
    }

    private void SortAfterStrongest(bool visible)
    {
        if (visible)
        {
            visibleCharacterList.OrderBy(x => (GetPrio(x)));
        }
        else
        {
            characterInRangeList.OrderBy(x => (GetPrio(x)));
        }
    }

    private int GetPrio(GameObject obj)
    {
        float strength = 0;

        strength += obj.GetComponent<Enemy>().Damage;
        strength += obj.GetComponent<Enemy>().Health;
        strength += 2 / obj.GetComponent<Enemy>().AttackSleep;
        //Debug.Log("PRIO STRENGTH: " + strength);
        return (int)strength;
    }

}
