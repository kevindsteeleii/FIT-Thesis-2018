using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// As the name indicates, it creates Loot items to be dropped upon enemy death
/// provided enemy was not turned into ammunition.
/// </summary>

public class LootGenerator : Singleton<LootGenerator>
{

    protected static Dictionary<PickupType, GameObject> lootIndex = new Dictionary<PickupType, GameObject>();

    [Tooltip("The health item prefab")]
    public GameObject healthPickUp;

    [Tooltip("The currency prefab")]
    public GameObject moneyPickUp;

    PickupType currentTypeofItem;

    //Collection that populates with all enemies present to be implemented in an enemy manager class later
    private Enemy [] enemiesPresent;

    //on awake populates dictionary 
    protected override void Awake()
    {
        base.Awake();
    }

    protected void Start()
    {
        PopulateList();
        enemiesPresent = FindObjectsOfType<Enemy>();

        //assigns LootGenerator as subscriber if enemies are present will refactor < Kev Note
        if (enemiesPresent != null)
        {
            foreach (Enemy enemy in enemiesPresent)
            {
                enemy.RandomLootDropped += MakeRandomLootNow;
                enemy.DefaultLootDrop += MakeThisLoot;
            }
        }
        
    }

    //function that takes passed params from event to generate random loot drop
    protected void MakeRandomLootNow(Vector3 pos, Quaternion rot)
    {
        MakeRandomLoot(pos, rot);
    }

    public void MakeThisLoot(Vector3 dropSpot, Quaternion rot, PickupType itemIs)
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

    /// <summary>
    /// Adds the pickups to the list to set active/inactive as objects being pooled
    /// </summary>
    protected virtual void PopulateList()
    {
        if (lootIndex != null)
        {
            return;
        }
        else
        {
            lootIndex.Add(PickupType.Health, healthPickUp);
            lootIndex.Add(PickupType.Money, moneyPickUp);
            lootIndex.Add(PickupType.Nothing, null);
        }
    }

    /// <summary>
    /// Makes random loot drop when enemy is destroyed
    /// </summary>
    /// <param name="dropSpot"></param>
    /// <param name="rot"></param>
    protected void MakeRandomLoot(Vector3 dropSpot, Quaternion rot)
    {
        float dropWeight;
        dropWeight = UnityEngine.Random.Range(1.0f, 100f);

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
            MakeThisLoot(dropSpot, rot, currentTypeofItem);
        }

        Debug.Log("Pick Up became " + currentTypeofItem + " !!");
    }
}
