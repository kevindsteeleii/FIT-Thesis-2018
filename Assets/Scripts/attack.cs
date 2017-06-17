using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack : MonoBehaviour {

	// Use this for initialization
	Animator myAnim;
	Rigidbody myRB;
	void Awake () {
		myAnim = GetComponent<Animator> ();
		myRB = GetComponent <Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		//triggers punch animation while K is pressed
			myAnim.SetBool ("punching",Input.GetKeyDown ("k"));

		//if L is pressed in the air, cancels other animations and starts the slam animation
		if (myAnim.GetBool ("grounded") == false && Input.GetKeyDown("l")) {
			myAnim.SetBool ("slam", true);
		}
	}

	//declares slam state false upon ground contact resetting its anim state
	void OnCollisionEnter (Collision collision){
		myAnim.SetBool ("slam", false);

	}
}
