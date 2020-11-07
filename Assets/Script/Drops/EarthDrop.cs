using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthDrop : Drop
{
    public override void Collect(Player player)
    {
        player.Earth++;
    }
}
