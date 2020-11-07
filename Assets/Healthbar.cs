using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healthbar : MonoBehaviour
{
    public Player Player;

    public RectTransform Scale;

    private float InitSize;

    // Start is called before the first frame update
    void Start()
    {
        if (!Player)
            Player = FindObjectOfType<Player>();

        InitSize = Scale.offsetMax.y;
    }

    // Update is called once per frame
    void Update()
    {
        Scale.offsetMax = new Vector3(0, InitSize * Player.Health / Player.MaxHealth);
    }
}
