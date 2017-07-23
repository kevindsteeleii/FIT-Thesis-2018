using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class death : MonoBehaviour {

	Animator myAnim;

	// Use this for initialization
	void Awake () {

		myAnim = GetComponent <Animator> ();

	}

	//false death-state upon contact with insta-Kill death object, just triggers death animation and ceases other ones
	void OnCollisionEnter (Collision col){
		if (col.gameObject.tag == "Death Object") {
			transform.Rotate (0, 0, 90);
			myAnim.SetBool ("dead", true);
		}
	}
}
