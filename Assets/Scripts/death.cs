using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class death : MonoBehaviour {

	Animator myAnim;
	Rigidbody myRB;
	Vector3 respawnPos;

	// Use this for initialization
	void Awake () {
		myRB = GetComponent <Rigidbody> ();
		myAnim = GetComponent <Animator> ();
		respawnPos = myRB.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		//some code that just creates a back to life to undo the false death state
		if (Input.GetKey (KeyCode.Space)&&myAnim.GetBool("dead")==true) {
			transform.Rotate (0, 0, -90);
			transform.position = respawnPos;
			myAnim.SetBool ("dead", false);
			myAnim.SetBool ("grounded", true);
		}
	}
	//false death-state upon contact with insta-Kill death object, just triggers death animation and ceases other ones
	void OnCollisionEnter (Collision col){
		if (col.gameObject.tag == "Death Object") {
			transform.Rotate (0, 0, 90);
			myAnim.SetBool ("dead", true);
		}
	}
}
