using UnityEngine;
using System.Collections;

/// <summary>
/// Class that stores and manages player stats with some additional functions
/// </summary>
public class PlayerStats : Singleton<PlayerStats>
{
    [SerializeField]
    StatsData stats;

    //camera needed to obscure/mask the player on separate layer by itself to simulate the transparency oscillation of the invincibility frames of older games
    [SerializeField]
    Camera camera;

    public static PlayerStats instance;
    /*number of intervals to be calculated by duration and wait time total*/
    int intervals = 0;


    bool invincible;
    public int wallet, hp;
    public static int playerHP;

    PlayerController player;

    // Use this for initialization
    new void Awake()
    {
        instance = this;
        player = this.GetComponent<PlayerController>();
        hp = stats.maxHP;
        invincible = false;
        wallet = 0;
    }

    void Update()
    {
        playerHP = hp;
        //Debug.Log("Invincible state is " + invincible);
        //Debug.Log("Stats are as follows: " + "HP is " + hp + "/" + stats.maxHP
           // + " Scrap :" + wallet);
        //makes sure the pickups addition to stat does not exceed the stat max itself
        if (hp > stats.maxHP) { hp = stats.maxHP; }
        if (wallet > stats.maxMoney) { wallet = stats.maxMoney; }
        Mathf.Clamp(hp, 0, stats.maxHP);

        if (invincible)
        {
            intervals = Mathf.RoundToInt((stats.duration * (1 / stats.waitTime)) / 2);
            StartCoroutine(IFramez());
            //Debug.Log("Now Mortal Again!!");
        }

    }

    public void resetState()
    {
        hp = stats.maxHP;
    }

    //culls the layer that holds player from game render view (layer 9)
    void CullOn()
    {
        camera.cullingMask &= ~(1 << 9);
    }
    //reveals the layer that holds the player in game render view
    void CullOff()
    {
        camera.cullingMask |= (1 << 9);
    }

    public void addMoney(int amount)
    {
        wallet += amount;
    }

    IEnumerator IFramez()
    {
        for (int i = 0; i < intervals; i++)
        {
            CullOn();
            //Debug.Log("Culling");
            yield return new WaitForSeconds(stats.waitTime / 2);
            CullOff();
            yield return new WaitForSeconds(stats.waitTime / 2);
            invincible = false;
            // Debug.Log("Not Culling");
        }
        //yield return null;
    }

    //handles damage taken by player character and its effects
    public void takeDamage(int dmg)
    {
        if (!invincible)
        {
            hp -= dmg;
            if (hp <= 0)
            {
                player.die();
            }
            else
            {
                invincible = true;
            }
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Enemy" && !invincible)
        {
            takeDamage(col.gameObject.GetComponent<Enemy>().damage);
            //Debug.Log("I've been hit!");
        }

        //logic that makes sure pickup affects the numbers of the stats
        if (col.gameObject.tag == "PickUp")
        {
            int amount  = 0;

            if (col.GetComponent<PickUp>().pickup == PickupType.Health)
            {
                amount = Mathf.RoundToInt(.25f * (stats.maxHP));
                hp = (hp + amount > stats.maxHP) ? stats.maxHP : hp + amount;
            }

            else if (col.GetComponent<PickUp>().pickup == PickupType.Money)
            {
                amount = col.GetComponent<PickUp>().purse;
                wallet += amount;
            }

            Debug.Log("The type of pick up is " + col.GetComponent<PickUp>().pickup);
            Destroy(col.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Death Object")
        {
            hp = 0;
        }
    }
}


