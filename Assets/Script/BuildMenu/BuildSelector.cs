using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class BuildSelector : MonoBehaviour
{
    public Action<GameObject> OnClick;
    public GameObject Plant;

    private void OnMouseDown()
    {
        OnClick(Plant);
    }

}
