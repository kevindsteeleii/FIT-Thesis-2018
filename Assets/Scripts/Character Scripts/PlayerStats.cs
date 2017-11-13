using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Class that stores and manages player stats with some additional functions
/// </summary>
public class PlayerStats : Singleton<PlayerStats>
{
    //Scriptable Object/asset used to store and transmit default stats data and limits
    [SerializeField]
    StatsData stats;

    //camera needed to obscure/mask the player on separate layer by itself to simulate the transparency oscillation of the invincibility frames of older games
    [SerializeField]
    Camera camera;

    /*number of intervals to be calculated by duration and wait time total*/
    int intervals = 0;

    /// <summary>
    /// When Hp reaches zero, talk to subscriber
    /// </summary>
    public event Action HPisZero;

    /// <summary>
    /// Event that transmits the HP amount to a subscriber to listen for.
    /// </summary>
    public event Action<int> HPAmount;

    /// <summary>
    /// Event that transmits the Monetary amount to a subscriber to listen for.
    /// </summary>
    public event Action<int> MoneyAmount;

    bool invincible;

    /*To make parameter accessible outside of Singleton set up as a get and set parameter like so*/
    public int Wallet { get; private set; }

    /*To make parameter accessible outside of Singleton set up as a get and set parameter like so*/
    public int HP { get; set; }

    // Use this for initialization
    void Start()
    {
        HP = stats.maxHP;
        invincible = false;
        Wallet = 0;
        //assigns ResetHP() as subscriber of Restarting event
        GameManager.instance.Restarting += ResetHP;
    }

    void Update()
    {
        /*clamps the hp and money values between 0 and the max set 
        by the scriptable object*/
        Mathf.Clamp(HP, 0, stats.maxHP);
        Mathf.Clamp(Wallet, 0, stats.maxMoney);

        //if (hp > stats.maxHP) { hp = stats.maxHP; }
        //if hp is  0 or less broadcasts the event that hp is zero to game manager to start gameover
        if (HP <= 0)
        {
            if (HPisZero != null)
            {
                HPisZero();
            }
        }

        else
        {   /*  ¯\_(ツ)_/¯ */     }

        //if (wallet > stats.maxMoney) { wallet = stats.maxMoney; }

        if (invincible)
        {
            intervals = Mathf.RoundToInt((stats.duration * (1 / stats.waitTime)) / 2);
            StartCoroutine(IFramez());
            //Debug.Log("Now Mortal Again!!");
        }

        if (HPAmount != null)
        {
            HPAmount(HP);
        }

        if (MoneyAmount != null)
        {
            MoneyAmount(Wallet);
        }
    }

    /// <summary>
    /// Part of a group of methods called at reset of game after death.
    /// </summary>
    void ResetHP()
    {
        HP = stats.maxHP;
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

    //public void addMoney(int amount)
    //{
    //    wallet += amount;
    //}

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
            HP -= dmg;
            if (HP > 0)
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
            int amount = 0;

            if (col.GetComponent<PickUpType>().pickup == PickupType.Health)
            {
                amount = Mathf.RoundToInt(.25f * (stats.maxHP));
                HP = (HP + amount > stats.maxHP) ? stats.maxHP : HP + amount;
            }

            else if (col.GetComponent<PickUpType>().pickup == PickupType.Money)
            {
                amount = col.GetComponent<PickUpType>().purse;
                Wallet += amount;
            }

            Debug.Log("The type of pick up is " + col.GetComponent<PickUpType>().pickup);
            Destroy(col.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Death Object")
        {
            HP = 0;
            //if event has a subscriber...
            //if (TouchDeath != null)
            //{

            //    TouchDeath();
            //}
        }
    }
}


