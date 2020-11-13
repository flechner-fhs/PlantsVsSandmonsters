using UnityEngine;

public class BuildOption : MonoBehaviour
{
    public GameObject Plant;
    public GameObject Container;
    public SpriteRenderer PlantRenderer;
    public SpriteRenderer Background;

    public void Setup()
    {
        BuildSelector selector = GetComponentInChildren<BuildSelector>();
        selector.Plant = Plant;
        selector.GetComponent<SpriteRenderer>().sprite = Plant.GetComponent<Plant>().Sprite;
        selector.transform.localPosition = Plant.GetComponent<Plant>().stats.MenuOffset;
    }

    public void SetRotation(float angle)
    {
        transform.rotation = Quaternion.Euler(0, 0, angle);
        Container.transform.rotation = Quaternion.identity;
    }
}
