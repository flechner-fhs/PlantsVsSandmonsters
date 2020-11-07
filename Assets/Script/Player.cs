using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{

    public override void Move()
    {
        Vector3 movement = Vector2.zero;

        movement += Input.GetAxisRaw("Horizontal") * Vector3.right;
        movement += Input.GetAxisRaw("Vertical") * Vector3.up;

        movement = movement.normalized;

        rigidbody.MovePosition(transform.position + movement * Time.fixedDeltaTime * MovementSpeed);
    }

}
