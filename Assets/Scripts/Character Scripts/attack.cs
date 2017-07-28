using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack : MonoBehaviour {

	// Use this for initialization
	Animator myAnim;
    Rigidbody myRB;
    private bool isPunching;

	[Range (0,2)]
	public float hitStop = 1f;
    //floats used as vars for movement and runspeed


	void Awake () {
        isPunching = false;
		myAnim = GetComponent<Animator> ();
        myRB = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	protected virtual void FixedUpdate () {
        //triggers punch animation while K is pressed
        if (Input.GetButton ("Punch") || Input.GetButtonDown("Punch")) {
			StartCoroutine (HitStopperPunch());	}        
        
        //if L is pressed in the air, cancels other animations and starts the slam animation
        if (myAnim.GetBool ("grounded") == false && Input.GetButton("Slam")) {
			myAnim.SetBool ("slam", true);
		}
	}

	//declares slam bool false upon ground contact resetting its anim state
	void OnCollisionEnter (Collision collision){
		myAnim.SetBool ("slam", false); }

	//hitStop coroutine for punch to hold and then stop animation
	IEnumerator HitStopperPunch (){
		myAnim.SetBool ("punching", true);
        PlayerController.movementPermitted = false;
        //find better way to hitStop on the punch its jaggy atm
        yield return new WaitForSeconds(hitStop);
                    //stops movement while punch is animated restarts on end
		myAnim.SetBool ("punching", false);
        PlayerController.movementPermitted = true;
    }
}
