using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Used to manage projectiles ammo count
/// </summary>
public class Ammo : MonoBehaviour
{

    //number of bullets
    public static int bullets;

    public static int capacity = 6;

    //bool used to describe whether or not any ammo is available
    public static bool emptyClip;
    public static bool fullCap;

    public static int Bullets
    {
        get
        {
            return bullets;
        }

        set
        {
            bullets = value;
        }
    }

    // Use this for initialization
    void Awake()    {

        emptyClip = false;
        fullCap = false;
    }

    /*Used to intiate a test clip of projectiles*/
    private void Start()
    {
 
    }

    protected virtual void Update()
    {
        //updates the ammo count
        if (bullets >=0)
        {
            
            emptyClip = false;
            if (capacity == bullets)
            {
                fullCap = true;
            }
            else
                fullCap = false;
        }
        else
        {
            emptyClip = true;
            fullCap = false;
        }
    }

    //adds another projectile to populate the list
    public static void load()   {

        if (!fullCap)
        {
            bullets ++;
        }
        else
            Debug.Log("Filled to max capacity! Try throwing one.");
    }

    /// <summary>
    /// Deletes last indexed projectile and launches different throwing
    /// </summary>
	public static void shootLoad()
    {
        if (Input.GetButton("Throw") && bullets <=0 )
            Debug.Log("No Ammo, Empty Clip");

        else if (Input.GetButton("Throw") && bullets >= 0)   {
            bullets --;
            Debug.Log("Shots fired! Only "+bullets+" shots left!");
        }      
    }



}
