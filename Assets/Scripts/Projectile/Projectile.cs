using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that houses logic for Projectile
/// </summary>
public class Projectile : Model
{
    //damage the projectile causes
    public float damage;

    [Range(0.1f,10f)]
    public float power;

    Rigidbody myRb;

    bool right = true;
    int isRight = 1;

    [Range(0.1f, 12f)]
    float throwForce;
    //rootAim is the approximate position of origin and aimProx is the
    //position of the aiming reticle
    Vector3 rootAim;
    Vector3 aimProx;
    Vector3 direction;

    private void Awake()    {
        myRb = this.GetComponent<Rigidbody>();
        aimProx = AimVisible.reticlePos;
        rootAim = RootAim.aimPos;
    }

    // Update is called once per frame
    protected virtual void FixedUpdate()
    {
        right = RootAim.facesRight;
        aimProx = AimVisible.reticlePos;
        rootAim = RootAim.aimPos;
        isRight = (right) ? 1 : -1;
        direction = rootAim - aimProx;
    }

    /// <summary>
    /// Throw at the angle of the aiming reticle
    /// </summary>
    public void throwAngle()
    {
        myRb.AddForceAtPosition(direction * throwForce, rootAim, ForceMode.Acceleration);
    }

    /// <summary>
    /// Throw straight when prompted
    /// </summary>
    public void throwStraight()
    {
        myRb.AddForceAtPosition(Vector3.right * isRight * throwForce, rootAim);
    }



}
