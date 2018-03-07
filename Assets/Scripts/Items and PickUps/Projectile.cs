using UnityEngine;
using System;
/// <summary>
/// Class that houses logic for Projectile
/// </summary>
public class Projectile : Model
{ 
    GameObject projectileObj;

    //damage the projectile causes
    [Range (0,15)]
    public int damage;
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
        On_BulletDestroyed_Sent += Ammo.instance.Reuse;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "HurtBox" || other.gameObject.tag == "ActiveBox"|| other.gameObject.tag == "DeathBox")
        {
            On_BulletDestroyed_Sent(projectileObj);
        }
    }

}
