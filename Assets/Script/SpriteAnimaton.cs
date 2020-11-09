using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteAnimaton : MonoBehaviour
{
    public SpriteRenderer SpriteRenderer;
    public Image Image;

    public float AnimationSpeed = 10;

    private float Frame = 0;

    public List<Sprite> Sprites;

    private void FixedUpdate()
    {
        Frame = (Frame + Time.fixedDeltaTime * AnimationSpeed) % Sprites.Count;

        if(SpriteRenderer)
            SpriteRenderer.sprite = Sprites[(int)Frame];
        if (Image)
            Image.sprite = Sprites[(int)Frame];
    }
}
