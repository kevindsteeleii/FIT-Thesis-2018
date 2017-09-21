using UnityEngine;

/// <summary>
/// As the name indicates, it creates Loot items to be dropped upon enemy death
/// provided enemy was not turned into ammunition.
/// </summary>
public class LootGenerator : Singleton<LootGenerator>
{
    PickupType itemType = PickupType.Health;

    [Tooltip("The health item prefab")]
    public GameObject healthPickUp;

    [Tooltip("The currency prefab")]
    public GameObject moneyPickUp;

    PickupType currentTypeofItem;
    GameObject itemDropped;



    public void makeThisLoot(Vector3 dropSpot, Quaternion rot, PickupType itemIs)
    {
        GameObject spawnObject;

        switch (itemIs)
        {
            case PickupType.Health:
                itemDropped = healthPickUp;
                break;
            case PickupType.Money:
                itemDropped = moneyPickUp;
                break;
            case PickupType.Nothing:
                itemDropped = null;
                break;
        };

        if (itemIs == PickupType.Health || itemIs == PickupType.Money)
        {
            spawnObject = Instantiate(itemDropped, dropSpot, rot);
            Rigidbody tempRB = spawnObject.GetComponent<Rigidbody>();
            Debug.Log(" The drop is a " + itemIs + " !!");
        }
        else
        {
            Debug.Log("Nothing for you");
        }

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
