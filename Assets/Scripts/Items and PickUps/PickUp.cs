using System;
using UnityEngine;
using System.Collections;

//enum used to switch what type of pickup 
public enum PickupType { Health, Money, Nothing};

/// <summary>
/// Class used to compartmentalize the purpose of pickup of items of various types
/// it is to be attached to a prefab to be instantiated by destroying enemy types
/// </summary>
public class PickUp : MonoBehaviour
{
    // Use this for initialization
    public PickupType pickup;

    //add randomness to create a range to be controlled on object attached not script itself
    [Tooltip("The amount added")]
    [Range(0, 1000)]
    public int purse = 250;


    private void Start()
    {
        //destroys spawned pickup after a certain timeFrame
        StartCoroutine(DestroyAfterTime());
    }

    IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSecondsRealtime(5.0f);
        Destroy(this.gameObject);
        yield return null;
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Destroy(this);
        }
    }

}
