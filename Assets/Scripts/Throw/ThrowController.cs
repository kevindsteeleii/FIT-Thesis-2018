using UnityEngine;

/// <summary>
///	Takes input for ThrowModel
/// </summary>
public class ThrowController : Controller
{
    protected virtual void FixedUpdate()
    {
        /*Uses Throw to trigger a branching nest of conditionals that will either throw a projectile
         at an angle or straight, or returns a debug log*/
        if (Input.GetButtonDown("Throw") && !Input.GetButton("Aim") && Ammo.bullets>0)   {
            ThrowModel.throwStraight();            
        }

        else if (Input.GetButtonDown("Throw") && Input.GetButton("Aim") && Ammo.bullets > 0)        {
            ThrowModel.throwAngle();
        }

        else if (Input.GetButtonDown("Throw") && Ammo.bullets <= 0)        {
            Ammo.shootLoad();
        }      
    }
}

