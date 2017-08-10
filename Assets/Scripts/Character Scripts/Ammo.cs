using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Used to manage projectiles as a list holds its data and not much else
/// </summary>
public class Ammo : MonoBehaviour {

    //List used to describe the ammo/banana clip
    public static List<Projectile> ammo;

    //rootAim used to get the origin of throw
    [SerializeField]
    private RootAim bullseye;

    [SerializeField]
    private static int bullets;

    [Range(1, 6)]
    private static int capacity;

    //bool used to describe whether or not any ammo is available
    public static bool emptyClip;

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
    void Awake () {
        emptyClip = true;
	}

    protected virtual void FixedUpdate()
    {
        //updates the ammo count
        if (ammo != null)
        {
            Bullets = ammo.Count;
            emptyClip = false;
        }
        else
        {
            emptyClip = true;
        }

    }


    //adds another projectile to populate the list
    public static void load()    {
        Projectile newBullet = new Projectile();
        if (capacity<Bullets)
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
