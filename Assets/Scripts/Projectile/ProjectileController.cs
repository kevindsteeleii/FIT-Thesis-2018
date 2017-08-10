using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Projectile controller class checks for input to throw and the ammo clip
/// projectile
/// </summary>
public class ProjectileController : Controller
{


    protected virtual void Awake()
    {

    }

    // Update is called once per frame
    protected virtual void Update()
    {
        //on hold of throw button 
        if (Input.GetButton("Grab"))
        {
            if (Ammo.emptyClip)
                Debug.Log("Empty Clip");
            else
                Debug.Log("Holding");
        }
        else if (Input.GetButtonUp("Grab") && Ammo.emptyClip)
        {
            Debug.Log("Empty Clip, Can");
        }
        else if (Input.GetButtonUp("Grab") && !Ammo.emptyClip)
        {
            Debug.Log("Throwing");
            Ammo.shootLoad();
        }

    }


}


