using UnityEngine;
using System.Collections.Generic;
using System;

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
    public int bullets { get; private set; }

    public List<GameObject> ammoList;
    Vector3 hidden = new Vector3(1000f, 1000f, 1000f);

    /// <summary>
    /// Event that passest the amount of bullets left in ammo
    /// </summary>
    public event Action<int> MyAmmo;

    //Collection that populates with all enemies present to be implemented in an enemy manager class later
    private Enemy[] enemiesPresent;

    // Use this for initialization
    void Start()
    {
        bullets = testLoad;
        cap = capacity;
        ammoList = new List<GameObject>();

        enemiesPresent = FindObjectsOfType<Enemy>();

        //assigns Ammo as subscriber if enemies are present will refactor < Kev Note
        if (enemiesPresent != null)
        {
            foreach (Enemy enemy in enemiesPresent)
            {
                enemy.BecomeAmmo += Load;
            }
        }

        Populate();
    }

    void Update()
    {
        if (MyAmmo != null)
        {
            MyAmmo(bullets);
        }
    }

    /// <summary>
    /// Populates the ammoList with prefabs
    /// </summary>
    void Populate()
    {
        for (int i = 0; i < capacity; i++)
        {
            GameObject obj = Instantiate(bullet, hidden, Quaternion.identity);
            obj.transform.SetParent(this.gameObject.transform);
            obj.SetActive(false);
            ammoList.Add(obj);
        }
    }

    /// <summary>
    /// Return Object that is inactive in hierarchy
    /// </summary>
    public GameObject GetPooledObject(Vector3 position)
    {
        for (int i = 0; i < capacity; i++)
        {
            if (!ammoList[i].activeInHierarchy)
            {
                ammoList[i].SetActive(true);
                ammoList[i].transform.position = position;
                Debug.Log("Number " + i + " bullet");
                return ammoList[i];
            }
        }
        return null;
    }

    /// <summary>
    /// As the name suggests it increments the bullet count just
    /// as the player collides w/ ammo type loot and collects it on contact
    /// </summary>
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
        {
            Debug.Log("Loading...Now you have " + bullets + " bullets!!");
            Reuse();
        }
    }

    /// <summary>
    /// Used to "increase" available ammo by recycling the oldest active member and recycling it in the object pool
    /// </summary>
    void Reuse()
    {
        for (int i = 0; i < capacity; i++)
        {
            if (ammoList[i].activeInHierarchy)
            {
                ammoList[i].SetActive(false);
                ammoList[i].transform.position = hidden;
            }
        }
    }
    /// <summary>
    /// Deletes from bullet int and launches different throwing
    /// </summary>
    public void ShootLoad()
    {
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
        //add before logic subtract after logic
        bullets--;
    }
}