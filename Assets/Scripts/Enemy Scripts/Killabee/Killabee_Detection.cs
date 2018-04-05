using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script that governs Killabee's sine wave movement and rotation upon changing velocity
/// </summary>
public class Killabee_Detection : MonoBehaviour
{
    public EnStatsData enStats;
    int multiplier = 1;
    Rigidbody myRB;
    Quaternion currentRot;

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
        myRB.velocity = Vector3.right * Time.timeScale * enStats.speed * multiplier;
    }
}

#region TODO list, refactoring etc
/************TODO Refactoring********************************************************************//*
 1-Get Player Detection to work w/n a certain range
 2-Fire on player when facing and w/n range
 3-stop when player found and resume previous behavior as in item (2)
 4-resume patrol when player is out of line of sight
 *5*-after demo done make it so that it stops and turns around if player w/n a certain range
 *************************************************************************************************/
#endregion
