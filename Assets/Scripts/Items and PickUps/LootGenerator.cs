using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// As the name indicates, it creates Loot items to be dropped upon enemy death
/// provided enemy was not turned into ammunition.
/// </summary>

public class LootGenerator : Singleton<LootGenerator>
{
    PickupType itemType = PickupType.Health;

    protected static Dictionary<PickupType, GameObject> lootIndex = new Dictionary<PickupType, GameObject>();

    [Tooltip("The health item prefab")]
    public GameObject healthPickUp;

    [Tooltip("The currency prefab")]
    public GameObject moneyPickUp;

    PickupType currentTypeofItem;
    GameObject itemDropped;

    //on awake populates dictionary 
    protected override void Awake()
    {
        base.Awake();
        PopulateList();
    }

    public void makeThisLoot(Vector3 dropSpot, Quaternion rot, PickupType itemIs)
    {
        GameObject spawnObject;

        if (lootIndex.TryGetValue(itemIs, out spawnObject))
        {
            GameObject objToSpawn = Instantiate(spawnObject, dropSpot, rot);

            Rigidbody tempRB = spawnObject.GetComponent<Rigidbody>();
            print("Item is " + itemIs);
        }
        else
        {
            // do a thing here if item doesn't exist
            print("Nothing available here!");
        }
    }

    public void PopulateList()
    {
        lootIndex.Add(PickupType.Health, healthPickUp);
        lootIndex.Add(PickupType.Money, moneyPickUp);
        lootIndex.Add(PickupType.Nothing, null);
    }

    public void makeRandomLoot(Vector3 dropSpot, Quaternion rot)
    {
        float dropWeight;
        dropWeight = Random.Range(1.0f, 100f);

        if (dropWeight >= 60 && dropWeight < 91)
        {
            currentTypeofItem = PickupType.Health;
        }
        else if (dropWeight >= 21 && dropWeight < 59)
        {
            currentTypeofItem = PickupType.Money;
        }
        else
        {
            currentTypeofItem = PickupType.Nothing;
        }

        if (currentTypeofItem != PickupType.Nothing)
        {
            makeThisLoot(dropSpot, rot, currentTypeofItem);
        }

        Debug.Log("Pick Up became " + currentTypeofItem + " !!");
    }
}
