using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack : MonoBehaviour {

	// Use this for initialization
	Animator myAnim;

	[Range (0,2)]
	public float hitStop = .5f;

	void Awake () {
		myAnim = GetComponent<Animator> ();

	}
	
	// Update is called once per frame
	void Update () {

		//triggers punch animation while K is pressed
		if (Input.GetButton ("Punch")) {
			StartCoroutine (HitStopperPunch());
			}


		//if L is pressed in the air, cancels other animations and starts the slam animation
		if (myAnim.GetBool ("grounded") == false && Input.GetButton("Slam")) {
			myAnim.SetBool ("slam", true);
		}
	}

	//declares slam bool false upon ground contact resetting its anim state
	void OnCollisionEnter (Collision collision){
		myAnim.SetBool ("slam", false);

	}

	//hitStop coroutine for punch to hold and then stop animation
	IEnumerator HitStopperPunch (){
		myAnim.SetBool ("punching", true);
		yield return new WaitForSeconds(hitStop);
		myAnim.SetBool ("punching", false);
	}
}
