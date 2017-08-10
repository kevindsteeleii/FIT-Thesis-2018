using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Projectile controller class checks for input to throw and the ammo clip
/// projectile
/// </summary>
public class ProjectileController : Controller
{

    [SerializeField]
    protected Projectile model;

    protected virtual void Awake()  {

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
                Debug.Log("Loading");
        }
        if (!Ammo.emptyClip)
        { 
            if (Input.GetButtonUp("Throw"))
            {
                Debug.Log("Throwing");
                model.throwing();
                Ammo.shootLoad();
            }

            else if (Input.GetButton("Throw") && Input.GetButton("Aim")) {
                Debug.Log("Aimed Throw");
                model.shooting();
                Ammo.shootLoad();
            }
        }

    }


}


