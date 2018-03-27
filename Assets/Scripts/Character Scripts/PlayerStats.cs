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

    [SerializeField]
    CapsuleCollider hurtBox;

    /*number of intervals to be calculated by duration and wait time total*/
    int intervals = 0;

    /// <summary>
    /// When Hp reaches zero, talk to subscriber
    /// </summary>
    public event Action On_ZeroHP_Sent;

    /// <summary>
    /// Event that transmits the HP amount to a subscriber to listen for.
    /// </summary>
    public event Action<int> On_HPAmount_Sent;

    /// <summary>
    /// Event that transmits the Monetary amount to a subscriber to listen for.
    /// </summary>
    public event Action<int> On_MoneyAmount_Sent;

    public bool invincible { get; private set; }

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
        GameManager.instance.On_RestartState_Sent += ResetHP;
        //TBD
        if (hurtBox == null)
        {
            hurtBox = GetComponentInChildren<CapsuleCollider>();
        }
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
            if (On_ZeroHP_Sent != null)
            {
                On_ZeroHP_Sent();
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

        if (On_HPAmount_Sent != null)
        {
            On_HPAmount_Sent(HP);
        }

        if (On_MoneyAmount_Sent != null)
        {
            On_MoneyAmount_Sent(Wallet);
        }
    }
    /// <summary>
    /// Observer of the pickup event On_Health_PickUp_Sent
    /// </summary>
    /// <param name="amount"></param>
    public void MakeMoney(int amount)
    {
        Wallet += amount;
    }
    /// <summary>
    /// Observer of the pickup event On_Money_PickUp_Sent
    /// </summary>
    /// <param name="Hp"></param>
    public void On_Health_PickUp_Received(int Hp)
    {
        HP += Hp;
    }
    
    /// <summary>
    /// Part of a group of methods called at reset of game after death.
    /// </summary>
    void ResetHP()
    {
        Debug.Log("HP is reset!!!");
        HP = stats.maxHP;
    }

    void HPDeath()
    {
        HP = 0;
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

    IEnumerator IFramez()
    {
        invincible = true;
        hurtBox.enabled=false;
        for (int i = 0; i < intervals; i++)
        {
            CullOn();
            //Debug.Log("Culling");
            yield return new WaitForSeconds(stats.waitTime/2);
            CullOff();
            yield return new WaitForSeconds(stats.waitTime/2);
            invincible = false;
            hurtBox.enabled = true;
        }
        yield return null;
    }

    //handles damage taken by player character and its effects
    public void TakeDamage(int dmg)
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
        if (col.gameObject.tag == "Death Object")
        {
            HP = 0;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Death Object")
        {
            HP = 0;
        }
    }
}


