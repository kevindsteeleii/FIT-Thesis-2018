using UnityEngine;
using System;
/// <summary>
///	Takes input for ThrowModel 
/// </summary>
public class ThrowController : Controller
{
    ThrowModel canon;
    
    public Pool<ThrowModel> bulletPool;
    protected virtual void Start()
    {
        if (canon == null)
        {
            canon = this.GetComponent<ThrowModel>();
        }

    }

    protected virtual void LateUpdate()
    {
        /*Uses Throw to trigger a branching nest of conditionals that will either throw a projectile
         at an angle or straight, or returns a debug log*/
        if (Input.GetButtonDown("Throw") && !Input.GetButton("Aim") && Ammo.instance.bullets > 0)
        {
            bulletPool.pull.throwStraight();
        }

        else if (Input.GetButtonDown("Throw") && Input.GetButton("Aim") && Ammo.instance.bullets > 0)
        {
            canon.throwAngle();
        }

        else if (Input.GetButtonDown("Throw") && Ammo.instance.bullets <= 0)
        {
            Ammo.instance.ShootLoad();
        }
    }
}

