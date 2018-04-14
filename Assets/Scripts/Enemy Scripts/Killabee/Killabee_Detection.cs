using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script that governs Killabee's sine wave movement and rotation upon changing velocity
/// </summary>
public class Killabee_Detection : MonoBehaviour
{
    public EnStatsData enStats;

    int multiplier = 1; //used to start/stop the enemy
    int modifier = 1;   // used to modify velocity upon player detection

    Rigidbody myRB;

    public MeshCollider visionCone;

    Quaternion currentRot;  //rotation to be set by the direction enemy moves in

    // Use this for initialization
    void Start()
    {
        if (myRB != null)
        {
            return;
        }
        else
        {
            myRB = gameObject.GetComponent<Rigidbody>();
        }

        if (visionCone != null)
        {
            return;
        }
        else
        {
            visionCone = gameObject.GetComponentInChildren<MeshCollider>();
        }
    }

    void Turn()
    {
        multiplier *= -1;
        gameObject.transform.rotation = currentRot;
    }

    private void FixedUpdate()
    {
        RaycastHit hit;
        Ray ray = new Ray();

        if (myRB.velocity.x > 0)    //if the velocity is positive so is the ray's direction
        {
            ray = new Ray(transform.position, Vector3.right);
            currentRot = new Quaternion(0, 0, 0, 1);
        }
        else
        {
            ray = new Ray(transform.position, Vector3.left);
            currentRot = new Quaternion(0, 180, 0, 1);
        }

        if (Physics.Raycast(ray, out hit, enStats.minDistance))
        {
            if (hit.collider.tag == "EndPoint")
            {
                Turn();
            }
        }

        myRB.velocity = Vector3.right * Time.timeScale * enStats.speed*modifier * multiplier;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            modifier = 0;
        }

        if(other.tag == "Hand")
        {
            Physics.IgnoreCollision(other, visionCone, true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            modifier = 0;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            modifier = 1;
        }
    }
}

#region TODO list, refactoring etc
/************TODO Refactoring********************************************************************//*
 1-resume patrol when player is out of line of sight not cone of vision, maybe
 2- make it so that the traversal time is the same regardless of the distance traveled
 (*hint* use maths)
 *3*-add sound cue for reloading
 * 4-slow down the fire rate (way too fast), its scary
 *5*-after demo done make it so that it stops and turns around if player w/n a certain range
 *************************************************************************************************/
#endregion