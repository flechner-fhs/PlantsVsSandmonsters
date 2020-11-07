using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DirtCounter : MonoBehaviour
{
    public Player Player;

    public Text Text;

    void LateUpdate()
    {
        Text.text = Player.Earth + "";
    }
}
