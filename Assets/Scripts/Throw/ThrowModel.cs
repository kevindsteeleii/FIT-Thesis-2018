using System;
using UnityEngine;
using System.Collections;

/// <summary>
/// Class used as empty model that generates the instantiated bullet to be shot
/// </summary>
public class ThrowModel : Model
{
    [Tooltip("The empty gameObject used as source or origin of aim")]
    public GameObject rootAim;

    [Tooltip("The aim reticle")]
    public GameObject aimReticle;

    //the game object used to manage the bullets when made
    [SerializeField]
    GameObject ammoManager;

    [Tooltip("X-Offset of the throw")]
    [Range(0f, 3f)]
    public float xOffset = .4f;

    [Tooltip("Intensity of throw")]
    [Range(0f, 100f)]
    public float throwForce;

    [Tooltip("Decreases speed of aimed throw")]
    [Range(0f, 04f)]
    public float aimThrowSpeed;

    [Tooltip("Determines height offset of the beginning of throw ")]
    [Range(0.1f, 4f)]
    public float throwHeightOffset;

    Vector3 aimDirection, direction;

    protected virtual void Update()
    {
        if (!RootAim.facesRight)
            direction = Vector3.left;
        else
            direction = Vector3.right;

        aimDirection = aimReticle.transform.localPosition;
        aimDirection.x = aimReticle.transform.localPosition.x * direction.x;

    }

    /*Creates an object variable that when assigned is passed as a prefab for the projectile.
     In turn, a rigidbody variable is created, then assigned the rigidbody inside of the prefab.
     Then force is applied, Ammo class decrements the ammo count, and the bullet is destroyed after a set amount of time*/

    public void ThrowStraight()
    {
        if (Ammo.instance.bullets > 0)
        {
            GameObject bullet = Ammo.instance.GetPooledObject(rootAim.transform.position);
            bullet.transform.SetParent(ammoManager.transform); //trying to fix possible  movement bug
            bullet.GetComponent<Rigidbody>().velocity = direction * throwForce;
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
            bullet.transform.SetParent(ammoManager.transform); //trying to fix possible  movement bug
            Rigidbody tempRB;
            tempRB = bullet.GetComponent<Rigidbody>();
            tempRB.AddForceAtPosition(aimDirection * throwForce * 10, transform.position, ForceMode.Acceleration);
            Ammo.instance.ShootLoad();
        }
        else
            throw new ArgumentOutOfRangeException("Ran out of bullets");
    }
}
