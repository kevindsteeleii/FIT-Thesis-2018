using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//enum used to switch what type of pickup 
public enum PickupType { Health, Money, Nothing };

/// <summary>
/// Class used to compartmentalize the purpose of pickup of items of various types
/// it is to be attached to a prefab to be instantiated by destroying enemy types
/// </summary>
public class PickUp : MonoBehaviour
{
    // Use this for initialization making enum public allows for easier use outside of this class 
    public PickupType pickup;
    Renderer render;

    //add randomness to create a range to be controlled on object attached not script itself
    [Tooltip("The amount added")]
    [Range(0, 1000)]
    public int purse = 250;

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
        for (int i = 0; i < 10; i++)
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
        if (other.tag == "Player")
        {
            Debug.Log("Destroy " + this.gameObject.name);
            Destroy(this);
        }
    }
}
