using UnityEngine;
using System.Collections;
using System;

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

    /*number of intervals to be calculated by duration and wait time total*/
    int intervals = 0;

    /// <summary>
    /// When Hp reaches zero, talk to subscriber
    /// </summary>
    public event Action HPisZero;

    /// <summary>
    /// Event triggered upon contact with a death object
    /// </summary>
    //public event Action TouchDeath;

    bool invincible;

    /*To make parameter accessible outside of Singleton set up as a get and set parameter like so*/
    public int wallet { get; set; }
    /*To make parameter accessible outside of Singleton set up as a get and set parameter like so*/
    public int hp { get; set; }

    // Use this for initialization
    void Start()
    {
        hp = stats.maxHP;
        invincible = false;
        wallet = 0;
    }

    void Update()
    {
        if (hp > stats.maxHP) { hp = stats.maxHP; }
        //if hp is  0 or less broadcasts the event that hp is zero to game manager to start gameover
        if (hp <= 0)
        {
            if (HPisZero != null)
            {
                HPisZero();
            }
        }
        //
        else
        {
            
        }

        if (wallet > stats.maxMoney) { wallet = stats.maxMoney; }

        Mathf.Clamp(hp, 0, stats.maxHP);

        if (invincible)
        {
            intervals = Mathf.RoundToInt((stats.duration * (1 / stats.waitTime)) / 2);
            StartCoroutine(IFramez());
            //Debug.Log("Now Mortal Again!!");
        }
    }

    public void ResetHP()
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
                //if (HPisZero != null)
                //{
                //    HPisZero();
                //}
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
            int amount = 0;

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
            //if event has a subscriber...
            //if (TouchDeath != null)
            //{

            //    TouchDeath();
            //}
        }
    }
}


