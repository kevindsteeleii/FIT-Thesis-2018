using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Used to manage projectiles ammo count
/// </summary>
public class Ammo : Singleton<Ammo>
{
    //number of bullets

    public int bullets;
    [Tooltip("Choose between 0 and 6 'Bullets' to preload Ammo class")]
    [Range(0, 6)]
    public int testLoad;
    public int capacity = 6;

    //bool used to describe whether or not any ammo is available
    bool fullCap;

    //creates Ammo as a singleton, the manager for Ammo may incorporate into PlayerStats in future...maybe
    public static new Ammo instance;

    // Use this for initialization
    new void Awake()
    {
        fullCap = false;
        bullets = testLoad;
        instance = this;
    }

    //adds another projectile to populate the list
    public virtual void load()
    {
        if (bullets<capacity)
        {
            bullets++;
        }
        else
            Debug.Log("Filled to max capacity! Try throwing one.");
    }

    /// <summary>
    /// Deletes from bullet int and launches different throwing
    /// </summary>
	public virtual void shootLoad()
    {
        if (Input.GetButton("Throw") && bullets<=0)
            Debug.Log("No Ammo, Empty Clip");

        else if (Input.GetButton("Throw") && bullets >= 0)
        {
            bullets--;
            Debug.Log("Shots fired! Only " + bullets + " shots left!");
        }
        else
            return;
    }
}
