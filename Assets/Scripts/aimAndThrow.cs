using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aimAndThrow : MonoBehaviour {
	//holding an enemy/projectile or has ammo if you will
	private bool holding;
	private bool facesRight = true;
	// -1 for left 1 for right to adjust the direction of the reticule, set default at 
	private float leftRight = 1;

	private List <GameObject> heldObjects;

	[Range (0f,15f)]
	public float distance;
	[Range (0f,15f)]
	public float heightAdjust;
	//external proxy object for aiming reticule
	public GameObject reticule;

	private Vector3 startPos;
	private Vector3 endPos;

	//time it takes to move across
	[Range(0,1)]
	public float delta = 0f;


	// Use this for initialization
	void Awake () {
		//player holding nothing at start of game
		holding = false;
		reticule.SetActive(true);
		startPos = new Vector3 (transform.position.x+distance,transform.position.y+heightAdjust,transform.position.z);
		endPos = new Vector3 (transform.position.x,transform.position.y+distance,transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
		//returns true if object is being held and false if no objects are currently held by PC
//		holding = (heldObjects.Count > 0) ? true : false;

	}

	void FixedUpdate() {
		
		float mover = Input.GetAxis ("Horizontal");
		if (mover > 0 && !facesRight) {
			leftRight *= -1;
			flipAim ();
		} else if (mover < 0 && facesRight) {
			leftRight *= -1;
			flipAim ();
		}

		startPos = new Vector3 (transform.position.x+(distance*leftRight),transform.position.y+heightAdjust,transform.position.z); 
		endPos = new Vector3 (transform.position.x,transform.position.y+distance,transform.position.z);
		reticule.transform.position = startPos;

		if (Input.GetButtonDown ("Aim")) {
			readyAim ();
		}
		if (Input.GetButton ("Throw") && holding) {
			tossThrow ();
		}
	}
			//toggles visibility of oscillating aim reticule 
		public void readyAim(){
		if (Input.GetButton("Aim")) {
			reticule.transform.position = Vector3.Slerp(startPos,endPos,delta*Time.deltaTime);
		}
//		if (Input.GetButtonUp ("Aim") 
////			&& holding
//		) {
//			tossThrow ();
//			heldObjects.RemoveAt (heldObjects.Count - 1);
//		}
			
		}

	public void tossThrow(){
		Debug.Log ("throwing");
		//needs to throw projectile in direction of oscillation at stop of aiming
//		reticule.SetActive(false);
	}

	public void flipAim(){
		facesRight = !facesRight;
	}

}


