
using UnityEngine;

/// <summary>
/// Class used to compartmentalize the purpose of pickup of items of various types
/// it is to be attached to a prefab to be instantiated by destroying enemy types
/// </summary>
public class PickUp : MonoBehaviour
{

    //enum used to switch what type of pickup 
    public enum PickupType { Health, Energy, Money };
    // Use this for initialization
    public PickupType pickup;
    
    //add randomness to create a range to be controlled on object attached not script itself
    [Tooltip ("The amount added")]
    [Range (0,1000)]
    public int purse = 250;

    [Tooltip("Bool that functions as an on-off for random drop per button press")]
    public bool randomDrop;

    // Update is called once per frame
    void Update()
    {
        
        if (randomDrop)
        {
            RandomDrop();
            randomDrop = false ;
        }
    }

    //weighted probability drop of health, energy, or game currency at the current settings 
    void RandomDrop()
    {
        Debug.Log("Pick Up became "+pickup);
        float dropWeight;
        dropWeight = Random.Range(1.0f, 100f);
        if (dropWeight >= 60 && dropWeight < 91) { pickup = PickupType.Health; }
        else if (dropWeight >= 21 && dropWeight < 59) { pickup = PickupType.Money; }
        else if (dropWeight <= 20 ) { pickup = PickupType.Energy; }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Destroy(this);
        }

    }


}
