using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ExplosionEffect : MonoBehaviour
{
    public List<Sprite> Sprites;

    public float FrameDuration;
    private float Playback = 0;

    private void Awake()
    {
        GetComponent<SpriteRenderer>().sprite = Sprites[0];
    }

    private void LateUpdate()
    {
        Playback += Time.deltaTime;
        int index = (int)(Playback / FrameDuration);

        if (index >= Sprites.Count)
            Destroy(gameObject);
        else
            GetComponent<SpriteRenderer>().sprite = Sprites[index];
    }
}
