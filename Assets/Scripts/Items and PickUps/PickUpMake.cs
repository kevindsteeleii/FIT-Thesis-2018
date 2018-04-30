using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class used to compartmentalize the purpose of pickup of items of various types
/// it is to be attached to a prefab to be instantiated by destroying enemy types
/// </summary>
public enum PickupType { Health, Money, Nothing };

public class PickUpMake : MonoBehaviour {

    // Use this for initialization making enum public allows for easier use outside of this class 
    public PickupType pickup;
    Renderer render;

    //add randomness to create a range to be controlled on object attached not script itself
    [Tooltip("The amount added")]
    [Range(0, 1000)]
    public int purse = 250;

    [Tooltip("The hp added")]
    [Range(0, 20)]
    public int health = 10;

    private Vector3 hidden = new Vector3 (-99f, -99f, -99f);

    /// <summary>
    /// Event that transmits the integer value of the in-game currency
    /// </summary>
    public event Action<int> On_Money_PickUp_Sent;

    /// <summary>
    /// Event that transmits the integer value of the health pickup
    /// </summary>
    public event Action<int> On_Health_PickUp_Sent;

    private void Start()
    {
        render = this.gameObject.GetComponent<Renderer>();
        //destroys spawned pickup after a certain timeFrame
        StartCoroutine(DestroyAfterTime());
    }

    //destroys after N-seconds with a blinker to illustrate time running out
    IEnumerator DestroyAfterTime()
    {
        /*Using this commentation to test instantiation of 
         pickUp item on enemy destruction*/

        yield return new WaitForSeconds(3.0f);
        for (int i = 0; i < 20; i++)
        {
            yield return new WaitForSeconds(.1f);
            render.enabled = false;
            yield return new WaitForSeconds(.1f);
            render.enabled = true;
        }
        //yield return new WaitForSecondsRealtime(5.0f);
        Destroy(this.gameObject);
        yield return null;
    }

    //destroys the object upon collision with an object tagged "Player"
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log( this.gameObject.name+ " was collected");
            OnPickUpCollected();
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Used to send the pickup information 
    /// </summary>
    private void OnPickUpCollected()
    {
        switch (pickup)
        {
            case PickupType.Health:
                On_Health_PickUp_Sent += PlayerStats.instance.On_Health_PickUp_Received;
                On_Health_PickUp_Sent(health);
                break;
            case PickupType.Money:
                Debug.Log("Money with value of " + purse);
                On_Money_PickUp_Sent += PlayerStats.instance.MakeMoney;
                On_Money_PickUp_Sent(purse);
                break;
            case PickupType.Nothing:
                break;
            default:
                break;
        }
    }

    private void Destroy(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.position = hidden;
    }
}