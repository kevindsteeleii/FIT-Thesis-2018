using UnityEngine;
using System;
using System.Collections;
/// <summary>
/// Class that houses logic for Projectile
/// </summary>
public class Projectile : Model
{ 
    GameObject projectileObj;
    SphereCollider mySphere;
    Vector3 initPos;

    //damage the projectile causes
    //[Tooltip ("The timeout in seconds for the projectile life cycle")]
    //[Range (0f,15f)]
    //public float outTime = 3f;

    //[Tooltip("The zoneout in seconds for the projectile life cycle")]
    //[Range(0f, 30f)]
    //public float outOfBounds = 15f;

    [Tooltip("The amount of damage done by the projectile")]
    [Range(0, 15)]
    public int damage = 10;

    /// <summary>
    /// event that transmsits the physical game object body of the projectile
    /// </summary>
    public event Action<GameObject> On_BulletDestroyed_Sent;
   
    public virtual void Start()
    {
        if (projectileObj == null)
        {
            projectileObj = this.gameObject;
        }

        if(mySphere != null)
        {
            return;
        }
        else
        {
            mySphere = gameObject.GetComponent<SphereCollider>();
        }
        On_BulletDestroyed_Sent += Ammo.instance.Reuse;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "HurtBox" || other.gameObject.tag == "DeathBox" /*|| other.gameObject.tag == "ActiveBox"*/)
        {
            On_BulletDestroyed_Sent(projectileObj);
        }
        if (other.tag == "VisionCone")
        {
            Physics.IgnoreCollision(mySphere, other, true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "ActiveBox")
        {
            On_BulletDestroyed_Sent(projectileObj);
        }
    }

    //private void OnEnable()
    //{
    //    initPos = gameObject.transform.position;
    //}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            gameObject.SetActive(false);
        }
    }

}
#region TODO list, refactoring etc
/************TODO Refactoring********************************************************************//*
 1- 
 2-
 3-
 4-
 *************************************************************************************************/
#endregion