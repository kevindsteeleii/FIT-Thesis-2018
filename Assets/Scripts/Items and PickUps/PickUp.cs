
using UnityEngine;

public class PickUp : MonoBehaviour {

    //enum used to switch what type of pickup 
    public enum PickupType  {Health,Energy,Money};
    // Use this for initialization
    public PickupType pickup;
    //amount to be added to specific type of player stat
    public int amount;
    //bool used to indicate if particular drop is set or random upon destruction of its holder
    public bool randomDrop;

    PlayerStats stats;

    private void Awake()
    {
        if (stats!= null)
        stats = this.GetComponent<PlayerStats>();
    }
    
    void Start () {

        switch (pickup)
        {
            case PickupType.Health:
                amount = Mathf.RoundToInt(.25f * (stats.maxHP));
                break;
            case PickupType.Money:
                amount = 50;
                    break;
            case PickupType.Energy:
                amount = Mathf.RoundToInt(.15f * (stats.maxEnergy));
                break;
            default:
                break;
        }
	}
	
    
	// Update is called once per frame
	void Update () {
        if (randomDrop)
            RandomDrop();
	}

    //weighted probability drop of health, energy, or game currency at the current settings 
    void RandomDrop()
    {
       float dropWeight;
        dropWeight = Random.Range(1.0f, 100f);
        if (dropWeight>=50 && dropWeight <91) { pickup = PickupType.Health; }
        else if( dropWeight>=21 && dropWeight<49) { pickup = PickupType.Money; }
        else if (dropWeight<=20) { pickup = PickupType.Energy; }

    }

   
}
