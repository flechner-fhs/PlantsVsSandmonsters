using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthDrop : Drop
{
    public bool IsHumid {
        get => WaterCollected >= WaterNeeded;
        set => WaterCollected = value ? WaterNeeded : 0;
    }

    public float WaterNeeded = 10;
    private float WaterCollected = 0;

    public Sprite DrySprite, HumidSprite;

    private void Awake()
    {
        GetComponentInChildren<SpriteRenderer>().sprite = DrySprite;
        IsHumid = false;
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
            WaterCollected++;

            if (IsHumid)
            {
                GetComponentInChildren<SpriteRenderer>().sprite = HumidSprite;
                gameObject.layer = 12;
            }
            
        }
    }
}
