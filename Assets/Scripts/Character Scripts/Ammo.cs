using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Used to manage projectiles as a list holds its data and not much else
/// </summary>
public class Ammo : MonoBehaviour
{

    //List used to describe the ammo/banana clip
    public static List<Projectile> ammo;

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

    protected virtual void FixedUpdate()
    {
        //updates the ammo count
        if (ammo != null)
        {
            Bullets = ammo.Count;
            emptyClip = false;
            if (capacity == ammo.Count)
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
    public static void load()
    {
        Projectile newBullet = new Projectile();

        if (!fullCap)
        {
            ammo.Add(newBullet);
        }
        else
            Debug.Log("Filled to max capacity! Try throwing one.");
    }

    /// <summary>
    /// deletes last indexed projectile
    /// </summary>
	public static void shootLoad()
    {
        if (ammo.Count >= 1)
        {
            ammo.RemoveAt(ammo.Count - 1);
            Debug.Log("There are now " + ammo.Count + "shots left");
        }
        else
            Debug.Log("No Ammo");
    }


}
