using UnityEngine;
using System;

/// <summary>
/// Enemy base class used to determine basic information and behavior of enemy class
/// </summary>
public class Enemy : MonoBehaviour
{
    #region Global Variables
    /// <summary>
    /// Event that transmits from Enemy to trigger ammo stocking 
    /// </summary>
    public event Action On_BecomeAmmo_Sent; //need to assign subscriber < Kev Note

    /// <summary>
    /// Event that transmits when the enemy becomes random loot on "destruction"
    /// </summary>
    public event Action<Vector3, Quaternion> On_RandomLootDropped_Sent;

    /// <summary>
    /// Event that transmits when the enemy becomes the default loot upon "destruction"
    /// </summary>
    public event Action<Vector3, Quaternion, PickupType> On_DefaultLootDrop_Sent;

    //allows for adjustment of enemy health points
    [Range(0, 25)]
    public int HP;
    public bool randomDrop = false;
    public bool randomBehavior = false;
    
    //enum used to determine the behavior of the enemy
    public EnemyBehavior enBehavior = EnemyBehavior.Stationary;

    //used to save max HP info for enemy
    int saveHP;
    #endregion

    // Use this for initialization
    protected virtual void Start()
    {
        saveHP = HP;
        RandomBehavior(randomBehavior);
    }

    protected virtual void Update()
    {
        Mathf.Clamp(HP, 0, 25);

        if (HP <= 0)
        {
            HP = 0;
            DealDeath();
        }
    }
    /// <summary>
    /// Logic ran upon death using several functions
    /// </summary>
    private void DealDeath()
    {
        Destroy(gameObject);
    }

    public virtual void EnemyTakeDamage(int dam)  {
        HP -= dam;
    }

    //if random behavior is toggled it randomly assigns the behavior type
    protected virtual void RandomBehavior(bool active)   {
        if (active)  {
            randomBehavior = false;
        }
    }

    /// <summary>
    /// Sets the position of the enemy
    /// </summary>
    /// <param name="pos"></param>
    public virtual void SetCenter(Vector3 pos)   {
        this.transform.position = pos;
    }

    /// <summary>
    /// Changes the tag of the enemy and transforming the body into a projectile.
    /// </summary>
    public virtual void BecomeProjectile()
    {
        On_BecomeAmmo_Sent += Ammo.instance.Load;
        //transmits to Ammo handling manager as a subject of subscription
        if (On_BecomeAmmo_Sent != null)
        {
            Debug.Log("Became Ammo");
            On_BecomeAmmo_Sent();
        }
        Destroy(gameObject); 
    }

    /// <summary>
    /// "Destroys" body by setting parent to inactive
    /// </summary>
    /// <param name="obj"></param>
    private void Destroy(GameObject obj)
    {
        obj.SetActive(false);
    }

    /// <summary>
    /// creates a pickUp item upon destruction if and only if
    ///the enemy did not become ammo to be shot, they are not
    ///mutually inclusive
    /// </summary>
    protected virtual void BecomePickUp()
    {
        if (randomDrop)
        {
            this.On_RandomLootDropped_Sent += LootGenerator.instance.On_RandomLootDropped_Received;
            On_RandomLootDropped_Sent(transform.position, transform.rotation);
        }
        else if (!randomDrop)
        {
            this.On_DefaultLootDrop_Sent += LootGenerator.instance.On_DefaultLootDrop_Received ;
            On_DefaultLootDrop_Sent(transform.position, transform.rotation, PickupType.Health);
        }
        Destroy(gameObject);   
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject obj = other.gameObject;
        if (obj.tag == "Projectile")
        {
            EnemyTakeDamage(obj.GetComponent<Projectile>().damage); //refactor with hitbox/hurtbox paradigm 
            if (HP <= 0)
            {
                BecomePickUp();
            }
        }

        /*In this block, if the object in contact is the hand it automatically does damage,
         then sorts out what happens based on comparing the max HP w/ the new adjusted amount and 
         outputs results accordingly*/
        if (obj.tag == "Hand")
        {
            EnemyTakeDamage(obj.GetComponent<GrabModel>().damage);

            if (HP <= saveHP / 2)
            {
                BecomeProjectile();
            }
        }
    }
}
#region TODO list, refactoring etc
/************TODO Refactoring********************************************************************//*
 1-
 2-
 3-
 4-
 *************************************************************************************************/
#endregion
