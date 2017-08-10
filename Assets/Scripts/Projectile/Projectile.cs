using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that houses logic for Projectile
/// </summary>
public class Projectile : Model
{
    //damage the projectile causes
    public static float damage;

    [Range(0.1f,10f)]
    public float power;

    [SerializeField]
    protected AimProxyModel reticle;

    [SerializeField]
    private GameObject player;

    Rigidbody myRb;

    private void Awake()    {
        myRb = this.GetComponent<Rigidbody>();
    }

    protected Vector3 projectilePos;

    /// <summary>
    /// Throwing without aiming.
    /// </summary>
    public void throwing()  {
        myRb.AddForce(Vector3.right * RootAim.direction, ForceMode.VelocityChange);
    }
    /// <summary>
    /// Aimed throwing
    /// </summary>
    public void shooting()  {
        Vector3 force;
        force = (AimVisible.reticlePos - RootAim.aimPos );
        myRb.AddForce(force * power, ForceMode.VelocityChange);       
    }



}
