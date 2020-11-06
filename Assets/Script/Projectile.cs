using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Projectile : MonoBehaviour
{
    public Rigidbody2D rigidbody;
    public float MovementSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
      GameObject monster = GameObject.Find("LilMonster");
        //transform.position = Vector2.MoveTowards(transform.position, monster.transform.position, 0.03f);
        Vector2 direction = monster.transform.position - transform.position;
        direction = direction.normalized * MovementSpeed * Time.fixedDeltaTime;
        rigidbody.MovePosition((Vector2)transform.position + direction);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        Destroy(this.gameObject);
    
    }
}
