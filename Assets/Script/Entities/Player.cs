using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class Player : Entity
{
    [Header("Animation")]
    public AnimationController AnimationController;

    [Header("Crafting")]
    public BuildMenu BuildMenu;
    public Tilemap Obstacles;
    public Tilemap FlowerEarth;
    public Tile EarthTile;
    public int Earth = 0;

    [Header("Water Gun")]
    public float WaterSupply = 100;
    public float ShootCostPerSecond = 10f;

    public float ShootSlow = .5f;

    public float AngleAdjustment = -5;

    public GameObject WaterProjectile;
    public Watergun Watergun;

    private bool GunMenuLock = false;


    public override void Move()
    {
        Vector3 movement = Vector2.zero;

        movement += Input.GetAxisRaw("Horizontal") * Vector3.right;
        movement += Input.GetAxisRaw("Vertical") * Vector3.up;

        AnimationController.WalkDirection(movement);

        movement = movement.normalized;

        if (IsShooting())
            movement *= ShootSlow;

        rigidbody.MovePosition(transform.position + movement * Time.fixedDeltaTime * MovementSpeed);
    }

    public override void Die()
    {
        Debug.Log("You died!");
        SceneManager.LoadScene(3);
    }

    private void Update()
    {
        if (BuildMenu.gameObject.activeSelf)
            GunMenuLock = true;
        //Shoot Water
        if (Input.GetMouseButton(0) && WaterSupply > 0 && !GunMenuLock)
        {
            //Shoot
            Vector3 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x);
            angle *= Mathf.Rad2Deg;
            angle += AngleAdjustment;

            Watergun.gameObject.transform.rotation = Quaternion.Euler(Vector3.forward * angle);
            AnimationController.WalkDirection(direction);

            if (!Watergun.IsActive())
                Watergun.Activate();

            WaterSupply -= ShootCostPerSecond * Time.deltaTime;
        }
        //Stop Water
        if (Input.GetMouseButtonUp(0) || WaterSupply <= 0)
        {
            Watergun.Deactivate();
            GunMenuLock = false;
        }
        //Place Earth
        if (Input.GetMouseButton(1) && Earth > 0)
        {
            Vector3Int pos = Obstacles.WorldToCell(transform.position);
            if (Obstacles.GetTile(pos) == null && FlowerEarth.GetTile(pos) == null)
            {
                FlowerEarth.SetTile(pos, EarthTile);
                Earth--;
                FlowerEarth.RefreshAllTiles();
            }
        }
        //Open Build Menu
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector3Int pos = FlowerEarth.WorldToCell(transform.position);
            if (Obstacles.GetTile(pos) == null && FlowerEarth.GetTile(pos) != null)
            {
                BuildMenu.Activate();
            }
        }
    }

    public bool IsShooting() => Input.GetMouseButton(0);
}
