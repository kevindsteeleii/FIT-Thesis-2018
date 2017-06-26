using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPace : MonoBehaviour {
	[Range(0,2)]
	public float enemySpeed;

	//time it takes to move across
	[Range(0,3)]
	public float delta = 1.5f;

	private Vector3 startPos;


	// Use this for initialization
	void Awake () {
		startPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 v = startPos;
		v.x += delta * Mathf.Sin (Time.time * enemySpeed);
		transform.position = v;
	}
}
