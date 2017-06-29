using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
  
	//this camera
	private Camera camera;
	//player as target
	public Transform target;
	//distance b/n camera and player
	public Vector3 offset ;

	public float smoothSpeed = 0.125f;

	void Awake (){
		camera = GetComponent<Camera> ();
		offset = Vector3.forward * -7.5f;

	}
	// Update is called once per frame after Update
	void FixedUpdate () {
		Vector3 desiredPosition = target.position + offset;
		Vector3 smoothedPosition = Vector3.Lerp (transform.position,desiredPosition,smoothSpeed);
		transform.position = smoothedPosition;
		}

}
