using UnityEngine;
using System;

/// <summary>
/// Enum that is used to handle the behavior of individual enemies. Never use total its just to number them for enumerations, iterations and the like.
/// </summary>
public enum EnemyBehavior { None, Patrolling, Turret, Floating, total };

/// <summary>
/// Enemy base class used to determine basic information and behavior of enemy class
/// </summary>
//[RequireComponent(typeof(EnemyPatrol), typeof(EnemyTurret))]
public class Enemy : MonoBehaviour
{
    #region Global Variables
    //the physical body of the enemy itself
    [SerializeField]
    GameObject body;

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

    /// <summary>
    /// Event that transmits the current enemies type of behavior
    /// </summary>
    public event Action<EnemyBehavior> On_SendBehavior_Sent;

    //allows for adjustment of enemy health points
    [Range(0, 25)]
    public int HP;
    public bool randomDrop = false;
    public bool randomBehavior = false;
    
    //enum used to determine the behavior of the enemy
    public EnemyBehavior enBehavior = EnemyBehavior.None;

    //used to save max HP info for enemy
    int saveHP;

    //[Range(0, 25)]
    //public int damage;
    #endregion

    // Use this for initialization
    protected virtual void Start()
    {
        body = this.gameObject;
        saveHP = HP;
        //enPlatform.PlatformMoved += EnPlatform_PlatformMoved;
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
        //if sendBehavior has a subscriber...

        if (On_SendBehavior_Sent != null)
        {
            On_SendBehavior_Sent(enBehavior);
        }
    }

    /// <summary>
    /// Logic ran upon death using several functions
    /// </summary>
    private void DealDeath()
    {
        Destroy(body);
    }

    public virtual void TakeDamage(int dam)
    {
        HP -= dam;
        //Debug.Log("Enemy taking damage. HP = "+HP);
    }

    //if random behavior is toggled it randomly assigns the behavior type
    protected virtual void RandomBehavior(bool active)
    {
        if (active)
        {
            int pick = UnityEngine.Random.Range(0, EnemyBehavior.total.GetHashCode() - 1);
            enBehavior = (EnemyBehavior)pick;
            Debug.Log("This enemy is the " + enBehavior + " type!!");
            randomBehavior = false;
        }
    }

    /// <summary>
    /// Switches the behavior exhibited by the enemy and transmits to its subsequent 
    /// </summary>
    /// <param name="behavior"></param>
    protected virtual void SwitchBehavior(EnemyBehavior behavior)
    {
        switch (behavior)
        {
            case EnemyBehavior.None:
                break;
            case EnemyBehavior.Patrolling:
                break;
            case EnemyBehavior.Turret:
                break;
            case EnemyBehavior.Floating:
                break;
            default:
                break;
        }
        enBehavior = behavior;
    }

    /// <summary>
    /// Sets the position of the enemy
    /// </summary>
    /// <param name="pos"></param>
    public virtual void SetCenter(Vector3 pos)
    {
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
        Destroy(body); 
    }

    private void Destroy(GameObject obj)
    {
        obj.SetActive(false);
    }

    /*creates a pickUp item upon destruction if and only if
     the enemy did not become ammo to be shot, they are not
     mutually inclusive*/
    protected virtual void BecomePickUp()
    {
        Destroy(body);
        if (randomDrop && On_RandomLootDropped_Sent != null)
        {
            On_RandomLootDropped_Sent(transform.position, transform.rotation);
        }
        else if (!randomDrop && On_DefaultLootDrop_Sent != null)
        {
            On_DefaultLootDrop_Sent(transform.position, transform.rotation, PickupType.Health);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject obj = other.gameObject;
        if (obj.tag == "Projectile")
        {
            TakeDamage(obj.GetComponent<Projectile>().damage); //refactor with hitbox/hurtbox paradigm 
            if (HP <= saveHP / 2)
            {
                BecomePickUp();
            }
            Debug.Log("Hit");
            //Destroy(other.gameObject);
        }

        /*In this block, if the object in contact is the hand it automatically does damage,
         then sorts out what happens based on comparing the max HP w/ the new adjusted amount and 
         outputs results accordingly*/

        else if (obj.tag == "Hand")
        {
            TakeDamage(obj.GetComponent<GrabModel>().damage);

            if (HP <= saveHP / 2)
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