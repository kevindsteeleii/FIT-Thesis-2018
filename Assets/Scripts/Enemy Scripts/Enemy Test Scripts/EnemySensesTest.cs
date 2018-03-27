using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemySensesTest : MonoBehaviour {

    public EnStatsData enStats;
    Rigidbody myRB;
    Animator myAnim;
    //ray used to detect player distance/change states of FSM
    Ray ray = new Ray();

    public event Action<bool> On_IsPlayerNearby_Sent;

	// Use this for initialization
	void Start () {
        if (myRB != null)
        {
            return;
        }
        else
        {
            myRB = gameObject.GetComponent<Rigidbody>();
        }

        if (myAnim != null)
        {
            return;
        }
        else
        {
            myAnim = gameObject.GetComponent<Animator>();
        }

        On_IsPlayerNearby_Sent += myAnim.GetBehaviour<PatrolFSM>().On_IsPlayerNearby_Received;
        On_IsPlayerNearby_Sent += myAnim.GetBehaviour<AttackFSM>().On_IsPlayerNearby_Received;
    }

    private void FixedUpdate()
    {

        if (myRB.velocity.x > 0)    //if the velocity is positive so is the ray's direction
        {
            ray = new Ray(transform.position, Vector3.right);
        }
        else
        {
            ray = new Ray(transform.position, Vector3.left);
        }

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, enStats.minDistance))
        {
            if (On_IsPlayerNearby_Sent !=null)
            {
                if (hit.collider.tag == "Player")
                {
                    On_IsPlayerNearby_Sent(true);
                }
                else
                {
                    On_IsPlayerNearby_Sent(false);
                }
            }
        }
    }
}
