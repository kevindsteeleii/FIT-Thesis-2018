using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
	//variables for movement
	[Range (1, 12)]
	public float runSpeed;


	Rigidbody myRB;
	Animator myAnim;
	bool facingRight = true;
	int dir;

	//for jumping player starts suspended above ground by default is not on ground
	[Range (0, 40)]
	public float jumpHeight;

	Vector3 respawnPos;
	Quaternion rot;


	// Use this for initialization
	void Awake ()
	{
		myRB = GetComponent <Rigidbody> ();
		myAnim = GetComponent <Animator> ();

		//these particular settings brute force initial conditions as being airborne and not on the ground
		myAnim.SetBool ("grounded", false);
		respawnPos = myRB.transform.position;
		rot = myRB.transform.rotation;
	}
	
	// Update is called once per frame gets expensive
	void Update ()
	{
		
		//Debug print out to verify outputs/ data
//		if (Input.anyKey) {
//			Debug.Log (Input.inputString);
//		}
		//Another debug log to determine velocity of player
//		Debug.Log(myRB.velocity);

	}

	//depends upon physics objects
	void FixedUpdate ()
	{


		//if false death-state is on all other actions cease
		if (!myAnim.GetBool ("dead")) {
			//this works but the in air or airborne cycle does not work it strobes
			if (Input.GetButton("Jump") && myAnim.GetBool ("grounded") == true) {
				Jump ();		
			}

			//lateral movement
			float move = Input.GetAxis ("Horizontal");
			myAnim.SetFloat ("speed", Mathf.Abs (move));
			myRB.velocity = new Vector3 (move * runSpeed, myRB.velocity.y, 0);


			if (move > 0 && !facingRight) {
				Flip ();
			} else if (move < 0 && facingRight) {
				Flip ();
			}
		} 
		// respawns after fake death state upon space bar press
		//later to be replaced with a respawn screen w/ countdown and penalty at a percentage of in game currency/points

		else if (Input.GetKey (KeyCode.Space) && myAnim.GetBool ("dead") == true) {
			transform.SetPositionAndRotation (respawnPos, rot);
			myAnim.SetBool ("dead", false);
			myAnim.SetBool ("grounded", true);
			facingRight = true;
		}

	}

	void Flip ()
	{
		facingRight = !facingRight;
		transform.Rotate (Vector3.up, 180.0f, Space.World);

	}
		
	void Jump ()
	{
		myAnim.SetBool ("grounded", false);
		myRB.velocity = new Vector3 (myRB.velocity.x, jumpHeight, 0);


	}

	void OnCollisionEnter (Collision collision)
	{
		if (collision.gameObject.tag == "Ground")
			myAnim.SetBool ("grounded", true);
	
	}


	void OnCollisionExit (Collision collision)
	{
		if (Mathf.Abs (myRB.velocity.y) > 0)
			myAnim.SetBool ("grounded", false);
	}


}