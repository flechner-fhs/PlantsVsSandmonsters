using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewScrpt : MonoBehaviour
{
    float ShootCooldown;

    public GameObject projectile;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ShootCooldown += Time.deltaTime;

        if(ShootCooldown >= 1)
        {
            GameObject.Instantiate(projectile);
            ShootCooldown = 0;
        }
    }


}
