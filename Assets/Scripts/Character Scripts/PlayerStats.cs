using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Class that stores and manages player stats with some additional functions
/// </summary>
public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    StatsData stats;

    //gets the mesh renderers for the separate body parts, pending change upon completion of beta+ mesh
    MeshRenderer [] myRender;

    int currentHP;
    bool invincible;
    public static int cash, hp, maxHP;

    PlayerController player;
    int wallet, energy;

    //wait time is length of single wait b/n visible/invisible and duration is total lenght of time
    [Tooltip("Wait Time describes the intervals between visible and invisible")]
    [Range(0,1)]
    public float waitTime;
    //[Tooltip("Duration is length of i-frames")]
    //public int duration;
	
	//instantiates the mainSlider, which is our healthbar
	public Slider mainSlider;

    // Use this for initialization
    void Awake()
    {
        player = this.GetComponent<PlayerController>();
		
        currentHP = stats.maxHP;
        maxHP = stats.maxHP;
        invincible = false;
        wallet = 0;
        energy = stats.maxEnergy;
        myRender = this.gameObject.GetComponentsInChildren<MeshRenderer>(); 
		/*sets the minimum and maximum values for the health slider*/
		mainSlider.minValue = 0;
		mainSlider.maxValue = maxHP;
    }

    void Update()
    {
        cash = wallet;
        hp = currentHP;
		//updating HP value for the slider
		mainSlider.value = currentHP;
        //debugger 
        Debug.Log("Stats are as follows: " + "HP is " + currentHP + "/" + stats.maxHP
            + " Scrap :" + wallet + " Energy is: " + energy + " / " + stats.maxEnergy);
        //makes sure the pickups addition to stat does not exceed the stat max itself
        if (currentHP > stats.maxHP) { currentHP = stats.maxHP; }
        if (wallet > stats.maxMoney) { wallet = stats.maxMoney; }
        if (energy > stats.maxEnergy) { energy = stats.maxEnergy; }
        Mathf.Clamp(currentHP, 0, stats.maxHP);

        if (invincible)
        {
            StartCoroutine(IFramer());
            Debug.Log("Now Mortal");
        }
    }

    //renders the array of MeshRenderers invisible
    void blinkOff()
    {
        foreach (MeshRenderer mRender in myRender)
        {
            mRender.enabled = false;
            Debug.Log("Invincible");
        }
    }

    //renders the array of MeshRenderers visible
    void blinkOn()
    {
        foreach (MeshRenderer mRender in myRender)
        {
            mRender.enabled = true;
            Debug.Log("still Invincible");
        }
    }

    //turns off the mesh renderers on and off to show damaged/invincibility state
    IEnumerator IFramer()
    {
        for (int i = 0; i <6; i++)
        {
            blinkOff();
            yield return new WaitForSeconds(waitTime);
            blinkOn();
            yield return new WaitForSeconds(waitTime );
        }
        invincible = false;
        yield return null;        
    }

    //handles damage taken by player character and its effects
    public void takeDamage(int dmg)
    {
        if (!invincible)
        {
            currentHP -= dmg;
            if (currentHP <= 0)
            {
                player.die();
            }
            else
            {
                invincible = true;
            }

        }
    }

    public void resetState()
    {
        currentHP = stats.maxHP;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Enemy")
        {
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
                currentHP = (currentHP + amount > stats.maxHP) ? stats.maxHP :currentHP + amount; 
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


