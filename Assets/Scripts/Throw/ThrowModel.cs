using System;
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Class used as empty model that generates the instantiated bullet to be shot
/// </summary>
public class ThrowModel : Model
{
    //Need to fix direction shot bug!!
    [Tooltip("The projectile prefab")]
    public GameObject testBullet;

    [Tooltip("The empty gameObject used as source or origin of aim")]
    public GameObject rootAim;

    [Tooltip("The aim reticle")]
    public GameObject aimReticle;

    [Tooltip("Intensity of throw")]
    [Range(400f, 1000f)]
    public float throwForce;

    [Tooltip("Decreases speed of aimed throw")]
    [Range(0f, 0.9f)]
    public float aimThrowSpeed;

    [Tooltip("Determines height offset of the beginning of throw ")]
    [Range(0.1f, 4f)]
    public float throwHeightOffset;

    int facing = 1;
    Vector3 right, left, direction, flatShot;
    // Use this for initialization
    void Awake()
    {
        right = Vector3.right;
        left = Vector3.left;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //facing is used to determine the direction the character is facing and uses a multiplier to affect trajectory of projectile
        //assignmentParameter = (outcomeOfConditional) ? a:b; // it equals a if the outcome is true and b if false
        flatShot = (PlayerController.facingRight) ? right : left;
        direction = aimReticle.transform.position - rootAim.transform.position;
    }

    /*Creates an object variable that when assigned is passed as a prefab for the projectile.
     In turn, a rigidbody variable is created, then assigned the rigidbody inside of the prefab.
     Then force is applied, Ammo class decrements the ammo count, and the bullet is destroyed after a set amount of time*/

    public void ThrowStraight()
    {
        if (Ammo.instance.bullets > 0)
        {
            GameObject bullet = Ammo.instance.GetPooledObject(rootAim.transform.position);
            bullet.transform.SetParent(null); //trying to fix possible  movement bug
            bullet.GetComponent<Rigidbody>().AddForce(flatShot * throwForce);
            Ammo.instance.ShootLoad();
        }
        else
            throw new ArgumentOutOfRangeException("Ran out of bullets");
    }

    public void ThrowAngle()
    {
        if (Ammo.instance.bullets > 0)
        {
            GameObject bullet = Ammo.instance.GetPooledObject(rootAim.transform.position);
            bullet.transform.SetParent(null); //trying to fix possible  movement bug
            Rigidbody tempRB;
            tempRB = bullet.GetComponent<Rigidbody>();
            tempRB.AddForceAtPosition(direction * throwForce * aimThrowSpeed, transform.position);
            Ammo.instance.ShootLoad();
        }
        else
            throw new ArgumentOutOfRangeException("Ran out of bullets");
    }
}
