using UnityEngine;
using System;
using System.Linq;
using System.Collections;
/// <summary>
///	Takes input for ThrowModel 
/// </summary>
public class ThrowController : Controller
{
   ThrowModel canon;
    public Animator myAnim;

    public void Start()
    {
        if (canon == null)
        {
            canon = this.GetComponent<ThrowModel>();
        }
        if (myAnim != null)
        {
            return;
        }
        else
            myAnim = GameObject.FindObjectOfType<Animator>();

        PlayerControllerFinal.instance.On_TossNow_Sent += On_TossNow_Received;
    }

    // method used to buffer the time b/n transition chnages
    IEnumerator WaitForIt(float waitTime) { 
    
        yield return new WaitForSecondsRealtime(waitTime);
        myAnim.SetBool("aiming", false);
        myAnim.SetBool("Throwing", false);
        yield return null;
    }

    protected virtual void FixedUpdate()
    {
        /*Uses Throw to trigger a branching nest of conditionals that will either throw a projectile
         at an angle or straight, or returns a debug log*/
         if (GameManager.instance.GetState() == GameState.inGame)
        {
            myAnim.SetBool("aiming", false);
            myAnim.SetBool("toss", false);
            myAnim.SetBool("Throwing", false);

            if (Input.GetButtonDown("Throw") && !Input.GetButton("Aim") && Ammo.instance._bullets > 0)
            {
                try
                {
                    myAnim.SetBool("toss", true);
                }
                catch (IndexOutOfRangeException e)
                {
                    throw new Exception("Out of Ammo. "+e.Message);
                }
            }
            else if(Input.GetButton("Aim"))
            {
                myAnim.SetBool("aiming", true);
                if (Input.GetButtonDown("Throw"))
                {
                    myAnim.SetBool("Throwing", true);
                    canon.ThrowAngle();
                }
            }
        }
    }

    public void On_TossNow_Received()
    {
        canon.ThrowStraight();
    }
}