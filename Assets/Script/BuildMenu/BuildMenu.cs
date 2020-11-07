using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildMenu : MonoBehaviour
{
    public List<GameObject> unlockedPlants;

    public GameObject BuildOptionPrefab;

    public Tilemap FlowerEarth;
    public Tilemap Obstacles;

    public Player Player;

    private void Awake()
    {
        for(int i = 0; i < unlockedPlants.Count; i++)
        {
            if (!unlockedPlants[i].GetComponent<Plant>())
                continue;

            GameObject BuildOptionObj = Instantiate(BuildOptionPrefab, transform);
            BuildOption BuildOption = BuildOptionObj.GetComponent<BuildOption>();
            BuildOption.Plant = unlockedPlants[i];
            BuildOption.SetRotation(Mathf.Min(i * 360 / unlockedPlants.Count, 60));
            BuildOption.GetComponentInChildren<BuildSelector>().OnClick = OnSelected;
            BuildOption.Setup();
        }

        gameObject.SetActive(false);
    }

    public void Activate()
    {
        Vector3Int playerPos = FlowerEarth.WorldToCell(Player.transform.position);
        if(FlowerEarth.GetTile(playerPos) != null && Obstacles.GetTile(playerPos) == null)
        {
            transform.position = FlowerEarth.CellToWorld(playerPos) + new Vector3(.5f, .5f);
            gameObject.SetActive(true);
        }
    }

    public void OnSelected(GameObject plant)
    {
        if (FlowerEarth.GetTile(FlowerEarth.WorldToCell(transform.position)) != null)
        {

            GameObject newPlantGo = Instantiate(plant, transform.position, Quaternion.identity);
            Plant newPlant = newPlantGo.GetComponent<Plant>();

            if (Player.WaterSupply >= newPlant.WaterCost)
            {
                Player.WaterSupply -= newPlant.WaterCost;
                gameObject.SetActive(false);
            }
            else
            {
                Destroy(newPlantGo);
            }
                
        }
    }

    private void FixedUpdate()
    {
        if(new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).sqrMagnitude > 0)
            gameObject.SetActive(false);
    }
}
