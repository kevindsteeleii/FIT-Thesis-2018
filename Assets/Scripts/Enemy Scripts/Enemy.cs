using UnityEngine;
using System;
public class Enemy : MonoBehaviour
{
    //the physical body of the enemy itself
    [SerializeField]
    public static GameObject body;

    /// <summary>
    /// Event that transmits from Enemy to trigger ammo stocking 
    /// </summary>
    public event Action BecomeAmmo; //need to assign subscriber < Kev Note

    /// <summary>
    /// Event that transmits when the enemy becomes random loot on "destruction"
    /// </summary>
    public event Action<Vector3, Quaternion> RandomLootDropped;

    /// <summary>
    /// Event that transmits when the enemy becomes the default loot upon "destruction"
    /// </summary>
    public event Action<Vector3,Quaternion,PickupType> DefaultLootDrop;

    //allows for adjustment of enemy health points
    [Range(0, 25)]
    public int HP;
    public bool randomDrop = false;

    //used to save max HP info for enemy
    int saveHP;

    [Range(0, 25)]
    public int damage;
   
    // Use this for initialization
    void Start()
    {
        body = this.gameObject;
        saveHP = HP;
    }

    protected virtual void Update()
    {
        Mathf.Clamp(HP, 0, 25);

        if (HP<=0)
        {
            HP = 0;
        }

        //Debug.Log("Enemy HP is " + HP);
    }

    public virtual void takeDamage(int dam)
    {
        HP -= dam;
    }

    /// <summary>
    /// Changes the tag of the enemy and transforming the body into a projectile.
    /// </summary>
    public void BecomeProjectile()
    {
        //transmits to Ammo handling manager as a subject of subscription
        if (BecomeAmmo != null)
        {
            BecomeAmmo();
        }
        //Ammo.instance.Load();
        Destroy(body); //needs implementation of a enemy spawner/ manager class to "disappear" enemies < Kev Note
        Debug.Log("Became Projectile!!");
    }

    /*creates a pickUp item upon destruction if and only if
     the enemy did not become ammo to be shot, they are not
     mutually inclusive*/
    public void BecomePickUp()
    {
        Destroy(body);
        if (randomDrop && RandomLootDropped != null)
        {
            RandomLootDropped(transform.position, transform.rotation);
            //LootGenerator.instance.MakeRandomLoot(transform.position, transform.rotation);
        }
        else if (!randomDrop && DefaultLootDrop != null)
        {
            DefaultLootDrop(transform.position, transform.rotation, PickupType.Health);
            //LootGenerator.instance.MakeThisLoot(transform.position, transform.rotation, PickupType.Health);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Projectile")
        {
            takeDamage(other.gameObject.GetComponent<Projectile>().damage);
            if (HP <= saveHP/2)
            {
                BecomePickUp();
            }
            Debug.Log("Hit");
            //Destroy(other.gameObject);
        }
        
        /*In this block, if the object in contact is the hand it automatically does damage,
         then sorts out what happens based on comparing the max HP w/ the new adjusted amount and 
         outputs results accordingly*/

        else if (other.gameObject.tag == "Hand")
        {
            takeDamage(other.gameObject.GetComponent<GrabModel>().damage);

            if (HP <= saveHP/2)
            {
                BecomeProjectile();
                Debug.Log("Grabbed");
            }
            else
            {
                Debug.Log("Grope!");
            }
        }

        /*Add cases for a punch or slam attack modeled after the
         prior conditional statements here later on*/
    }
}
