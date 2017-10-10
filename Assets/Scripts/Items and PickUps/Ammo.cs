using UnityEngine;
using System.Collections.Generic;
/// <summary>
/// Used to manage projectiles ammo count
/// any reference made to Ammo singleton follow this example
/// ClassName.instance.publicVariable
/// or ClassName.instance.publicFunction()
/// </summary>
public class Ammo : Singleton<Ammo>
{

    //the bullet or single projectile to be "loaded" into the PoolList known as ammoList
    public GameObject bullet;
    //creation of collection to handle pooling of ammo, as opposed to instantiating and destroying and using up lots of memory allocation to do so
    //at this scope its harmless but its best practice

    [Tooltip("Choose between 0 and 6 'Bullets' to preload Ammo class")]
    [Range(0, 6)]
    public int testLoad;

    //both are bullet capacity, one is private though for internal use
    public int capacity = 6;
    int cap;

    /*To make parameter accessible outside of Singleton set up as a get and set parameter like so*/
    public int bullets { get; set; }

    // Use this for initialization
    void Start()
    {
        bullets = testLoad;
        cap = capacity;
        //creates the pool List of bullets

    }

    public void Load()
    {
        bullets++;
        Mathf.Clamp(bullets, 0, cap);

        if (bullets >= cap)
        {
            bullets = cap;
            Debug.Log("Filled to max capacity! Try throwing one.");
        }
        else
            Debug.Log("Loading...Now you have " + bullets + " bullets!!");
    }

    /// <summary>
    /// Deletes from bullet int and launches different throwing
    /// </summary>
    public void ShootLoad()
    {
        bullets--;
        Mathf.Clamp(bullets, 0, cap);

        if (Input.GetButton("Throw") && bullets <= 0)
        {
            bullets = 0;
            Debug.Log("No Ammo, Empty Clip");
        }

        else if (Input.GetButton("Throw") && bullets >= 0)
        {
            Debug.Log("Shots fired! Only " + bullets + " shots left!");
            
        }
        else
            return;
    }
}