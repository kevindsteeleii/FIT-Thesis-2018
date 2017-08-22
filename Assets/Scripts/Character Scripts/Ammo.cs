using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Used to manage projectiles ammo count
/// </summary>
public class Ammo : MonoBehaviour
{
    //number of bullets
    public static int bullets;

    [Tooltip("Choose between 0 and 6 'Bullets' to preload Ammo class")]
    [Range(0,6)]
    public int testLoad;
    public int capacity = 6;

    //bool used to describe whether or not any ammo is available
    public static bool emptyClip;
    public static bool fullCap;
    
    // Use this for initialization
    void Awake()    {

        emptyClip = false;
        fullCap = false;
        bullets = testLoad;
    }

    /*Used to intiate a test clip of projectiles*/
    private void Start()
    {
 
    }

    //checks the veracity of full, empty, or loaded states of ammo class 
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
