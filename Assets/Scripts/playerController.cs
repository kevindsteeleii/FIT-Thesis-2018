using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {
	//variables for movement
	[Range (1,12)]
	public float runSpeed;


	Rigidbody myRB;
	Animator myAnim;
	bool facingRight;
	int dir;

	//for jumping player starts suspended above ground by default is not on ground
	[Range (0,40)]
	public float jumpHeight;

	// Use this for initialization
	void Awake () {
		myRB = GetComponent <Rigidbody>();
		myAnim = GetComponent <Animator>();

		//these particular settings brute force initial conditions as being airborne and not on the ground
		myAnim.SetBool ("grounded", false);
		facingRight = true;
	}
	
	// Update is called once per frame gets expensive
	void Update () {
		
		//Debug print out to verify outputs/ data
//		if (Input.anyKey) {
//			Debug.Log (Input.inputString);
//		}
		//Another debug log to determine velocity of player
//		Debug.Log(myRB.velocity);

		}

	//depends upon physics objects
	void FixedUpdate(){

		//some code that just creates a back to life to undo the false death state
		if (Input.GetKey (KeyCode.Space)) {
			transform.Rotate (0, 0, -90);
			myAnim.SetBool ("dead", false);
		}

		//if false death-state is on all other actions cease
		if (!myAnim.GetBool ("dead")) {
			//this works but the in air or airborne cycle does not work it strobes
			if (Input.GetKey ("j") && myAnim.GetBool ("grounded") == true && myAnim.GetBool ("slam") == false) {
				Jump ();		
			}


			//lateral movement
			float move = Input.GetAxis ("Horizontal");
			myAnim.SetFloat ("speed", Mathf.Abs (move));
			myRB.velocity = new Vector3 (move * runSpeed, myRB.velocity.y, 0);


			if (move > 0 && !facingRight) {
				dir = -1;
				Flip ();
			} else if (move < 0 && facingRight) {
				dir = 1;
				Flip ();
			}
		}

	}

	void Flip() {
		facingRight = !facingRight;
		transform.Rotate (0.0f, 90f*dir, 0.0f);}

	void Jump (){
		myAnim.SetBool ("grounded", false);
		myRB.velocity = new Vector3 (myRB.velocity.x, jumpHeight,0);


		}
	void OnCollisionEnter (Collision collision){
		myAnim.SetBool ("grounded", true);
	
	}


	void OnCollisionExit (Collision collision){
		myAnim.SetBool ("grounded", false);
	}

}