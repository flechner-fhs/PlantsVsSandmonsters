using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildOption : MonoBehaviour
{
    public GameObject Plant;
    public SpriteRenderer PlantRenderer;
    public SpriteRenderer Background;

    public void Setup()
    {
        BuildSelector selector = GetComponentInChildren<BuildSelector>();
        selector.Plant = Plant;
        selector.GetComponent<SpriteRenderer>().sprite = Plant.GetComponent<Plant>().Sprite;
    }

    public void SetRotation(float angle)
    {
        transform.rotation = Quaternion.Euler(0, 0, angle);
        PlantRenderer.transform.rotation = Quaternion.identity;
    }
}
