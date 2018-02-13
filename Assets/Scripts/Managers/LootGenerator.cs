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
    [Tooltip("The health item prefab")]
    public GameObject healthPickUp;

    [Tooltip("The currency prefab")]
    public GameObject moneyPickUp;

    PickupType currentTypeofItem;

    //Collection that populates with all enemies present to be implemented in an enemy manager class later
    [SerializeField]
    List<GameObject> enemiesPresent = new List<GameObject>();

    private void Start()
    {
        //EnemySpawner.instance.On_EnemySpawns_Sent += On_EnemySpawns_Received;
    }
    private void Update()
    {
        EnemySpawner.instance.On_EnemySpawns_Sent += On_EnemySpawns_Received;
    }
    protected virtual void On_EnemySpawns_Received(List<GameObject> obj)
    {
        enemiesPresent = obj;

        //for (int i =0; i < obj.Count;i++)
        //{
        //    enemiesPresent[i].GetComponent<Enemy>().On_RandomLootDropped_Sent += On_RandomLootDropped_Received;
        //    enemiesPresent[i].GetComponent<Enemy>().On_DefaultLootDrop_Sent += On_DefaultLootDrop_Received;
        //}
    }

    public void On_DefaultLootDrop_Received(Vector3 pos1, Quaternion rot, PickupType pickUp)
    {
        MakeThisLoot(pos1, rot, pickUp);
    }

    /// <summary>
    /// Function that takes passed params from event to generate random loot drop
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="rot"></param>
    public void On_RandomLootDropped_Received(Vector3 pos, Quaternion rot)
    {
        MakeRandomLoot(pos, rot);
    }

    /// <summary>
    /// Makes loot of type pickuptype specified, at specific location and quaternion rotation
    /// </summary>
    /// <param name="dropSpot"></param>
    /// <param name="rot"></param>
    /// <param name="itemIs"></param>
    public void MakeThisLoot(Vector3 dropSpot, Quaternion rot, PickupType itemIs)
    {
        GameObject spawnObject;
        switch (itemIs)
        {
            case PickupType.Health:
                spawnObject = Instantiate(healthPickUp);
                spawnObject.transform.position = dropSpot;
                break;
            case PickupType.Money:
                spawnObject = moneyPickUp;
                spawnObject = Instantiate(moneyPickUp);
                spawnObject.transform.position = dropSpot;
                break;
            case PickupType.Nothing:
                break;
            default:
                break;
        }

        //print("Item is " + itemIs);
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
            MakeThisLoot(dropSpot, rot, currentTypeofItem);
        }
        else if (dropWeight >= 21 && dropWeight < 59)
        {
            currentTypeofItem = PickupType.Money;
            MakeThisLoot(dropSpot, rot, currentTypeofItem);
        }
        else
        {
            currentTypeofItem = PickupType.Nothing;
        }

        //Debug.Log("Pick Up became " + currentTypeofItem + " !!");
    }
}
