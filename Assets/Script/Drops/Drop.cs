using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Drop : MonoBehaviour
{
    public string Name;

    public float FloatRange = .05f;
    public float FloatSpeed = 3;

    private float progress = 0;

    public SpriteRenderer MainSprite;

    public abstract void Collect(Player player);

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (player)
        {
            Collect(player);
            Destroy(gameObject);
        }
    }

    public void LateUpdate()
    {
        MainSprite.gameObject.transform.localPosition = Vector3.up * Mathf.Sin(progress) * FloatRange;
        progress += Time.deltaTime * FloatSpeed;
    }

}
