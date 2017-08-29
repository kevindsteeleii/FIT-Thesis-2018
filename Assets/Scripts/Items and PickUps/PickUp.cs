
using UnityEngine;

/// <summary>
/// Class used to compartmentalize the purpose of pickup of items of various types
/// </summary>
public class PickUp : MonoBehaviour
{

    //enum used to switch what type of pickup 
    public enum PickupType { Health, Energy, Money };
    // Use this for initialization
    public PickupType pickup;

    //bool used to indicate if particular drop is set or random upon destruction of its holder
    public bool randomDrop;


    //determines the amount returned to add from pickUp of given stat
    public int determineAmount(int stat)
    {
        int amount = 0;
        switch (pickup)
        {
            case PickupType.Health:
                amount = Mathf.RoundToInt(.25f * (stat));
                break;
            case PickupType.Money:
                amount = 50;
                break;
            case PickupType.Energy:
                amount = Mathf.RoundToInt(.15f * (stat));
                break;
                //default:
                //    break;
        }
        return amount;
    }

    // Update is called once per frame
    void Update()
    {
        if (randomDrop)
            RandomDrop();
    }

    //weighted probability drop of health, energy, or game currency at the current settings 
    void RandomDrop()
    {
        float dropWeight;
        dropWeight = Random.Range(1.0f, 100f);
        if (dropWeight >= 50 && dropWeight < 91) { pickup = PickupType.Health; }
        else if (dropWeight >= 21 && dropWeight < 49) { pickup = PickupType.Money; }
        else if (dropWeight <= 20) { pickup = PickupType.Energy; }
    }


}
