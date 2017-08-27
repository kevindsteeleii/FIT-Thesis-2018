using UnityEngine;
using System.Collections;

/// <summary>
/// Class that stores and manages player stats with some additional functions
/// </summary>
public class PlayerStats : MonoBehaviour {

    [Tooltip("Sets Player Health")]
    [Range(1, 100)]
    public int maxHP = 30;

    public int currentHP;

    bool dead;

    bool invincible;
    PlayerController player;

    [Tooltip("Seconds of IFrames indicated by blinking")]
    [Range(2, 8)]
    public float waitTime = 6;

    Renderer rendy;

    //Money/In-game currency counting and energy min and maxes

    int wallet, energy;
    [Range (0,10000)]
    public int maxMoney,maxEnergy;

    [Tooltip("Percentage of money to take away upon death")]
    [Range(0,30)]
    public int percentDeduction;


    public delegate void AddStat(int current, int amount, int max);
    AddStat addThisStat = AddNewStat;



    // Use this for initialization
    void Awake()
    {
        player = this.GetComponent<PlayerController>();
        currentHP = maxHP;
        invincible = false;
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

    //Adds stat (health, money, energy) on pickup
    public static void AddNewStat(int current, int amt, int max) {
        if (current + amt >= max) { current = max; }
        else
            current += amt;
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
        for (int i = 0; i < waitTime * 5; i++)
        {
            rendy.material.color = newColor;
            yield return new WaitForSecondsRealtime(.1f);
            rendy.material.color = oldColor;
            yield return new WaitForSecondsRealtime(.1f);
        }
        invincible = false;
        // yield return null;
    }


    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            StartCoroutine(iFrames());
            takeDamage(col.gameObject.GetComponent<Enemy>().damage);
            Debug.Log("I've been hit!");

        }

        if (col.gameObject.tag == "PickUp") {
            PickUp item;
            item = col.GetComponent<PickUp>();         
            
            switch (item.pickup)
            {
                case PickUp.PickupType.Health:
                    addThisStat(currentHP, item.amount, maxHP);
                    Destroy(item.gameObject);
                    break;
                case PickUp.PickupType.Energy:
                    addThisStat(energy, item.amount, maxEnergy);
                    break;
                case PickUp.PickupType.Money:
                    addThisStat(wallet, item.amount, maxMoney);
                    break;
                default:
                    break;
            }
        }
    }
}
