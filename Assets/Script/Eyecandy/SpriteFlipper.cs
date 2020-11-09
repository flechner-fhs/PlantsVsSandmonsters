using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFlipper : MonoBehaviour
{
    public bool IsLeftOriented = false;

    private Entity entity;

    private void Awake()
    {
        entity = GetComponentInParent<Entity>();
    }

    void LateUpdate()
    {
        transform.localScale = new Vector3(0, 1, 1) + Vector3.right * (entity.FacingLeft == IsLeftOriented ? 1 : -1);
    }
}
