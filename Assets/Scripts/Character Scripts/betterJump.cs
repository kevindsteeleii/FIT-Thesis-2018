using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class betterJump : MonoBehaviour {
	//initiates the mario jump where after the peak of jump character's gravity snappily increases
	//reduces float

	[Range (1,20)]
	public float fallMultiplier = 2.5f;

	//establishes the lowest jump possible upon quick release of jump button 
	[Range (1,10)]
	public float lowJumpMultiplier = 5f;
	// Use this for initialization
	Rigidbody myRB;
	void Awake () {
		myRB = GetComponent<Rigidbody> ();
	}

//	void Start () {
//		
//	}

	// Update is called once per frame
	void Update () {
		 //checks if rigidbody is descending and increases rate of drop for snappier jump does not accelerate tthe same if slamming down
		if (myRB.velocity.y < 0 ) {
			myRB.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier -1) * Time.deltaTime;
		} 

		else if (myRB.velocity.y > 0 && !Input.GetButton("Jump")) {
			myRB.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier -1) * Time.deltaTime;
		} 

	}

}
