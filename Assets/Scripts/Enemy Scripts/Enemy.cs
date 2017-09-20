using UnityEngine;

public class Enemy : MonoBehaviour
{
    //sets default
    bool grabbable,becameAmmo = false;
    //the physical body of the enemy itself
    [SerializeField]
    public static GameObject body;

    //allows for adjustment of enemy health points
    [Range(0, 25)]
    public int HP;
    public bool randomDrop = false;
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
        //checks the current HP vs. fullHP and if current is <= half of full HP change
        if (HP <= saveHP / 2 && HP > 0)
        {
            grabbable = true;
        }
        else if (HP > saveHP / 2)
        {
            grabbable = false;
        }
        else if (HP <= 0)
        {
            HP = 0;
            Destroy(body);
        }
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
        Ammo.instance.load();
        becameAmmo = true;
        Destroy(body);
    }

    /*creates a pickUp item upon destruction if and only if
     the enemy did not become ammo to be shot, they are not
     mutually inclusive*/
    public void BecomePickUp()
    {
        if (randomDrop)
        {
            LootGenerator.lootGen.makeRandomLoot(transform.position, transform.rotation);
        }
        else
        {
            LootGenerator.lootGen.makeThisLoot(transform.position, transform.rotation, PickupType.Health);
        }
    }

    //re-write if a derived class is made for a boss enemy
    private void OnDestroy()
    {
        if (!becameAmmo)
        {
            BecomePickUp();            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Projectile")
        {
            takeDamage(other.gameObject.GetComponent<Projectile>().damage);
            Debug.Log("Hit");
            Destroy(other.GetComponent<GameObject>());
        }

        else if (other.gameObject.tag == "Hand" && grabbable)
        {
            BecomeProjectile();
            Debug.Log("Grabbed");
        }

        else if (other.gameObject.tag == "Hand" && !grabbable)
        {
            takeDamage(other.gameObject.GetComponent<GrabModel>().damage);
            Debug.Log("Grope");
        }
        
        /*Add cases for a punch or slam attack modeled after the
         prior conditional statements here*/
    }

}
