using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildMenu : MonoBehaviour
{
    public List<GameObject> unlockedPlants;

    public GameObject BuildOptionPrefab;
    public GameObject OptionContainer;

    public Tilemap FlowerEarth;
    public Tilemap Obstacles;

    public Player Player;

    public float BuildRestriction = .2f;

    public float PopupTime = .5f;
    public bool IsOpened = false;

    private void Awake()
    {
        for(int i = 0; i < unlockedPlants.Count; i++)
        {
            if (!unlockedPlants[i].GetComponent<Plant>())
                continue;

            GameObject BuildOptionObj = Instantiate(BuildOptionPrefab, OptionContainer.transform);
            BuildOption BuildOption = BuildOptionObj.GetComponent<BuildOption>();
            BuildOption.Plant = unlockedPlants[i];
            BuildOption.SetRotation(-i * Mathf.Min(360 / unlockedPlants.Count, 60));
            BuildOption.GetComponentInChildren<BuildSelector>().OnClick = OnSelected;
            BuildOption.Setup();
        }

        OptionContainer.transform.localScale = Vector3.zero;
        OptionContainer.SetActive(true);
        IsOpened = false;
    }

    public void Activate()
    {
        Vector3Int playerPos = FlowerEarth.WorldToCell(Player.transform.position);
        if(FlowerEarth.GetTile(playerPos) != null && Obstacles.GetTile(playerPos) == null)
        {
            Vector3 targetPos = FlowerEarth.CellToWorld(playerPos) + new Vector3(.5f, .5f);
            if (FindObjectsOfType<Plant>().Where(x => (x.transform.position - targetPos).sqrMagnitude < BuildRestriction).Count() == 0)
            {
                transform.position = targetPos;
                StartCoroutine(PopUp());
            }

        }
    }

    IEnumerator PopUp()
    {
        float steps = 30;
        OptionContainer.transform.localScale = Vector3.zero;
        OptionContainer.SetActive(true);
        for (int i = 0; i < steps; i++)
        {
            OptionContainer.transform.localScale = Vector3.one * (i / steps);
            yield return new WaitForSeconds(PopupTime / steps);
        }
        OptionContainer.transform.localScale = Vector3.one;
        IsOpened = true;
    }
    IEnumerator CloseDown()
    {
        float steps = 30;
        OptionContainer.transform.localScale = Vector3.one;
        IsOpened = false;
        for (int i = 0; i < steps; i++)
        {
            OptionContainer.transform.localScale = Vector3.one * (1 - i / steps);
            yield return new WaitForSeconds(PopupTime / steps);
        }
        OptionContainer.transform.localScale = Vector3.zero;
        OptionContainer.SetActive(false);
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
                StartCoroutine(CloseDown());
            }
            else
            {
                Destroy(newPlantGo);
            }
        }
    }

    private void FixedUpdate()
    {
        if (IsOpened && new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).sqrMagnitude > 0)
            StartCoroutine(CloseDown());
    }
}
