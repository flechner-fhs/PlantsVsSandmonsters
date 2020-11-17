using UnityEngine;

public class WaterScale : MonoBehaviour
{
    public Player Player;

    public RectTransform Scale;
    public RectTransform BackgroundScale;

    private float InitSize;

    // Start is called before the first frame update
    void Start()
    {
        if (!Player)
            Player = FindObjectOfType<Player>();

        InitSize = Scale.offsetMax.y * Player.WaterSupplyMax / 100;
        BackgroundScale.offsetMax = new Vector3(0, InitSize);
    }

    // Update is called once per frame
    void Update()
    {
        Scale.offsetMax = new Vector3(0, InitSize * Player.WaterSupply / Player.WaterSupplyMax);
    }
}
