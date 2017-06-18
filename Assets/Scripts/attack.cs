using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack : MonoBehaviour {

	// Use this for initialization
	Animator myAnim;
	Rigidbody myRB;
	Vector3 curVel;
	void Awake () {
		myAnim = GetComponent<Animator> ();
		myRB = GetComponent <Rigidbody> ();

	}
	
	// Update is called once per frame
	void Update () {

		//triggers punch animation while K is pressed
		if (Input.GetKey ("k")) {
			myRB.velocity = new Vector3 (0, 0, 0);
			myAnim.SetBool ("punching", Input.GetKey ("k"));
		}


		//if L is pressed in the air, cancels other animations and starts the slam animation
		if (myAnim.GetBool ("grounded") == false && Input.GetKeyDown("l")) {
			curVel = myRB.velocity;
			myRB.velocity = new Vector3 (0,curVel.y,0);
			myAnim.SetBool ("slam", true);
		}
	}

	//declares slam bool false upon ground contact resetting its anim state
	void OnCollisionEnter (Collision collision){
		myAnim.SetBool ("slam", false);

	}
}
