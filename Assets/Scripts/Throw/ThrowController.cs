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
        if (Input.GetButton("Throw"))   {
            Ammo.shootLoad();
        }
    }


}

