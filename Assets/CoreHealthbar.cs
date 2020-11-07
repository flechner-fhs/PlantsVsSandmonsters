using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreHealthbar : MonoBehaviour
{
    public Core Core;

    public RectTransform Scale;

    private float InitSize;

    // Start is called before the first frame update
    void Start()
    {
        if (!Core)
            Core = FindObjectOfType<Core>();

        InitSize = Scale.offsetMax.y;
    }

    // Update is called once per frame
    void Update()
    {
        Scale.offsetMax = new Vector3(0, InitSize * Core.Health / Core.MaxHealth);
    }
}
