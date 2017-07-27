using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that houses logic for Projectile
/// </summary>
public class Projectile : Model
{

    GameObject gun;

    [SerializeField]
    private Projectile projectile;

    //list to host the ammo
    public static List<GameObject> ammo = new List<GameObject>();

    //Counter of ammo left
    public static int bullets;

    //damage the projectile causes
    public static float damage;

    [SerializeField]
    protected AimProxyModel reticle;

    [SerializeField]
    private GameObject player;

    protected Vector3 projectilePos;

    protected virtual void Awake()
    {
        
    }

    // Use this for initialization
    protected virtual void Start()
    {

        gun = this.gameObject;
        projectilePos = player.transform.position;
    }

    // Update is called once per frame
    protected virtual void Update()
    {

    }

    protected virtual void FixedUpdate()
    {
        //updates the ammo count
        if (ammo != null)
        {
            bullets = ammo.Count;
        }
        // updates shots in list of ammo
        projectilePos = player.transform.position;
    }
    //check for projectile 
    public virtual bool hasAmmo()
    {
        bool hasAmmo;
        hasAmmo = (bullets >= 1) ? false : true;

        return hasAmmo;
    }
    /// <summary>
    /// loads ammo automatically into clip when called
    /// </summary>
	public virtual void ammoLoader()
    {
        GameObject[] myAmmo = GameObject.FindGameObjectsWithTag("Projectile");
        foreach (GameObject bullet in myAmmo)
        {
            ammo.Add(bullet);
        }
    }

    /// <summary>
    /// deletes last indexed projectile
    /// </summary>
	public virtual void shootLoad()
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
