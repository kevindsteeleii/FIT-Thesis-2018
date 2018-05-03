using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Component to be attached to the GameObject for melee range detection
/// refer to trashcannon for reference in how to set up the enemy for AI.
/// </summary>
public class EnemyMelee : MonoBehaviour {

    //public Rigidbody myRB;
    public Animator myAnim;
    public event Action<int> On_ProximityAlert_Sent;
    public EnemyVisionDetection trashVisionDetection;

    private void Start()
    {
        //if (myRB != null)
        //{
        //    return;
        //}
        //else
        //{
        //    myRB = gameObject.GetComponentInParent<Rigidbody>();
        //}

        if (myAnim != null)
        {
            return;
        }
        else
        {
            myAnim = gameObject.transform.parent.gameObject.GetComponentInChildren<Animator>();
        }

        if (trashVisionDetection != null)
        {
            return;
        }
        else
        {
            trashVisionDetection = gameObject.transform.parent.gameObject.GetComponentInChildren<EnemyVisionDetection>(); 
        }
        On_ProximityAlert_Sent += gameObject.transform.root.gameObject.GetComponent<EnemyDetection>().On_Stopper_Received;
        //to make the RB stopping possible uncomment line below!!!
        //On_ProximityAlert_Sent += trashVisionDetection.On_ProximityAlert_Received;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //Debug.Log("Player within melee range");
            myAnim.SetBool("meleeRange",true);
            On_ProximityAlert_Sent(0);
            //trashVisionDetection.On_ProximityAlert_Received(0);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //Debug.Log("Player within melee range");
            myAnim.SetBool("meleeRange", true);
            On_ProximityAlert_Sent(0);
            //trashVisionDetection.On_ProximityAlert_Received(0);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //Debug.Log("Player outside of melee range");
            myAnim.SetBool("meleeRange",false);
            //On_ProximityAlert_Sent(1);
            trashVisionDetection.On_ProximityAlert_Received(1);
        }
    }
}
