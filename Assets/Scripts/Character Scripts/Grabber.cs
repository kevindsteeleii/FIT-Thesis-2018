using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabber : MonoBehaviour {

	//range of grab uses slider before cemented into code
	[Range (0f,3f)]
	public float grabRange;

	//finds projectile class to populate if grab is successful
	[SerializeField]
	private Projectile model;

	//direction of ray and relative position of player
	private Vector3 direction;
	private Vector3 myPos;

	// Use this for initialization
	protected virtual void Start () {
	}
	
	// Update is called once per frame
	protected virtual void Update () {
		myPos = playerController.myPos;

		//reverses direction of Raycast to either left or right
		if (!playerController.facingRight)
			direction = Vector3.left;
		else
			direction = Vector3.right;
		
		// on grab button attempts to grab
		if (Input.GetButtonDown ("Grab")) {
			grab ();
		}

	}

	protected virtual void grab (){
		//look at Physics.OverlapSphere for detection and tags
		Debug.Log ("The direction is "+ direction);
//		RaycastHit hit;
		Vector3 forward = transform.TransformDirection (direction) * 10;
//		Debug.DrawRay(myPos,)

		if (Physics.Raycast (myPos, direction, grabRange)  ) {
			Debug.Log ("Something was HIT!!");

		}

	}

}

