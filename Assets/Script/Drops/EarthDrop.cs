using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthDrop : Drop
{
    public bool IsHumid = false;

    public Sprite DrySprite, HumidSprite;

    private void Awake()
    {
        GetComponentInChildren<SpriteRenderer>().sprite = DrySprite;
    }

    public override void Collect(Player player)
    {
        player.Earth++;
    }

    public override bool IsCollectible() => IsHumid;

    private void OnParticleCollision(GameObject other)
    {
        if (other.GetComponent<Watergun>())
        {
            IsHumid = true;
            GetComponentInChildren<SpriteRenderer>().sprite = HumidSprite;
            gameObject.layer = 12;
        }
    }
}
