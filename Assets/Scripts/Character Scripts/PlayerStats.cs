using UnityEngine;
using System.Collections;

/// <summary>
/// Class that stores and manages player stats with some additional functions
/// </summary>
public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    StatsData stats;

    int currentHP;
    bool invincible;

    PlayerController player;

    Renderer rendy;

    int wallet, energy;

    // Use this for initialization
    void Awake()
    {
        player = this.GetComponent<PlayerController>();
        currentHP = stats.maxHP;
        invincible = false;
        wallet = 0;
        energy = stats.maxEnergy;
    }

    void Update()
    {
        //debugger 
        Debug.Log("Stats are as follows: " + "HP is " + currentHP + "/" + stats.maxHP
            + " Scrap :" + wallet + " Energy is: " + energy + " / " + stats.maxEnergy);
        //makes sure the pickups addition to stat does not exceed the stat max itself
        if (currentHP > stats.maxHP) { currentHP = stats.maxHP; }
        if (wallet > stats.maxMoney) { wallet = stats.maxMoney; }
        if (energy > stats.maxEnergy) { energy = stats.maxEnergy; }

    }

    //handles damage taken by player character and its effects
    public void takeDamage(int dmg)
    {
        if (!invincible)
        {
            currentHP -= dmg;
            if (currentHP <= 0)
            {
                currentHP = 0;
                player.die();
            }
            else
            {
                invincible = true;
            }
        }

    }

    /// <summary>
    /// Handles I-Frames as blinking character to indicate invincibility status
    /// </summary>
    /// <returns></returns>
    IEnumerator iFrames()
    {
        //still need a way to make the model flicker adn not just the surface application will fix later
        Color oldColor;
        Color newColor = new Color(255, 255, 255, 0);
        rendy = this.gameObject.GetComponent<Renderer>();
        oldColor = rendy.material.color;
        for (int i = 0; i < stats.waitTime * 5; i++)
        {
            rendy.material.color = newColor;
            yield return new WaitForSecondsRealtime(.1f);
            rendy.material.color = oldColor;
            yield return new WaitForSecondsRealtime(.1f);
        }
        invincible = false;
        // yield return null;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            StartCoroutine(iFrames());
            takeDamage(col.gameObject.GetComponent<Enemy>().damage);
            Debug.Log("I've been hit!");
        }

        //logic that makes sure pickup affects the numbers of the stats
        if (col.gameObject.tag == "PickUp")
        {
            int amount;

            if (col.GetComponent<PickUp>().pickup == PickUp.PickupType.Energy)
            {
                amount = Mathf.RoundToInt(.25f * (stats.maxEnergy));
                energy += amount;
            }

            else if (col.GetComponent<PickUp>().pickup == PickUp.PickupType.Health)
            {
                amount = Mathf.RoundToInt(.25f * (stats.maxHP));
                currentHP += amount;
            }

            else if (col.GetComponent<PickUp>().pickup == PickUp.PickupType.Money)
            {
                amount = col.GetComponent<PickUp>().purse;
                wallet += amount;
            }


            Debug.Log("The type of pick up is " + col.GetComponent<PickUp>().pickup);
            Destroy(col.gameObject);

        }
    }
}


