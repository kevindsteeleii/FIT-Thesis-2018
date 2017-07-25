using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : Model {
	//Class used to create projectile with its own velocity

	//damage the projectile causes
	[SerializeField]
	protected float damage = 5f;
	// direction of the projectile
	[SerializeField]
	protected Vector3 direction;

	[SerializeField]
	protected AimProxyModel reticle;

	void Awake () {
//		r
	}

	// Use this for initialization
	void Start () {
		
	}


	
	// Update is called once per frame
	void Update () {
		
	}
	//My game is about robots though too
}
