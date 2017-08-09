using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that houses logic for Projectile
/// </summary>
public class Projectile : Model
{


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
    public void Shoot()    {
        //needs to add a certain amount of force at a direction tbd

    }

    protected virtual void FixedUpdate()
    {
     
    }
   



}
