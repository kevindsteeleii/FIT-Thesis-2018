using UnityEngine;

/// <summary>
///	Takes input for ThrowModel 
/// </summary>
public class ThrowController : Controller
{
    ThrowModel canon;

    protected virtual void Start()
    {
        if (canon == null)
        {
            canon = this.GetComponent<ThrowModel>();
        }
    }

    protected virtual void FixedUpdate()
    {
        /*Uses Throw to trigger a branching nest of conditionals that will either throw a projectile
         at an angle or straight, or returns a debug log*/
        if (Input.GetButtonDown("Throw") && !Input.GetButton("Aim") && Ammo.bullets > 0)
        {
            canon.throwStraight();
        }

        else if (Input.GetButtonDown("Throw") && Input.GetButton("Aim") && Ammo.bullets > 0)
        {
            canon.throwAngle();
        }

        else if (Input.GetButtonDown("Throw") && Input.GetButtonDown("Slam") && !Input.GetButton("Aim") && Ammo.bullets > 0)
        {
            canon.throwUp();
        }

        else if (Input.GetButtonDown("Throw") && Ammo.bullets <= 0)
        {
            Ammo.shootLoad();
        }
    }
}

