using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLifeDeath : MonoBehaviour {

#region Global Variables

    /*rigidboday and animator children of the player character 
     respawn position & respawn rotation     */
    Animator myAnim;
    Rigidbody myRB;
    Vector3 respawnPos;
    Quaternion rot;

#endregion // All global scope variables in class PlayerLifeDeath

    // Use this for initialization
    void Awake ()
    {
        myAnim = this.GetComponent<Animator>();
        myRB = this.GetComponent<Rigidbody>();
        respawnPos = myRB.transform.position;
        rot = myRB.transform.rotation;
    }


    // Update is called once per frame
    void FixedUpdate () {
		if (GameManager.instance.isDead)
        {

        }
        else
        {

        }
	}

    
}
