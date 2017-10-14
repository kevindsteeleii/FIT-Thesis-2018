using UnityEngine;

public class Enemy : MonoBehaviour
{

    //the physical body of the enemy itself
    [SerializeField]
    public static GameObject body;

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
        Ammo.instance.Load();
        Destroy(body);
        Debug.Log("Became Projectile!!");
    }

    /*creates a pickUp item upon destruction if and only if
     the enemy did not become ammo to be shot, they are not
     mutually inclusive*/
    public void BecomePickUp()
    {
        Destroy(body);
        if (randomDrop)
        {
            LootGenerator.instance.makeRandomLoot(transform.position, transform.rotation);
        }
        else if (!randomDrop)
        {
            LootGenerator.instance.makeThisLoot(transform.position, transform.rotation, PickupType.Health);
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
