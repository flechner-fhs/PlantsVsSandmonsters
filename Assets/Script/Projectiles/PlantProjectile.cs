﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UIElements;

public class PlantProjectile : Projectile
{
    [HideInInspector]
    public float AttRange;
    protected float lifeTime;
    float cd = 10f;
    [HideInInspector]
    public GameObject target = null;
    [HideInInspector]
    public Vector2 oldDirection = Vector2.zero;

    void Start()
    {
        lifeTime = AttRange / MovementSpeed * 2;
        //Debug.Log("LIFETIME: " + lifeTime);
    }

    void Update()
    {
        Fly();
    }

    public void Fly()
    {
        if (lifeTime > 0)
        {
            if (cd >= 0.5)
            {
                TargetFinding tf = new TargetFinding();
                Vector2 direction = oldDirection;
                if (tf.findSpecialTarget(ref direction, target, transform.position))
                {

                    direction = direction.normalized * MovementSpeed * Time.fixedDeltaTime;
                    oldDirection = direction;
                }
                else
                {
                    oldDirection *= MovementSpeed;
                }
                Debug.DrawRay(transform.position, oldDirection, Color.cyan, 1);
                cd = 0;
            }
            else
            {
                cd += Time.fixedDeltaTime;
            }
            //Debug.Log("Length: " + oldDirection.magnitude);
            Rigidbody.MovePosition((Vector2)transform.position + oldDirection);
            lifeTime -= Time.fixedDeltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject obj = collision.gameObject;
        if (obj.tag == "Enemy")
        {
            obj.GetComponent<Enemy>().TakeDamage(Damage);
        }

        if (!obj.GetComponent<Plant>() || !obj.GetComponent<Projectile>() || !obj.GetComponent<Player>())
            Destroy(gameObject);
    }
}
