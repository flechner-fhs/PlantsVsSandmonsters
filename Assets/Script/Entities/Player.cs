using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    public float WaterSupply = 100;
    public float ShootCostPerSecond = .1f;

    public float AngleAdjustment = -5;

    public GameObject WaterProjectile;
    public Watergun Watergun;


    public override void Move()
    {
        Vector3 movement = Vector2.zero;

        movement += Input.GetAxisRaw("Horizontal") * Vector3.right;
        movement += Input.GetAxisRaw("Vertical") * Vector3.up;

        movement = movement.normalized;

        rigidbody.MovePosition(transform.position + movement * Time.fixedDeltaTime * MovementSpeed);
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (WaterSupply > 0)
            {
                //Shoot
                Vector3 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
                float angle = Mathf.Atan2(direction.y, direction.x);
                angle *= Mathf.Rad2Deg;
                angle += AngleAdjustment;

                Watergun.gameObject.transform.rotation = Quaternion.Euler(Vector3.forward * angle);

                if(!Watergun.IsActive())
                    Watergun.Activate();

                //WaterSupply -= ShootCostPerSecond * Time.deltaTime;
            }
        }
        if (Input.GetMouseButtonUp(0))
            Watergun.Deactivate();
    
    }
}
