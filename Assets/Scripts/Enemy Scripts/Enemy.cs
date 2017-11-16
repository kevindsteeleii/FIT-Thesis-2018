using UnityEngine;
using System;

/// <summary>
/// Enum that is used to handle the behavior of individual enemies. Never use total its just to number them for enumerations, iterations and the like.
/// </summary>
public enum EnemyBehavior { None, Patrolling, Turret, Floating, total };
/// <summary>
/// Enemy base class used to determine basic information and behavior of enemy class
/// </summary>
[RequireComponent(typeof (EnemyPatrol))]
public class Enemy : MonoBehaviour
{

    #region Global Variables
    //the physical body of the enemy itself
    [SerializeField]
    public GameObject body;

    /// <summary>
    /// Platform enemy is place upon
    /// </summary>
    [SerializeField]
    Platforms enPlatform;

    /// <summary>
    /// Event that transmits which platform enemy is assigned to
    /// </summary>
    public event Action<Platforms> EnPlatformIs;

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
    public event Action<Vector3, Quaternion, PickupType> DefaultLootDrop;

    /// <summary>
    /// Event that transmits the current enemies type of behavior
    /// </summary>
    public event Action<EnemyBehavior> SendBehavior;

    //allows for adjustment of enemy health points
    [Range(0, 25)]
    public int HP;
    public bool randomDrop = false;
    public bool randomBehavior = false;

    //enum used to determine the behavior of the enemy
    public EnemyBehavior enBehavior = EnemyBehavior.None;

    //used to save max HP info for enemy
    int saveHP;

    [Range(0, 25)]
    public int damage;

    #endregion

    // Use this for initialization
    void Start()
    {
        body = this.gameObject;
        saveHP = HP;
       
    }

    protected virtual void Update()
    {
        Mathf.Clamp(HP, 0, 25);

        if (HP <= 0)
        {
            HP = 0;
        }
        //if sendBehavior has a subscriber...

        if (SendBehavior != null)
        {
            SendBehavior(enBehavior);
        }

        if (EnPlatformIs != null)
        {
            EnPlatformIs(enPlatform);
        }
        #region Behavior Switch Block
        //switch (enBehavior)
        //{
        //    case EnemyBehavior.None:
        //        break;
        //    case EnemyBehavior.Patrolling:
        //        OnPatrol();
        //        break;
        //    case EnemyBehavior.Turret:
        //        break;
        //    case EnemyBehavior.Floating:
        //        break;
        //    default:
        //        break;
        //}
        #endregion

        //if random behavior is toggled it randomly assigns the behavior type
        if (randomBehavior)
        {
            int pick = UnityEngine.Random.Range(0, EnemyBehavior.total.GetHashCode() - 1);
            enBehavior = (EnemyBehavior)pick;
            Debug.Log("This enemy is the " + enBehavior + " type!!");
            randomBehavior = false;
        }

        Debug.Log("The exhibited behavior is "+ enBehavior);

        //Debug.Log("Enemy HP is " + HP);
    }

    /// <summary>
    /// Switches the behavior exhibited by the enemy and transmits to its subsequent 
    /// </summary>
    /// <param name="behavior"></param>
    public void SwitchBehavior(EnemyBehavior behavior)
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
                case EnemyBehavior.total:
                    break;
                default:
                    break;
            }
        enBehavior = behavior;
    }

    /// <summary>
    /// Assigns Platform of player the value of passed param.
    /// </summary>
    /// <param name="tempPlat"></param>
    public void SetPlatform(Platforms tempPlat)
    {
        enPlatform = tempPlat;
        Debug.Log("Platform Assigned!!");
    }

    /// <summary>
    /// Calculates damage.
    /// </summary>
    /// <param name="dam"></param>
    public virtual void TakeDamage(int dam)
    {
        HP -= dam;
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
    public virtual void BecomePickUp()
    {
        Destroy(body);
        if (randomDrop && RandomLootDropped != null)
        {
            RandomLootDropped(transform.position, transform.rotation);
        }
        else if (!randomDrop && DefaultLootDrop != null)
        {
            DefaultLootDrop(transform.position, transform.rotation, PickupType.Health);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Projectile")
        {
            TakeDamage(other.gameObject.GetComponent<Projectile>().damage);
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

        else if (other.gameObject.tag == "Hand")
        {
            TakeDamage(other.gameObject.GetComponent<GrabModel>().damage);

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
