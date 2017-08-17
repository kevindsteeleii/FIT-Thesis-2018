using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Used to manage projectiles as a list holds its data and not much else
/// </summary>
public class Ammo : MonoBehaviour
{
    /*Two float variables used to adjust mock bullets loaded in the start function*/
    [Range(0.1f, 12f)]
    public static float throwForce;
    [Range(0.1f, 10f)]
    public static float power;
    [Range(0.1f, 10f)]
    public static float damage;

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

    /*Used to intiate a test clip of projectiles*/
    private void Start()
    {
        for (int i = 0; i < 6; i++)
        {
            load();
        }
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
    public static void load()   {

        Projectile newBullet = new Projectile(power,throwForce,damage);

        if (!fullCap)
        {
            ammo.Add(newBullet);
        }
        else
            Debug.Log("Filled to max capacity! Try throwing one.");
    }

    /// <summary>
    /// Deletes last indexed projectile and launches different throwing
    /// </summary>
	public static void shootLoad()
    {
        if (Input.GetButton("Throw") && ammo == null)
            Debug.Log("No Ammo");

        else if (Input.GetButton("Throw") && !Input.GetButton("Aim"))
        {
            ammo[ammo.Count - 1].throwStraight();
        }
        else if (Input.GetButton("Throw") && Input.GetButton("Aim"))
        {
            ammo[ammo.Count - 1].throwAngle();
        }

        ammo.RemoveAt(ammo.Count - 1);
        Debug.Log("There are now " + ammo.Count + "shots left");

    }



}
