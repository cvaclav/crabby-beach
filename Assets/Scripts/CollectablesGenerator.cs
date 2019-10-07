using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectablesGenerator : MonoBehaviour
{
    public Transform minX;

    public Transform maxX;

    public Transform[] stagesY;

    public BuildableItem[] buildableItems;

    public GameObject collectablePrefab;

    public int maxWoodCount;

    public int maxStonesCount;

    public int maxCoralsCount;

    public int itemsStartCount;

    public int itemsMaxCount;

    private int woodCount;

    private int stonesCount;

    private int coralsCount;

    void Start()
    {
        woodCount = 0;
        stonesCount = 0;
        coralsCount = 0;

        for (int i = 1; i <= itemsStartCount; i++)
        {
            this.generateItem();
        }
    }

    public void generateAfterWave()
    {
        int totalCount = woodCount + stonesCount + coralsCount;

        for (int i = 1; i <= (itemsMaxCount - totalCount); i++)
        {
            generateItem();
        }
    }

    public void generateItem()
    {
        List<BuildableItem> posibleItems = new List<BuildableItem>();

        if (woodCount < this.maxWoodCount)
        {
            posibleItems.Add(this.buildableItems[0]);
        }

        if (stonesCount < this.maxStonesCount)
        {
            posibleItems.Add(this.buildableItems[1]);
        }

        if (coralsCount < this.maxCoralsCount)
        {
            posibleItems.Add(this.buildableItems[2]);
        }

        BuildableItem buildableItem = posibleItems[Random.Range(0, posibleItems.Count)];

        if (buildableItem.material == ItemMaterial.Wood)
        {
            woodCount++;
        }
        else if (buildableItem.material == ItemMaterial.Stone)
        {
            stonesCount++;
        }
        else if (buildableItem.material == ItemMaterial.Coral)
        {
            coralsCount++;
        }

        int stage = buildableItem.stage;

        if (stage == 1)
        {
            if (Random.Range(1, 6) == 1)
            {
                stage = 2;
            }
        }
        else
        {
            stage = Random.Range(2, 3);
        }

        float randomPositionX = Random.Range(this.minX.position.x, this.maxX.position.x);
        float randomPositionY = Random.Range(this.stagesY[stage - 1].position.y, this.stagesY[stage].position.y);

        Vector2 position = new Vector2(randomPositionX, randomPositionY);

        GameObject newItem = Instantiate(collectablePrefab, position, Quaternion.identity) as GameObject;
        newItem.GetComponent<CollectableItem>().buildableItem = buildableItem;
    }

    public void removeItem(BuildableItem item)
    {
        if (item.material == ItemMaterial.Wood)
        {
            woodCount--;
        }
        else if (item.material == ItemMaterial.Stone)
        {
            stonesCount--;
        }
        else if (item.material == ItemMaterial.Coral)
        {
            coralsCount--;
        }
    }
}
